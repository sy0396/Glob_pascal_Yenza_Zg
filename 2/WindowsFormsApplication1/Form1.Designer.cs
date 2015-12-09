namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
         this.richTextBox1 = new System.Windows.Forms.RichTextBox();
         this.button1 = new System.Windows.Forms.Button();
         this.button2 = new System.Windows.Forms.Button();
         this.richTextBox2 = new System.Windows.Forms.RichTextBox();
         this.label2 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // richTextBox1
         // 
         this.richTextBox1.Location = new System.Drawing.Point(12, 12);
         this.richTextBox1.Name = "richTextBox1";
         this.richTextBox1.Size = new System.Drawing.Size(365, 336);
         this.richTextBox1.TabIndex = 0;
         this.richTextBox1.Text = "";
         // 
         // button1
         // 
         this.button1.Location = new System.Drawing.Point(439, 12);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(112, 23);
         this.button1.TabIndex = 1;
         this.button1.Text = "Открыть файл";
         this.button1.UseVisualStyleBackColor = true;
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // button2
         // 
         this.button2.Location = new System.Drawing.Point(593, 12);
         this.button2.Name = "button2";
         this.button2.Size = new System.Drawing.Size(112, 23);
         this.button2.TabIndex = 2;
         this.button2.Text = "Анализировать";
         this.button2.UseVisualStyleBackColor = true;
         this.button2.Click += new System.EventHandler(this.button2_Click);
         // 
         // richTextBox2
         // 
         this.richTextBox2.Location = new System.Drawing.Point(383, 54);
         this.richTextBox2.Name = "richTextBox2";
         this.richTextBox2.Size = new System.Drawing.Size(451, 294);
         this.richTextBox2.TabIndex = 3;
         this.richTextBox2.Text = "";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(489, 169);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(0, 13);
         this.label2.TabIndex = 5;
         this.label2.Visible = false;
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(383, 38);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(35, 13);
         this.label3.TabIndex = 6;
         this.label3.Text = "Спен:";
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(846, 360);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.richTextBox2);
         this.Controls.Add(this.button2);
         this.Controls.Add(this.button1);
         this.Controls.Add(this.richTextBox1);
         this.Name = "Form1";
         this.Text = "СПЕНЧИК ПАСКАЛЬЧИК АНАЛЬЗИНЧИК";
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

