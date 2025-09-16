using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HM1._4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void strtButton_Click(object sender, EventArgs e)
        {
            string exePath = "D:\\TOP Accademy\\учебные материалы\\Основы C# и .NET\\Системное прогрпммирование на C#\\TOP-SP\\HM1\\HM1.4\\ReadFile\\ReadFile\\bin\\Debug\\ReadFile.exe";
        
            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = exePath,
                Arguments = $"{word} {filePath.Text}", //{filePath.Text}
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false,
                StandardOutputEncoding = Encoding.UTF8

            };

            try
            {
                using (Process process = Process.Start(psi))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    MessageBox.Show($"{output}");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
