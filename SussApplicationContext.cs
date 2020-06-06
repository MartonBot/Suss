using System;
using System.Drawing;
using System.Windows.Forms;

namespace Suss
{
    class SussApplicationContext : ApplicationContext
    {
        private Suss sussConfig;
        private NotifyIcon notifyIcon;
        public SussApplicationContext()
        {
            sussConfig = new Suss();
            MenuItem configMenuItem = new MenuItem("Suss", new EventHandler(SussConfig));
            MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));

            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new Icon("agentsmith.ico", 16, 16);
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[]
                { configMenuItem, exitMenuItem });
            notifyIcon.MouseClick += new MouseEventHandler(SussConfig);
            notifyIcon.Visible = true;
        }

        void SussConfig(object sender, EventArgs e)
        {
            // If we are already showing the window, merely focus it.
            if (sussConfig.Visible)
            {
                sussConfig.Activate();
            }
            else
            {
                sussConfig.ShowDialog();
            }
        }

        void Exit(object sender, EventArgs e)
        {
            // We must manually tidy up and remove the icon before we exit.
            // Otherwise it will be left behind until the user mouses over.
            notifyIcon.Visible = false;
            Application.Exit();
        }
    }
}
