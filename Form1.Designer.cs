namespace blackhole;

partial class Form1
{
    private const int buttonWidth = 100, buttonHeight = 30;
    private System.Windows.Forms.Button btnNormal;
    private System.Windows.Forms.Button btnSingleRay;
    private System.Windows.Forms.Button btnPointSource;

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
        this.btnNormal.Click += new System.EventHandler(this.btnNormal_Click);
        // 
        // btnSingleRay
        // 
        this.btnSingleRay.Location = new System.Drawing.Point(Form1.screenWidth / 2 - 0 * buttonWidth, Form1.screenHeight - 2 * buttonHeight);
        this.btnSingleRay.Name = "btnSingleRay";
        this.btnSingleRay.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
        this.btnSingleRay.TabIndex = 1;
        this.btnSingleRay.Text = "Single Ray";
        this.btnSingleRay.UseVisualStyleBackColor = true;
        this.btnSingleRay.Click += new System.EventHandler(this.btnSingleRay_Click);
        // 
        // btnPointSource
        // 
        this.btnPointSource.Location = new System.Drawing.Point(Form1.screenWidth / 2 + 2 * buttonWidth, Form1.screenHeight - 2 * buttonHeight);
        this.btnPointSource.Name = "btnPointSource";
        this.btnPointSource.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
        this.btnPointSource.TabIndex = 2;
        this.btnPointSource.Text = "Point Source";
        this.btnPointSource.UseVisualStyleBackColor = true;
        this.btnPointSource.Click += new System.EventHandler(this.btnPointSource_Click);
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 600);
        this.Controls.Add(this.btnPointSource);
        this.Controls.Add(this.btnSingleRay);
        this.Controls.Add(this.btnNormal);
        this.Name = "Form1";
        this.Text = "Black Hole Simulation";
        this.ResumeLayout(false);
    }

    #endregion
}
