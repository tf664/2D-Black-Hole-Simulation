using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace blackhole
{
    public enum SpawnMode
    {
        Normal,
        SingleRay,
        PointSource
    }

    public partial class Form1 : Form
    {
        private SpawnMode currentMode = SpawnMode.Normal;
        public const int screenWidth = 1000, screenHeight = 800;
        public const double rayAmount = 40;
        public const double metersPerPixel = 4e8;      // 1 pixel = 400 million meters
        public const double speed = 5e10; // m/s, reasonable for visualization

        // --- Physical constants (SI units) ---
        public const double G = 6.67430e-11;           // Gravitational constant (m^3 / (kg s^2))
        public const double c = 299792458.0;           // Speed of light (m / s)
        public const double mass = 8.54e36;            // Mass (kg) (of Sagittarius A*)
        public static readonly double r_s = 2.0 * G * mass / (c * c); // Schwarzschild radius (m)
        public static double r_s_dynamic = 2.0 * G * mass / (c * c);


        // --- Scaling ---
        public static double holeX_m = (screenWidth / 2 - 50) * metersPerPixel;
        public static double holeY_m = (screenHeight / 2) * metersPerPixel;
        public static float holeRadiusPixels = (float)(r_s / metersPerPixel);

        // Mouse drag variables
        private bool isDragging = false;
        private Point lastMousePos;

        Bitmap screen;
        System.Windows.Forms.Timer timer;
        List<Ray> rays = new List<Ray>();

        public Form1()
        {
            InitializeComponent();

            ClientSize = new Size(screenWidth, screenHeight);
            Text = "Black Hole Simulation";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            DoubleBuffered = true;

            screen = new Bitmap(screenWidth, screenHeight);

            SpawnRays();

            Paint += Form1_Paint;

            // Mouse event handlers for dragging
            MouseDown += Form1_MouseDown;
            MouseMove += Form1_MouseMove;
            MouseUp += Form1_MouseUp;

            // Timer for animation
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 20; // ~50 FPS
            timer.Tick += Timer_Tick;
            timer.Start();
        }



        private void Timer_Tick(object? sender, EventArgs e)
        {
            double dt = timer.Interval / 1000.0; // seconds per frame

            for (int i = 0; i < rays.Count; i++)
            {
                if (rays[i].IsActive)
                {
                    rays[i].Step(dt);
                }
            }

            RedrawScene();
            Invalidate();
        }

        private void RedrawScene()
        {
            using (Graphics g = Graphics.FromImage(screen))
            {
                g.Clear(Color.Black);

                // Draw black hole in pixels
                DrawHole(g, screenWidth / 2, screenHeight / 2, holeRadiusPixels); // match physics                // Draw rays
                foreach (var ray in rays)
                    // if (ray.IsActive)
                    ray.DrawTrail(g);
                foreach (var ray in rays)
                    ray.Draw(g);
            }
        }

        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(screen, 0, 0, screen.Width, screen.Height);
        }

        private void DrawHole(Graphics g, float centerX, float centerY, float radius)
        {
            // Convert meters -> pixels
            float rsPixels = (float)(r_s_dynamic / metersPerPixel);
            float photonSpherePixels = (float)((1.5 * r_s_dynamic) / metersPerPixel);

            // Center in pixels
            float cx = (float)(holeX_m / metersPerPixel);
            float cy = (float)(holeY_m / metersPerPixel);

            using (var darkRedBrush = new SolidBrush(Color.DarkRed))
            using (var redPen = new Pen(Color.Red, 2))
            {
                // Draw black event horizon disk
                g.FillEllipse(
                    darkRedBrush,
                    cx - rsPixels, cy - rsPixels,
                    rsPixels * 2, rsPixels * 2
                );

                // Draw red photon sphere ring
                g.DrawEllipse(
                    redPen,
                    cx - photonSpherePixels, cy - photonSpherePixels,
                    photonSpherePixels * 2, photonSpherePixels * 2
                );
            }
        }

        private void BtnNormal_Click(object sender, EventArgs e)
        {
            currentMode = SpawnMode.Normal;
            SpawnRays();
        }

        private void BtnSingleRay_Click(object sender, EventArgs e)
        {
            currentMode = SpawnMode.SingleRay;
            SpawnRays();
        }

        private void BtnPointSource_Click(object sender, EventArgs e)
        {
            currentMode = SpawnMode.PointSource;
            SpawnRays();
        }

        private void BtnSettings_Click(object? sender, EventArgs e)
        {
            settingsPanel.Visible = !settingsPanel.Visible;
        }

        private void MassSlider_Scroll(object? sender, EventArgs e)
        {
            double scale = massSlider.Value / 50.0;
            massLabel.Text = $"Mass scale: {massSlider.Value}";

            // mass 50 = 4.3 mio solar masses, mass 10 = 0.2 of original = ~860000 solar masses
            double newMass = mass * scale;
            r_s_dynamic = 2.0 * G * newMass / (c * c);
            holeRadiusPixels = (float)(r_s_dynamic / metersPerPixel);

            // reset rays
            SpawnRays();

            RedrawScene();
            Invalidate();
        }

        private void Form1_MouseDown(object? sender, MouseEventArgs e)
        {
            // Only allow dragging when settings panel is visible
            if (settingsPanel.Visible && e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastMousePos = e.Location;
            }
        }

        private void Form1_MouseMove(object? sender, MouseEventArgs e)
        {
            if (isDragging && settingsPanel.Visible)
            {
                // Calculate the movement in pixels
                int deltaX = e.X - lastMousePos.X;
                int deltaY = e.Y - lastMousePos.Y;

                // Convert pixel movement to meters and update hole position
                holeX_m += deltaX * metersPerPixel;
                holeY_m += deltaY * metersPerPixel;

                // Update the mouse position for next frame
                lastMousePos = e.Location;

                // Respawn rays with new black hole position
                SpawnRays();
                
                // Redraw the scene
                RedrawScene();
                Invalidate();
            }
        }

        private void Form1_MouseUp(object? sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        /// <summary>
        /// Spawns the different ray modes.
        /// </summary>
        private void SpawnRays()
        {
            rays.Clear();

            if (currentMode == SpawnMode.Normal)
            {
                for (int i = 0; i < rayAmount; i++)
                {
                    double y = (i + 0.5) * (screenHeight / rayAmount) * metersPerPixel;
                    double x = 0;
                    rays.Add(new Ray(x, y, speed, 0));
                }
            }
            else if (currentMode == SpawnMode.SingleRay)
            {
                double y = holeY_m + 1.7315 * r_s_dynamic;
                double x = 0;
                rays.Add(new Ray(x, y, speed, 0));
            }
            else if (currentMode == SpawnMode.PointSource)
            {
                double sx = 0;
                double sy = holeY_m;
                int count = 50;
                for (int i = 0; i < count; i++)
                {
                    double angle = -Math.PI / 4 + i * (Math.PI / 2) / (count - 1);
                    double dx = speed * Math.Cos(angle);
                    double dy = speed * Math.Sin(angle);
                    rays.Add(new Ray(sx, sy, dx, dy));
                }
            }
        }
    }
}
