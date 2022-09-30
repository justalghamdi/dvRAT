
namespace dvrat
{
    partial class ConfigMessageBox
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
            this.message_box_ico_checkbox = new System.Windows.Forms.CheckedListBox();
            this.title_textbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.message_textbox = new System.Windows.Forms.TextBox();
            this.send_button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // message_box_ico_checkbox
            // 
            this.message_box_ico_checkbox.BackColor = System.Drawing.Color.Black;
            this.message_box_ico_checkbox.ForeColor = System.Drawing.Color.White;
            this.message_box_ico_checkbox.FormattingEnabled = true;
            this.message_box_ico_checkbox.Items.AddRange(new object[] {
            "Error",
            "Question",
            "Warning\t",
            "Info"});
            this.message_box_ico_checkbox.Location = new System.Drawing.Point(6, 16);
            this.message_box_ico_checkbox.Name = "message_box_ico_checkbox";
            this.message_box_ico_checkbox.Size = new System.Drawing.Size(143, 64);
            this.message_box_ico_checkbox.TabIndex = 0;
            // 
            // title_textbox
            // 
            this.title_textbox.BackColor = System.Drawing.Color.Black;
            this.title_textbox.ForeColor = System.Drawing.Color.White;
            this.title_textbox.Location = new System.Drawing.Point(184, 28);
            this.title_textbox.Name = "title_textbox";
            this.title_textbox.Size = new System.Drawing.Size(119, 20);
            this.title_textbox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(181, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Title";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(181, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Message";
            // 
            // message_textbox
            // 
            this.message_textbox.BackColor = System.Drawing.Color.Black;
            this.message_textbox.ForeColor = System.Drawing.Color.White;
            this.message_textbox.Location = new System.Drawing.Point(184, 81);
            this.message_textbox.Name = "message_textbox";
            this.message_textbox.Size = new System.Drawing.Size(119, 20);
            this.message_textbox.TabIndex = 1;
            // 
            // send_button
            // 
            this.send_button.BackColor = System.Drawing.Color.Black;
            this.send_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.send_button.ForeColor = System.Drawing.Color.White;
            this.send_button.Location = new System.Drawing.Point(12, 133);
            this.send_button.Name = "send_button";
            this.send_button.Size = new System.Drawing.Size(291, 23);
            this.send_button.TabIndex = 3;
            this.send_button.Text = "send";
            this.send_button.UseVisualStyleBackColor = false;
            this.send_button.Click += new System.EventHandler(this.send_button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.message_box_ico_checkbox);
            this.groupBox1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Location = new System.Drawing.Point(20, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(155, 89);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ICO";
            // 
            // config_message_box
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(320, 168);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.send_button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.message_textbox);
            this.Controls.Add(this.title_textbox);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "config_message_box";
            this.Text = "CONFIG MESSAGE BOX";
            this.Load += new System.EventHandler(this.config_message_box_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox message_box_ico_checkbox;
        private System.Windows.Forms.TextBox title_textbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox message_textbox;
        private System.Windows.Forms.Button send_button;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}