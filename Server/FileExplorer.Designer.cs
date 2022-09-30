
namespace dvrat
{
    partial class FileExplorer
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
            System.Windows.Forms.ColumnHeader _name;
            System.Windows.Forms.ColumnHeader columnHeader1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileExplorer));
            this.listView1 = new System.Windows.Forms.ListView();
            this._type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.manage_files_contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.path_label = new System.Windows.Forms.Label();
            this.err_label = new System.Windows.Forms.Label();
            _name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.manage_files_contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _name
            // 
            _name.Text = "NAME";
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "NAME";
            columnHeader1.Width = 136;
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.BackColor = System.Drawing.Color.Black;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            _name,
            this._type});
            this.listView1.ContextMenuStrip = this.manage_files_contextMenuStrip1;
            this.listView1.ForeColor = System.Drawing.Color.White;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(202, 449);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listView1_DrawColumnHeader);
            this.listView1.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listView1_DrawItem);
            this.listView1.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listView1_DrawSubItem);
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            this.listView1.MouseEnter += new System.EventHandler(this.listView1_MouseEnter);
            this.listView1.MouseHover += new System.EventHandler(this.listView1_MouseHover);
            // 
            // _type
            // 
            this._type.Text = "TYPE";
            // 
            // manage_files_contextMenuStrip1
            // 
            this.manage_files_contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.newfileToolStripMenuItem,
            this.newFolderToolStripMenuItem});
            this.manage_files_contextMenuStrip1.Name = "manage_files_contextMenuStrip1";
            this.manage_files_contextMenuStrip1.Size = new System.Drawing.Size(131, 114);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.deleteToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.deleteToolStripMenuItem.Image = global::dvrat.ResourceFile.delete;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.deleteToolStripMenuItem.Text = "delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.renameToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.renameToolStripMenuItem.Image = global::dvrat.ResourceFile.rename;
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.renameToolStripMenuItem.Text = "rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.refreshToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.refreshToolStripMenuItem.Image = global::dvrat.ResourceFile.refresh;
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.refreshToolStripMenuItem.Text = "refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // newfileToolStripMenuItem
            // 
            this.newfileToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.newfileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.newfileToolStripMenuItem.Image = global::dvrat.ResourceFile.new_file;
            this.newfileToolStripMenuItem.Name = "newfileToolStripMenuItem";
            this.newfileToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.newfileToolStripMenuItem.Text = "new file";
            this.newfileToolStripMenuItem.Click += new System.EventHandler(this.newfileToolStripMenuItem_Click);
            // 
            // newFolderToolStripMenuItem
            // 
            this.newFolderToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.newFolderToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.newFolderToolStripMenuItem.Image = global::dvrat.ResourceFile.new_folder;
            this.newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
            this.newFolderToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.newFolderToolStripMenuItem.Text = "new folder";
            this.newFolderToolStripMenuItem.Click += new System.EventHandler(this.newFolderToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "unknown_file.png");
            this.imageList1.Images.SetKeyName(1, "folder.png");
            this.imageList1.Images.SetKeyName(2, "hard_drive.png");
            // 
            // listView2
            // 
            this.listView2.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView2.BackColor = System.Drawing.Color.Black;
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView2.ContextMenuStrip = this.manage_files_contextMenuStrip1;
            this.listView2.ForeColor = System.Drawing.Color.White;
            this.listView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(203, 49);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(459, 400);
            this.listView2.SmallImageList = this.imageList1;
            this.listView2.TabIndex = 1;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            this.listView2.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listView1_DrawColumnHeader);
            this.listView2.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listView1_DrawItem);
            this.listView2.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listView1_DrawSubItem);
            this.listView2.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView2_ItemSelectionChanged);
            this.listView2.SelectedIndexChanged += new System.EventHandler(this.listView2_SelectedIndexChanged);
            this.listView2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView2_MouseDoubleClick);
            this.listView2.MouseEnter += new System.EventHandler(this.listView2_MouseEnter);
            this.listView2.MouseHover += new System.EventHandler(this.listView2_MouseHover);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "TYPE";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "SIZE";
            // 
            // path_label
            // 
            this.path_label.AutoSize = true;
            this.path_label.Location = new System.Drawing.Point(207, 29);
            this.path_label.Name = "path_label";
            this.path_label.Size = new System.Drawing.Size(26, 13);
            this.path_label.TabIndex = 2;
            this.path_label.Text = "C:\\\\";
            this.path_label.Click += new System.EventHandler(this.label1_Click);
            // 
            // err_label
            // 
            this.err_label.AutoSize = true;
            this.err_label.ForeColor = System.Drawing.Color.Red;
            this.err_label.Location = new System.Drawing.Point(207, 9);
            this.err_label.Name = "err_label";
            this.err_label.Size = new System.Drawing.Size(0, 13);
            this.err_label.TabIndex = 2;
            this.err_label.Click += new System.EventHandler(this.label1_Click);
            // 
            // FileExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(665, 451);
            this.Controls.Add(this.err_label);
            this.Controls.Add(this.path_label);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.listView1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FileExplorer";
            this.Text = "File Explorer";
            this.Load += new System.EventHandler(this.file_explorer_form_Load);
            this.manage_files_contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader _type;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label path_label;
        private System.Windows.Forms.ContextMenuStrip manage_files_contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newfileToolStripMenuItem;
        private System.Windows.Forms.Label err_label;
        private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
    }
}