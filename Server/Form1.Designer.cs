
namespace dvrat
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.header_1 = new System.Windows.Forms.Label();
            this.exit_x_label = new System.Windows.Forms.Label();
            this.minimized_label = new System.Windows.Forms.Label();
            this.line_label = new System.Windows.Forms.Label();
            this.download_label = new System.Windows.Forms.Label();
            this.upload_label = new System.Windows.Forms.Label();
            this.connections_label = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this._flag = new System.Windows.Forms.DataGridViewImageColumn();
            this._ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._active_window = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._windows_username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._PC_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isadmin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._client_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fUCKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overWriteMBRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.messageBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oPENCDROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tASKMANEGARToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cHATToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fILEEXPLORERToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uACBYPASSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.socket_label = new System.Windows.Forms.Label();
            this.log_box = new System.Windows.Forms.ListBox();
            this.log_link_label = new System.Windows.Forms.LinkLabel();
            this.threads_label = new System.Windows.Forms.Label();
            this.builder_link_label = new System.Windows.Forms.LinkLabel();
            this.title_label = new System.Windows.Forms.Label();
            this.current_time_label = new System.Windows.Forms.Label();
            this.mem_usage_label = new System.Windows.Forms.Label();
            this.cpu_usage_label = new System.Windows.Forms.Label();
            this.selected_client_label = new System.Windows.Forms.Label();
            this.is_focus_label1 = new System.Windows.Forms.Label();
            this.about_linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // header_1
            // 
            this.header_1.BackColor = System.Drawing.Color.Red;
            this.header_1.ForeColor = System.Drawing.Color.Red;
            this.header_1.Location = new System.Drawing.Point(-10, -3);
            this.header_1.Name = "header_1";
            this.header_1.Size = new System.Drawing.Size(1021, 23);
            this.header_1.TabIndex = 0;
            this.header_1.Text = "label1";
            this.header_1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.header_1_MouseDown);
            // 
            // exit_x_label
            // 
            this.exit_x_label.AutoSize = true;
            this.exit_x_label.BackColor = System.Drawing.Color.Red;
            this.exit_x_label.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exit_x_label.Location = new System.Drawing.Point(985, -1);
            this.exit_x_label.Name = "exit_x_label";
            this.exit_x_label.Size = new System.Drawing.Size(17, 18);
            this.exit_x_label.TabIndex = 1;
            this.exit_x_label.Text = "X";
            this.exit_x_label.Click += new System.EventHandler(this.exit_x_label_Click);
            this.exit_x_label.MouseLeave += new System.EventHandler(this.exit_x_label_MouseLeave);
            this.exit_x_label.MouseHover += new System.EventHandler(this.exit_x_label_MouseHover);
            // 
            // minimized_label
            // 
            this.minimized_label.AutoSize = true;
            this.minimized_label.BackColor = System.Drawing.Color.Red;
            this.minimized_label.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimized_label.Location = new System.Drawing.Point(964, -1);
            this.minimized_label.Name = "minimized_label";
            this.minimized_label.Size = new System.Drawing.Size(16, 18);
            this.minimized_label.TabIndex = 1;
            this.minimized_label.Text = "_";
            this.minimized_label.Click += new System.EventHandler(this.minimized_label_Click);
            this.minimized_label.MouseLeave += new System.EventHandler(this.minimized_label_MouseLeave);
            this.minimized_label.MouseHover += new System.EventHandler(this.minimized_label_MouseHover);
            // 
            // line_label
            // 
            this.line_label.AutoSize = true;
            this.line_label.Location = new System.Drawing.Point(-19, 406);
            this.line_label.Name = "line_label";
            this.line_label.Size = new System.Drawing.Size(1063, 13);
            this.line_label.TabIndex = 2;
            this.line_label.Text = "_________________________________________________________________________________" +
    "________________________________________________________________________________" +
    "_______________";
            // 
            // download_label
            // 
            this.download_label.AutoSize = true;
            this.download_label.Location = new System.Drawing.Point(879, 427);
            this.download_label.Name = "download_label";
            this.download_label.Size = new System.Drawing.Size(107, 13);
            this.download_label.TabIndex = 3;
            this.download_label.Text = "Download [ 0 Bytes ]";
            // 
            // upload_label
            // 
            this.upload_label.AutoSize = true;
            this.upload_label.Location = new System.Drawing.Point(779, 427);
            this.upload_label.Name = "upload_label";
            this.upload_label.Size = new System.Drawing.Size(93, 13);
            this.upload_label.TabIndex = 3;
            this.upload_label.Text = "Upload [ 0 Bytes ]";
            // 
            // connections_label
            // 
            this.connections_label.AutoSize = true;
            this.connections_label.Location = new System.Drawing.Point(686, 427);
            this.connections_label.Name = "connections_label";
            this.connections_label.Size = new System.Drawing.Size(89, 13);
            this.connections_label.TabIndex = 3;
            this.connections_label.Text = "Connections [ 0 ]";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._flag,
            this._ip,
            this._active_window,
            this._windows_username,
            this._PC_NAME,
            this.isadmin,
            this._client_status});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(2, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1000, 398);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseMove);
            // 
            // _flag
            // 
            this._flag.HeaderText = "FLAG";
            this._flag.Name = "_flag";
            this._flag.ReadOnly = true;
            this._flag.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this._flag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this._flag.Width = 57;
            // 
            // _ip
            // 
            this._ip.HeaderText = "IP";
            this._ip.Name = "_ip";
            this._ip.ReadOnly = true;
            this._ip.Width = 42;
            // 
            // _active_window
            // 
            this._active_window.HeaderText = "Active Window";
            this._active_window.Name = "_active_window";
            this._active_window.ReadOnly = true;
            this._active_window.Width = 103;
            // 
            // _windows_username
            // 
            this._windows_username.HeaderText = "USERNAME";
            this._windows_username.Name = "_windows_username";
            this._windows_username.ReadOnly = true;
            this._windows_username.Width = 86;
            // 
            // _PC_NAME
            // 
            this._PC_NAME.HeaderText = "PC-NAME";
            this._PC_NAME.Name = "_PC_NAME";
            this._PC_NAME.ReadOnly = true;
            this._PC_NAME.Width = 77;
            // 
            // isadmin
            // 
            this.isadmin.HeaderText = "IS-ADMIN";
            this.isadmin.Name = "isadmin";
            this.isadmin.ReadOnly = true;
            this.isadmin.Width = 79;
            // 
            // _client_status
            // 
            this._client_status.HeaderText = "STATUS";
            this._client_status.Name = "_client_status";
            this._client_status.ReadOnly = true;
            this._client_status.Width = 70;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fUCKToolStripMenuItem,
            this.messageBoxToolStripMenuItem,
            this.oPENCDROMToolStripMenuItem,
            this.tASKMANEGARToolStripMenuItem,
            this.cHATToolStripMenuItem,
            this.fILEEXPLORERToolStripMenuItem,
            this.uACBYPASSToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 158);
            // 
            // fUCKToolStripMenuItem
            // 
            this.fUCKToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.fUCKToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.overWriteMBRToolStripMenuItem});
            this.fUCKToolStripMenuItem.Enabled = false;
            this.fUCKToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fUCKToolStripMenuItem.Image = global::dvrat.Resource1.FUCK;
            this.fUCKToolStripMenuItem.Name = "fUCKToolStripMenuItem";
            this.fUCKToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.fUCKToolStripMenuItem.Text = "FUCK";
            // 
            // overWriteMBRToolStripMenuItem
            // 
            this.overWriteMBRToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.overWriteMBRToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.overWriteMBRToolStripMenuItem.Image = global::dvrat.Resource1.hardware;
            this.overWriteMBRToolStripMenuItem.Name = "overWriteMBRToolStripMenuItem";
            this.overWriteMBRToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.overWriteMBRToolStripMenuItem.Text = "OverWriteMBR";
            // 
            // messageBoxToolStripMenuItem
            // 
            this.messageBoxToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.messageBoxToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.messageBoxToolStripMenuItem.Image = global::dvrat.Resource1.messagebox;
            this.messageBoxToolStripMenuItem.Name = "messageBoxToolStripMenuItem";
            this.messageBoxToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.messageBoxToolStripMenuItem.Text = "MessageBox";
            this.messageBoxToolStripMenuItem.Click += new System.EventHandler(this.messageBoxToolStripMenuItem_Click);
            // 
            // oPENCDROMToolStripMenuItem
            // 
            this.oPENCDROMToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.oPENCDROMToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.oPENCDROMToolStripMenuItem.Image = global::dvrat.Resource1.cd_rom;
            this.oPENCDROMToolStripMenuItem.Name = "oPENCDROMToolStripMenuItem";
            this.oPENCDROMToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.oPENCDROMToolStripMenuItem.Text = "OPEN CD-ROM";
            this.oPENCDROMToolStripMenuItem.Click += new System.EventHandler(this.oPENCDROMToolStripMenuItem_Click);
            // 
            // tASKMANEGARToolStripMenuItem
            // 
            this.tASKMANEGARToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.tASKMANEGARToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.tASKMANEGARToolStripMenuItem.Image = global::dvrat.Resource1.task_manager;
            this.tASKMANEGARToolStripMenuItem.Name = "tASKMANEGARToolStripMenuItem";
            this.tASKMANEGARToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.tASKMANEGARToolStripMenuItem.Text = "TASK MANAGER";
            this.tASKMANEGARToolStripMenuItem.Click += new System.EventHandler(this.tASKMANEGARToolStripMenuItem_Click);
            // 
            // cHATToolStripMenuItem
            // 
            this.cHATToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.cHATToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.cHATToolStripMenuItem.Image = global::dvrat.Resource1.CHAT;
            this.cHATToolStripMenuItem.Name = "cHATToolStripMenuItem";
            this.cHATToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.cHATToolStripMenuItem.Text = "CHAT";
            this.cHATToolStripMenuItem.Click += new System.EventHandler(this.cHATToolStripMenuItem_Click);
            // 
            // fILEEXPLORERToolStripMenuItem
            // 
            this.fILEEXPLORERToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.fILEEXPLORERToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fILEEXPLORERToolStripMenuItem.Image = global::dvrat.Resource1.file_explorer;
            this.fILEEXPLORERToolStripMenuItem.Name = "fILEEXPLORERToolStripMenuItem";
            this.fILEEXPLORERToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.fILEEXPLORERToolStripMenuItem.Text = "FILE EXPLORER";
            this.fILEEXPLORERToolStripMenuItem.Click += new System.EventHandler(this.fILEEXPLORERToolStripMenuItem_Click);
            // 
            // uACBYPASSToolStripMenuItem
            // 
            this.uACBYPASSToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.uACBYPASSToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.uACBYPASSToolStripMenuItem.Image = global::dvrat.Resource1.administrator;
            this.uACBYPASSToolStripMenuItem.Name = "uACBYPASSToolStripMenuItem";
            this.uACBYPASSToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.uACBYPASSToolStripMenuItem.Text = "UAC BY PASS";
            this.uACBYPASSToolStripMenuItem.Click += new System.EventHandler(this.uACBYPASSToolStripMenuItem_Click);
            // 
            // socket_label
            // 
            this.socket_label.AutoSize = true;
            this.socket_label.Location = new System.Drawing.Point(578, 427);
            this.socket_label.Name = "socket_label";
            this.socket_label.Size = new System.Drawing.Size(100, 13);
            this.socket_label.TabIndex = 5;
            this.socket_label.Text = "SOCKET_FD [NULL]";
            // 
            // log_box
            // 
            this.log_box.BackColor = System.Drawing.Color.Black;
            this.log_box.ForeColor = System.Drawing.Color.Lime;
            this.log_box.FormattingEnabled = true;
            this.log_box.Location = new System.Drawing.Point(1014, 1);
            this.log_box.Name = "log_box";
            this.log_box.Size = new System.Drawing.Size(349, 446);
            this.log_box.TabIndex = 6;
            // 
            // log_link_label
            // 
            this.log_link_label.AutoSize = true;
            this.log_link_label.LinkColor = System.Drawing.Color.White;
            this.log_link_label.Location = new System.Drawing.Point(30, 427);
            this.log_link_label.Name = "log_link_label";
            this.log_link_label.Size = new System.Drawing.Size(43, 13);
            this.log_link_label.TabIndex = 7;
            this.log_link_label.TabStop = true;
            this.log_link_label.Text = "[ Logs ]";
            this.log_link_label.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.log_link_label_LinkClicked);
            // 
            // threads_label
            // 
            this.threads_label.AutoSize = true;
            this.threads_label.Location = new System.Drawing.Point(485, 427);
            this.threads_label.Name = "threads_label";
            this.threads_label.Size = new System.Drawing.Size(81, 13);
            this.threads_label.TabIndex = 5;
            this.threads_label.Text = "Threads [NULL]";
            // 
            // builder_link_label
            // 
            this.builder_link_label.AutoSize = true;
            this.builder_link_label.LinkColor = System.Drawing.Color.White;
            this.builder_link_label.Location = new System.Drawing.Point(79, 427);
            this.builder_link_label.Name = "builder_link_label";
            this.builder_link_label.Size = new System.Drawing.Size(53, 13);
            this.builder_link_label.TabIndex = 7;
            this.builder_link_label.TabStop = true;
            this.builder_link_label.Text = "[ Builder ]";
            this.builder_link_label.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.builder_link_label_LinkClicked);
            // 
            // title_label
            // 
            this.title_label.AutoSize = true;
            this.title_label.BackColor = System.Drawing.Color.Red;
            this.title_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title_label.Location = new System.Drawing.Point(0, 2);
            this.title_label.Name = "title_label";
            this.title_label.Size = new System.Drawing.Size(54, 13);
            this.title_label.TabIndex = 8;
            this.title_label.Text = "dvRAT |";
            // 
            // current_time_label
            // 
            this.current_time_label.AutoSize = true;
            this.current_time_label.BackColor = System.Drawing.Color.Red;
            this.current_time_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.current_time_label.Location = new System.Drawing.Point(51, 3);
            this.current_time_label.Name = "current_time_label";
            this.current_time_label.Size = new System.Drawing.Size(92, 13);
            this.current_time_label.TabIndex = 9;
            this.current_time_label.Text = "[ 12:59:59 AM ]";
            // 
            // mem_usage_label
            // 
            this.mem_usage_label.AutoSize = true;
            this.mem_usage_label.BackColor = System.Drawing.Color.Red;
            this.mem_usage_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mem_usage_label.Location = new System.Drawing.Point(143, 3);
            this.mem_usage_label.Name = "mem_usage_label";
            this.mem_usage_label.Size = new System.Drawing.Size(134, 13);
            this.mem_usage_label.TabIndex = 9;
            this.mem_usage_label.Text = "Mem_Usage [ 999MB ]";
            // 
            // cpu_usage_label
            // 
            this.cpu_usage_label.AutoSize = true;
            this.cpu_usage_label.BackColor = System.Drawing.Color.Red;
            this.cpu_usage_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpu_usage_label.Location = new System.Drawing.Point(279, 3);
            this.cpu_usage_label.Name = "cpu_usage_label";
            this.cpu_usage_label.Size = new System.Drawing.Size(82, 13);
            this.cpu_usage_label.TabIndex = 9;
            this.cpu_usage_label.Text = "CPU [ 100% ]";
            // 
            // selected_client_label
            // 
            this.selected_client_label.AutoSize = true;
            this.selected_client_label.BackColor = System.Drawing.Color.Red;
            this.selected_client_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selected_client_label.Location = new System.Drawing.Point(362, 3);
            this.selected_client_label.Name = "selected_client_label";
            this.selected_client_label.Size = new System.Drawing.Size(136, 13);
            this.selected_client_label.TabIndex = 9;
            this.selected_client_label.Text = "SELECTED_CLIENT [ -1 ]";
            // 
            // is_focus_label1
            // 
            this.is_focus_label1.AutoSize = true;
            this.is_focus_label1.BackColor = System.Drawing.Color.Red;
            this.is_focus_label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.is_focus_label1.Location = new System.Drawing.Point(498, 3);
            this.is_focus_label1.Name = "is_focus_label1";
            this.is_focus_label1.Size = new System.Drawing.Size(105, 13);
            this.is_focus_label1.TabIndex = 9;
            this.is_focus_label1.Text = "IS_FOCUS [ true ]";
            // 
            // about_linkLabel1
            // 
            this.about_linkLabel1.AutoSize = true;
            this.about_linkLabel1.LinkColor = System.Drawing.Color.White;
            this.about_linkLabel1.Location = new System.Drawing.Point(136, 427);
            this.about_linkLabel1.Name = "about_linkLabel1";
            this.about_linkLabel1.Size = new System.Drawing.Size(50, 13);
            this.about_linkLabel1.TabIndex = 7;
            this.about_linkLabel1.TabStop = true;
            this.about_linkLabel1.Text = "[ About ]";
            this.about_linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.about_linkLabel1_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1368, 450);
            this.Controls.Add(this.is_focus_label1);
            this.Controls.Add(this.selected_client_label);
            this.Controls.Add(this.cpu_usage_label);
            this.Controls.Add(this.mem_usage_label);
            this.Controls.Add(this.current_time_label);
            this.Controls.Add(this.title_label);
            this.Controls.Add(this.about_linkLabel1);
            this.Controls.Add(this.builder_link_label);
            this.Controls.Add(this.log_link_label);
            this.Controls.Add(this.log_box);
            this.Controls.Add(this.threads_label);
            this.Controls.Add(this.socket_label);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.connections_label);
            this.Controls.Add(this.upload_label);
            this.Controls.Add(this.download_label);
            this.Controls.Add(this.line_label);
            this.Controls.Add(this.minimized_label);
            this.Controls.Add(this.exit_x_label);
            this.Controls.Add(this.header_1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "dvRAT v0.1";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.Deactivate += new System.EventHandler(this.Form1_Deactivate);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label header_1;
        private System.Windows.Forms.Label exit_x_label;
        private System.Windows.Forms.Label minimized_label;
        private System.Windows.Forms.Label line_label;
        private System.Windows.Forms.Label download_label;
        private System.Windows.Forms.Label upload_label;
        private System.Windows.Forms.Label connections_label;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label socket_label;
        private System.Windows.Forms.ListBox log_box;
        private System.Windows.Forms.LinkLabel log_link_label;
        private System.Windows.Forms.Label threads_label;
        private System.Windows.Forms.LinkLabel builder_link_label;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fUCKToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem overWriteMBRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem messageBoxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oPENCDROMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tASKMANEGARToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cHATToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fILEEXPLORERToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uACBYPASSToolStripMenuItem;
        private System.Windows.Forms.Label title_label;
        private System.Windows.Forms.Label current_time_label;
        private System.Windows.Forms.Label mem_usage_label;
        private System.Windows.Forms.Label cpu_usage_label;
        private System.Windows.Forms.Label selected_client_label;
        private System.Windows.Forms.DataGridViewImageColumn _flag;
        private System.Windows.Forms.DataGridViewTextBoxColumn _ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn _active_window;
        private System.Windows.Forms.DataGridViewTextBoxColumn _windows_username;
        private System.Windows.Forms.DataGridViewTextBoxColumn _PC_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn isadmin;
        private System.Windows.Forms.DataGridViewTextBoxColumn _client_status;
        private System.Windows.Forms.Label is_focus_label1;
        private System.Windows.Forms.LinkLabel about_linkLabel1;
    }
}

