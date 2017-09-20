using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsApi.Properties;
using ApiWrapper;

namespace WindowsApi
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            this.Closing += Form1_Closing;
        }

        private void Form1_Closing(object sender, CancelEventArgs e)
        {
            hk.DisableKeyBoardHook();
        }

        private HookLib.HookLib hk;
        private void Form1_Load(object sender, EventArgs e)
        {
            hk = new HookLib.HookLib();
            hk.OnKeyTriggered += HkOnKeyTriggered;
            hk.KeyPressed += Hk_KeyPressed;
            hk.EnableKeyBoardHook();
            TryLoadConfig();
        }

        public void TryLoadConfig()
        {
            for (int i = 1; i <= 9; i++)
            {
                try
                {
                    var name = "txt" + i;
                    Controls.Find(name, true).First().Text = ConfigurationManager.AppSettings[name];
                }
                catch
                {
                    //ignore
                }
            }

        }

        private void Hk_KeyPressed(object sender, HookLib.KeyPressedEvent e)
        {
            var keyCode = int.Parse(e.KeyCode.ToString()) - 48;
            if (keyCode <= 9 && keyCode >= 1)
            {
                var ctrl = Controls.Find("txt" + keyCode, true).First() as TextBox;
                Keyboard.TypeThisString(ctrl.Text);
                e.Handled = true;
            }
        }

        private void HkOnKeyTriggered()
        {
            hk.Enabled = !hk.Enabled;
            if (hk.Enabled)
            {
                Text = Resources.Form1_HkOnKeyTriggered_Working;
            }
            else
            {
                Text = Resources.Form1_HkOnKeyTriggered_Pasued;
            }
            EnableControl(!hk.Enabled);
        }

        private void EnableControl(bool enabled)
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox)
                {
                    control.Enabled = enabled;
                }
            }
        }
    }
}
