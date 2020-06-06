using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Suss
{
    public partial class Suss : Form
    {
        private Timer timer;
        private Capture capture;

        public int DelayInSeconds { get; set; } = 20;

        public Suss()
        {
            InitializeComponent();

            // create a timer
            timer = new Timer();
            timer.Tick += new EventHandler(OnTimer);

            numericUpDown1.Value = DelayInSeconds;

            capture = new Capture();
            UpdateButtons();
            this.Icon = new System.Drawing.Icon("agentsmith.ico");
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            timer.Start();
            UpdateButtons();
        }

        public void OnTimer(object sender, EventArgs args)
        {
            try
            {
                capture.Screenshot();
            }
            catch(Win32Exception e)
            {
                capture.LogError(e.Message);
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
            UpdateButtons();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            var newValue = (int)numericUpDown1.Value;
            if (newValue > 0 && newValue < 86400)
            {
                DelayInSeconds = newValue;
                timer.Interval = DelayInSeconds * 1000;
            }
        }

        private void UpdateButtons()
        {

            startButton.Enabled = !timer.Enabled;
            stopButton.Enabled = timer.Enabled;
        }
    }
}