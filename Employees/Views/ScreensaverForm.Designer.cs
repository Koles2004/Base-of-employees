namespace Employees.Views
{
    partial class ScreensaverForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.closeTimer = new System.Windows.Forms.Timer(this.components);
            this.opacityTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // closeTimer
            // 
            this.closeTimer.Enabled = true;
            this.closeTimer.Interval = 5000;
            this.closeTimer.Tick += new System.EventHandler(this.closeTimer_Tick_1);
            // 
            // opacityTimer
            // 
            this.opacityTimer.Enabled = true;
            this.opacityTimer.Interval = 1;
            this.opacityTimer.Tick += new System.EventHandler(this.opacityTimer_Tick_1);
            // 
            // ScreensaverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Employees.Properties.Resources.leader;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(200, 200);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScreensaverForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer closeTimer;
        private System.Windows.Forms.Timer opacityTimer;
    }
}