using System;
using System.Drawing;
using System.Windows.Forms;

namespace Employees.Views
{
    public partial class ScreensaverForm : Form
    {
        public ScreensaverForm()
        {
            InitializeComponent();
            ClientSize = new Size(BackgroundImage.Size.Width * 3 / 2, BackgroundImage.Size.Height * 3 / 2);
            Opacity = 0;
        }

        private void closeTimer_Tick_1(object sender, EventArgs e)
        {
            Close();
        }

        private void opacityTimer_Tick_1(object sender, EventArgs e)
        {
            if ((int)(Opacity += 0.005d) == 1)
                opacityTimer.Stop();
        }
    }
}