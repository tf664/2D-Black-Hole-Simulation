using System;
using System.Collections.Generic;
using System.Drawing;

namespace blackhole
{
    public class Ray
    {
        // State variables in meters and radians
        public double x, y;            // Cartesian position (m)
        public double r, phi;          // Polar position relative to hole (m, rad)
        public double dr, dphi;        // Polar rates (m/s, rad/s)

        public bool IsActive { get; private set; } = true;

        private readonly List<PointF> trail = new List<PointF>();

        // Conserved energy parameter for null geodesic
        private double E;

        public Ray(double x, double y, double dx, double dy)
        {
            this.x = x;
            this.y = y;

            // Polar position relative to hole
            double rx = x - Form1.holeX_m;
            double ry = y - Form1.holeY_m;
            r = Math.Sqrt(rx * rx + ry * ry);
            phi = Math.Atan2(ry, rx);

            // Convert velocity (dx, dy) to polar components
            dr = (rx * dx + ry * dy) / r;
            dphi = (rx * dy - ry * dx) / (r * r);

            // Compute conserved E like in C++ version
            double f = 1.0 - Form1.r_s / r;
            double dt_dλ = Math.Sqrt((dr * dr) / (f * f) + (r * r * dphi * dphi) / f);
            E = f * dt_dλ;

            // Store initial trail point
            trail.Add(new PointF(
                (float)(x / Form1.metersPerPixel),
                (float)(y / Form1.metersPerPixel)
            ));
        }

        public void Step(double dt)
        {
            if (IsActive)
            {
                // RK4 step
                double[] y0 = { r, phi, dr, dphi };
                double[] k1 = Geodesic(y0);
                double[] k2 = Geodesic(Add(y0, k1, dt / 2));
                double[] k3 = Geodesic(Add(y0, k2, dt / 2));
                double[] k4 = Geodesic(Add(y0, k3, dt));

                for (int i = 0; i < 4; i++)
                    y0[i] += (dt / 6.0) * (k1[i] + 2 * k2[i] + 2 * k3[i] + k4[i]);

                r = y0[0];
                phi = y0[1];
                dr = y0[2];
                dphi = y0[3];

                // Update Cartesian
                x = Form1.holeX_m + r * Math.Cos(phi);
                y = Form1.holeY_m + r * Math.Sin(phi);

                // Store trail
                trail.Add(new PointF(
                    (float)(x / Form1.metersPerPixel),
                    (float)(y / Form1.metersPerPixel)
                ));

                if (r <= Form1.r_s)
                {
                    IsActive = false;
                    return;
                }

                if (trail.Count > 500)
                    trail.RemoveAt(0);

                // Out-of-bounds check
                float px = (float)(x / Form1.metersPerPixel);
                float py = (float)(y / Form1.metersPerPixel);
                if (px < -10 || px > Form1.screenWidth + 10 ||
                    py < -10 || py > Form1.screenHeight + 10)
                    IsActive = false;
            }
        }

        /// <summary>
        /// Geodesic RHS equivalent to geodesicRHS() in your C++ version.
        /// y = [r, phi, dr, dphi]
        /// </summary>
        private double[] Geodesic(double[] y)
        {
            double r = y[0];
            double dr = y[2];
            double dphi = y[3];

            double rs = Form1.r_s;
            double f = 1.0 - rs / r;

            // dr/dλ and dφ/dλ are just current velocities
            double d_r = dr;
            double d_phi = dphi;

            // dt/dλ = E / f
            double dt_dλ = E / f;

            // Schwarzschild null geodesics
            double d2r =
                -(rs / (2.0 * r * r)) * f * (dt_dλ * dt_dλ)
                + (rs / (2.0 * r * r * f)) * (dr * dr)
                + (r - rs) * (dphi * dphi);

            double d2phi = -2.0 * dr * dphi / r;

            return new double[] { d_r, d_phi, d2r, d2phi };
        }

        private double[] Add(double[] a, double[] b, double scale)
        {
            return new double[]
            {
                a[0] + b[0] * scale,
                a[1] + b[1] * scale,
                a[2] + b[2] * scale,
                a[3] + b[3] * scale
            };
        }

        public void Draw(Graphics g)
        {
            float px = (float)(x / Form1.metersPerPixel);
            float py = (float)(y / Form1.metersPerPixel);
            if (!float.IsFinite(px) || !float.IsFinite(py)) return;
            if (px < -10 || px > Form1.screenWidth + 10 ||
                py < -10 || py > Form1.screenHeight + 10) return;

            g.FillEllipse(Brushes.White, px - 2, py - 2, 4, 4);
        }

        public void DrawTrail(Graphics g)
        {
            for (int i = 1; i < trail.Count; i++)
            {
                double alpha = (double)i / (trail.Count - 1);
                if (alpha < 0.05) alpha = 0.05;
                using (var p = new Pen(Color.FromArgb((int)(alpha * 255), 255, 255, 255), 1f))
                {
                    g.DrawLine(p, trail[i - 1], trail[i]);
                }
            }
        }
    }
}
