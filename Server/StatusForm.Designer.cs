
namespace dvrat
{
    partial class StatusForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.title_label = new System.Windows.Forms.Label();
            this.recv_label = new System.Windows.Forms.Label();
            this.send_label = new System.Windows.Forms.Label();
            this.disconnect_label = new System.Windows.Forms.Label();
            this.connect_label = new System.Windows.Forms.Label();
            this.start_time_label = new System.Windows.Forms.Label();
            this.time_up_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // title_label
            // 
            this.title_label.AutoSize = true;
            this.title_label.Location = new System.Drawing.Point(110, 9);
            this.title_label.Name = "title_label";
            this.title_label.Size = new System.Drawing.Size(61, 13);
            this.title_label.TabIndex = 0;
            this.title_label.Text = "Devil R.A.T";
            // 
            // recv_label
            // 
            this.recv_label.AutoSize = true;
            this.recv_label.Location = new System.Drawing.Point(13, 44);
            this.recv_label.Name = "recv_label";
            this.recv_label.Size = new System.Drawing.Size(44, 13);
            this.recv_label.TabIndex = 1;
            this.recv_label.Text = "Recv: 0";
            // 
            // send_label
            // 
            this.send_label.AutoSize = true;
            this.send_label.Location = new System.Drawing.Point(13, 64);
            this.send_label.Name = "send_label";
            this.send_label.Size = new System.Drawing.Size(44, 13);
            this.send_label.TabIndex = 1;
            this.send_label.Text = "Send: 0";
            // 
            // disconnect_label
            // 
            this.disconnect_label.AutoSize = true;
            this.disconnect_label.Location = new System.Drawing.Point(12, 83);
            this.disconnect_label.Name = "disconnect_label";
            this.disconnect_label.Size = new System.Drawing.Size(77, 13);
            this.disconnect_label.TabIndex = 1;
            this.disconnect_label.Text = "Disconnects: 0";
            // 
            // connect_label
            // 
            this.connect_label.AutoSize = true;
            this.connect_label.Location = new System.Drawing.Point(13, 103);
            this.connect_label.Name = "connect_label";
            this.connect_label.Size = new System.Drawing.Size(65, 13);
            this.connect_label.TabIndex = 1;
            this.connect_label.Text = "Connects: 0";
            // 
            // start_time_label
            // 
            this.start_time_label.AutoSize = true;
            this.start_time_label.Location = new System.Drawing.Point(13, 122);
            this.start_time_label.Name = "start_time_label";
            this.start_time_label.Size = new System.Drawing.Size(148, 13);
            this.start_time_label.TabIndex = 1;
            this.start_time_label.Text = "Starts At: 0000/0/0 00:00:00";
            // 
            // time_up_label
            // 
            this.time_up_label.AutoSize = true;
            this.time_up_label.Location = new System.Drawing.Point(13, 141);
            this.time_up_label.Name = "time_up_label";
            this.time_up_label.Size = new System.Drawing.Size(96, 13);
            this.time_up_label.TabIndex = 1;
            this.time_up_label.Text = "Time Up: 00:00:00";
            // 
            // StatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(339, 167);
            this.Controls.Add(this.time_up_label);
            this.Controls.Add(this.start_time_label);
            this.Controls.Add(this.connect_label);
            this.Controls.Add(this.disconnect_label);
            this.Controls.Add(this.send_label);
            this.Controls.Add(this.recv_label);
            this.Controls.Add(this.title_label);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StatusForm";
            this.Text = "Devil R.A.T ";
            this.Load += new System.EventHandler(this.StatusForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title_label;
        private System.Windows.Forms.Label recv_label;
        private System.Windows.Forms.Label send_label;
        private System.Windows.Forms.Label disconnect_label;
        private System.Windows.Forms.Label connect_label;
        private System.Windows.Forms.Label start_time_label;
        private System.Windows.Forms.Label time_up_label;
    }
}