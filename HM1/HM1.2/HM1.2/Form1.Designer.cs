namespace HM1._2
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.StartProcessBtn = new System.Windows.Forms.Button();
            this.MyProcess = new System.Diagnostics.Process();
            this.SuspendLayout();
            // 
            // StartProcessBtn
            // 
            this.StartProcessBtn.Location = new System.Drawing.Point(171, 103);
            this.StartProcessBtn.Name = "StartProcessBtn";
            this.StartProcessBtn.Size = new System.Drawing.Size(372, 95);
            this.StartProcessBtn.TabIndex = 0;
            this.StartProcessBtn.Text = "StartProcess";
            this.StartProcessBtn.UseVisualStyleBackColor = true;
            this.StartProcessBtn.Click += new System.EventHandler(this.StartProcessBtn_Click);
            // 
            // MyProcess
            // 
            this.MyProcess.StartInfo.Domain = "";
            this.MyProcess.StartInfo.LoadUserProfile = false;
            this.MyProcess.StartInfo.Password = null;
            this.MyProcess.StartInfo.StandardErrorEncoding = null;
            this.MyProcess.StartInfo.StandardOutputEncoding = null;
            this.MyProcess.StartInfo.UserName = "";
            this.MyProcess.SynchronizingObject = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.StartProcessBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartProcessBtn;
        private System.Diagnostics.Process MyProcess;
    }
}

