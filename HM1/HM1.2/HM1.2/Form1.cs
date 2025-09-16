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

namespace HM1._2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MyProcess.StartInfo = new System.Diagnostics.
            ProcessStartInfo("Notepad.exe");

            
        }

        private void StartProcessBtn_Click(object sender, EventArgs e)
        {
            MyProcess.Start();

            //MessageBox.Show("Запущен процесс: " + MyProcess.ProcessName);

            DialogResult dialogResult = MessageBox.Show("Вы хотите принудительно завершить процесс?", "Сообщение", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    MyProcess.Kill();
                    MyProcess.WaitForExit();
                   

                        MessageBox.Show("Процесс завершился с кодом: " + MyProcess.ExitCode);
                    
                }
                catch {  }
            }
            else if (dialogResult == DialogResult.No)
            {
                try
                {
                    //MyProcess.CloseMainWindow();
                    MyProcess.WaitForExit();
                    

                        MessageBox.Show("Процесс завершился с кодом: " + MyProcess.ExitCode);
                    
                }
                catch { }
            }

            
        }
    }
}
