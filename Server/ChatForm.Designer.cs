
namespace dvrat
{
    partial class ChatForm
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.message_textBox = new System.Windows.Forms.TextBox();
            this.send_button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.Black;
            this.listBox1.ForeColor = System.Drawing.Color.White;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(439, 342);
            this.listBox1.TabIndex = 0;
            // 
            // message_textBox
            // 
            this.message_textBox.BackColor = System.Drawing.Color.Black;
            this.message_textBox.ForeColor = System.Drawing.Color.White;
            this.message_textBox.Location = new System.Drawing.Point(12, 369);
            this.message_textBox.Name = "message_textBox";
            this.message_textBox.Size = new System.Drawing.Size(415, 20);
            this.message_textBox.TabIndex = 1;
            this.message_textBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // send_button1
            // 
            this.send_button1.BackColor = System.Drawing.Color.Black;
            this.send_button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.send_button1.Location = new System.Drawing.Point(12, 396);
            this.send_button1.Name = "send_button1";
            this.send_button1.Size = new System.Drawing.Size(415, 23);
            this.send_button1.TabIndex = 2;
            this.send_button1.Text = "Send";
            this.send_button1.UseVisualStyleBackColor = false;
            this.send_button1.Click += new System.EventHandler(this.send_button1_Click);
            // 
            // chat_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(439, 450);
            this.Controls.Add(this.send_button1);
            this.Controls.Add(this.message_textBox);
            this.Controls.Add(this.listBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "chat_form";
            this.Text = "chat_form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.chat_form_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.chat_form_FormClosed);
            this.Load += new System.EventHandler(this.chat_form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox message_textBox;
        private System.Windows.Forms.Button send_button1;
    }
}