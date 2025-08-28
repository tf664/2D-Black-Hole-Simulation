namespace blackhole;

partial class Form1
{
    private const int buttonWidth = 100, buttonHeight = 30;
    private System.Windows.Forms.Button btnNormal;
    private System.Windows.Forms.Button btnSingleRay;
    private System.Windows.Forms.Button btnPointSource;

    private System.Windows.Forms.Button btnSettings;
    private System.Windows.Forms.Panel settingsPanel;
    private System.Windows.Forms.TrackBar massSlider;
    private System.Windows.Forms.Label massLabel;

    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>

    private void InitializeComponent()
    {
        this.btnNormal = new System.Windows.Forms.Button();
        this.btnSingleRay = new System.Windows.Forms.Button();
        this.btnPointSource = new System.Windows.Forms.Button();

        this.btnSettings = new System.Windows.Forms.Button();
        this.settingsPanel = new System.Windows.Forms.Panel();
        this.massSlider = new System.Windows.Forms.TrackBar();
        this.massLabel = new System.Windows.Forms.Label();

        this.SuspendLayout();

        // 
        // btnNormal
        // 
        this.btnNormal.Location = new System.Drawing.Point(Form1.screenWidth / 2 - 2 * buttonWidth, Form1.screenHeight - 2 * buttonHeight);
        this.btnNormal.Name = "btnNormal";
        this.btnNormal.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
        this.btnNormal.TabIndex = 0;
        this.btnNormal.Text = "Normal";
        this.btnNormal.UseVisualStyleBackColor = true;
        this.btnNormal.Click += new System.EventHandler(this.BtnNormal_Click);

        // 
        // btnSingleRay
        // 
        this.btnSingleRay.Location = new System.Drawing.Point(Form1.screenWidth / 2 - 0 * buttonWidth, Form1.screenHeight - 2 * buttonHeight);
        this.btnSingleRay.Name = "btnSingleRay";
        this.btnSingleRay.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
        this.btnSingleRay.TabIndex = 1;
        this.btnSingleRay.Text = "Single Ray";
        this.btnSingleRay.UseVisualStyleBackColor = true;
        this.btnSingleRay.Click += new System.EventHandler(this.BtnSingleRay_Click);

        // 
        // btnPointSource
        // 
        this.btnPointSource.Location = new System.Drawing.Point(Form1.screenWidth / 2 + 2 * buttonWidth, Form1.screenHeight - 2 * buttonHeight);
        this.btnPointSource.Name = "btnPointSource";
        this.btnPointSource.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
        this.btnPointSource.TabIndex = 2;
        this.btnPointSource.Text = "Point Source";
        this.btnPointSource.UseVisualStyleBackColor = true;
        this.btnPointSource.Click += new System.EventHandler(this.BtnPointSource_Click);

        // 
        // btnSettings
        // 
        this.btnSettings.Location = new System.Drawing.Point(10, 10);
        this.btnSettings.Name = "btnSettings";
        this.btnSettings.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
        this.btnSettings.TabIndex = 3;
        this.btnSettings.Text = "Settings";
        this.btnSettings.UseVisualStyleBackColor = true;
        this.btnSettings.Click += new System.EventHandler(this.BtnSettings_Click);

        // 
        // settingsPanel
        // 
        this.settingsPanel.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
        this.settingsPanel.Location = new System.Drawing.Point(10, 50);
        this.settingsPanel.Name = "settingsPanel";
        this.settingsPanel.Size = new System.Drawing.Size(200, 100);
        this.settingsPanel.Visible = false;

        // 
        // massSlider
        // 
        this.massSlider.Minimum = 1;
        this.massSlider.Maximum = 100;
        this.massSlider.Value = 50;
        this.massSlider.TickStyle = System.Windows.Forms.TickStyle.None;
        this.massSlider.Location = new System.Drawing.Point(10, 30);
        this.massSlider.Size = new System.Drawing.Size(180, 30);
        this.massSlider.Scroll += new System.EventHandler(this.MassSlider_Scroll);

        // 
        // massLabel
        // 
        this.massLabel.Text = "Mass scale: 50";
        this.massLabel.ForeColor = System.Drawing.Color.White;
        this.massLabel.Location = new System.Drawing.Point(10, 10);
        this.massLabel.Size = new System.Drawing.Size(180, 20);

        // Add controls into panel
        this.settingsPanel.Controls.Add(this.massSlider);
        this.settingsPanel.Controls.Add(this.massLabel);

        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 600);
        this.Controls.Add(this.btnSettings);
        this.Controls.Add(this.settingsPanel);
        this.Controls.Add(this.btnPointSource);
        this.Controls.Add(this.btnSingleRay);
        this.Controls.Add(this.btnNormal);
        this.Name = "Form1";
        this.Text = "Black Hole Simulation";
        this.ResumeLayout(false);
    }

    #endregion
}