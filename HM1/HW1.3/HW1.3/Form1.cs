using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;



namespace HW1._3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("+");
            comboBox1.Items.Add("-");
            comboBox1.Items.Add("*");
            comboBox1.Items.Add("/");

        }

        private void CalcBtn_Click(object sender, EventArgs e)
        {
            
            string exePath = "D:\\TOP Accademy\\учебные материалы\\Основы C# и .NET\\Системное прогрпммирование на C#\\TOP-SP\\HM1\\HW1.3\\Calculator.exe";
            
            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = exePath,
                Arguments = $"{Num1.Text} {Num2.Text} {comboBox1.Text}",
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
                    MessageBox.Show(output);
                }

            }
            catch (Exception ex) { 
            MessageBox.Show(ex.Message);    
            }
        }
    }
}
