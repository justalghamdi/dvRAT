
namespace dvrat
{
    partial class ConsoleForm
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
            this.console_windows_richtextbox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // console_windows_richtextbox
            // 
            this.console_windows_richtextbox.BackColor = System.Drawing.Color.Black;
            this.console_windows_richtextbox.Cursor = System.Windows.Forms.Cursors.No;
            this.console_windows_richtextbox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.console_windows_richtextbox.ForeColor = System.Drawing.Color.White;
            this.console_windows_richtextbox.Location = new System.Drawing.Point(9, 5);
            this.console_windows_richtextbox.Name = "console_windows_richtextbox";
            this.console_windows_richtextbox.ReadOnly = true;
            this.console_windows_richtextbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.console_windows_richtextbox.Size = new System.Drawing.Size(701, 619);
            this.console_windows_richtextbox.TabIndex = 0;
            this.console_windows_richtextbox.Text = "";
            this.console_windows_richtextbox.WordWrap = false;
            this.console_windows_richtextbox.TextChanged += new System.EventHandler(this.console_windows_richtextbox_TextChanged);
            // 
            // console_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(717, 636);
            this.Controls.Add(this.console_windows_richtextbox);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "console_form";
            this.Text = "D E V I L CONSOLE";
            this.Load += new System.EventHandler(this.console_form_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox console_windows_richtextbox;
    }
}