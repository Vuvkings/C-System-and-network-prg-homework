namespace HW1._3
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
            this.Num1 = new System.Windows.Forms.TextBox();
            this.Num2 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.CalcBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Num1
            // 
            this.Num1.Location = new System.Drawing.Point(130, 251);
            this.Num1.Name = "Num1";
            this.Num1.Size = new System.Drawing.Size(100, 22);
            this.Num1.TabIndex = 0;
            // 
            // Num2
            // 
            this.Num2.Location = new System.Drawing.Point(489, 249);
            this.Num2.Name = "Num2";
            this.Num2.Size = new System.Drawing.Size(100, 22);
            this.Num2.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(308, 249);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 2;
            // 
            // CalcBtn
            // 
            this.CalcBtn.Location = new System.Drawing.Point(251, 149);
            this.CalcBtn.Name = "CalcBtn";
            this.CalcBtn.Size = new System.Drawing.Size(209, 48);
            this.CalcBtn.TabIndex = 3;
            this.CalcBtn.Text = "Посчитать";
            this.CalcBtn.UseVisualStyleBackColor = true;
            this.CalcBtn.Click += new System.EventHandler(this.CalcBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CalcBtn);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.Num2);
            this.Controls.Add(this.Num1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Num1;
        private System.Windows.Forms.TextBox Num2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button CalcBtn;
    }
}

