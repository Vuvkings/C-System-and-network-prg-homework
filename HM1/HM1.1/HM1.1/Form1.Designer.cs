namespace HM1._1
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
            this.NotepadBtn = new System.Windows.Forms.Button();
            this.MyProcess = new System.Diagnostics.Process();
            this.eventLog1 = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.SuspendLayout();
            // 
            // NotepadBtn
            // 
            this.NotepadBtn.Location = new System.Drawing.Point(280, 108);
            this.NotepadBtn.Name = "NotepadBtn";
            this.NotepadBtn.Size = new System.Drawing.Size(228, 97);
            this.NotepadBtn.TabIndex = 0;
            this.NotepadBtn.Text = "Start Notepad";
            this.NotepadBtn.UseVisualStyleBackColor = true;
            this.NotepadBtn.Click += new System.EventHandler(this.NotepadBtn_Click);
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
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.NotepadBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button NotepadBtn;
        private System.Diagnostics.Process MyProcess;
        private System.Diagnostics.EventLog eventLog1;
    }
}

