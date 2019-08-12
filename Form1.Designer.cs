namespace File_Sync
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox_Path1 = new System.Windows.Forms.TextBox();
            this.textBox_Path2 = new System.Windows.Forms.TextBox();
            this.button_Path1 = new System.Windows.Forms.Button();
            this.button_Path2 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button_Apply = new System.Windows.Forms.Button();
            this.groupBox_SelectInitialSourcePath = new System.Windows.Forms.GroupBox();
            this.radioButton_SecondPath = new System.Windows.Forms.RadioButton();
            this.radioButton_FirstPath = new System.Windows.Forms.RadioButton();
            this.button_Stop = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.groupBox_SelectInitialSourcePath.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_Path1
            // 
            this.textBox_Path1.Location = new System.Drawing.Point(189, 17);
            this.textBox_Path1.Name = "textBox_Path1";
            this.textBox_Path1.Size = new System.Drawing.Size(368, 20);
            this.textBox_Path1.TabIndex = 0;
            this.textBox_Path1.Text = "C:\\Users\\Huseyin\\Desktop\\test1";
            // 
            // textBox_Path2
            // 
            this.textBox_Path2.Location = new System.Drawing.Point(189, 43);
            this.textBox_Path2.Name = "textBox_Path2";
            this.textBox_Path2.Size = new System.Drawing.Size(368, 20);
            this.textBox_Path2.TabIndex = 1;
            this.textBox_Path2.Text = "C:\\Users\\Huseyin\\Desktop\\test2";
            // 
            // button_Path1
            // 
            this.button_Path1.Location = new System.Drawing.Point(563, 15);
            this.button_Path1.Name = "button_Path1";
            this.button_Path1.Size = new System.Drawing.Size(75, 23);
            this.button_Path1.TabIndex = 2;
            this.button_Path1.Text = "Select";
            this.button_Path1.UseVisualStyleBackColor = true;
            this.button_Path1.Click += new System.EventHandler(this.Button_Path1_Click);
            // 
            // button_Path2
            // 
            this.button_Path2.Location = new System.Drawing.Point(563, 41);
            this.button_Path2.Name = "button_Path2";
            this.button_Path2.Size = new System.Drawing.Size(75, 23);
            this.button_Path2.TabIndex = 3;
            this.button_Path2.Text = "Select";
            this.button_Path2.UseVisualStyleBackColor = true;
            this.button_Path2.Click += new System.EventHandler(this.Button_Path2_Click);
            // 
            // button_Apply
            // 
            this.button_Apply.Location = new System.Drawing.Point(12, 93);
            this.button_Apply.Name = "button_Apply";
            this.button_Apply.Size = new System.Drawing.Size(75, 23);
            this.button_Apply.TabIndex = 4;
            this.button_Apply.Text = "Apply";
            this.button_Apply.UseVisualStyleBackColor = true;
            this.button_Apply.Click += new System.EventHandler(this.Button_Apply_Click);
            // 
            // groupBox_SelectInitialSourcePath
            // 
            this.groupBox_SelectInitialSourcePath.Controls.Add(this.radioButton_SecondPath);
            this.groupBox_SelectInitialSourcePath.Controls.Add(this.radioButton_FirstPath);
            this.groupBox_SelectInitialSourcePath.Controls.Add(this.textBox_Path1);
            this.groupBox_SelectInitialSourcePath.Controls.Add(this.button_Path2);
            this.groupBox_SelectInitialSourcePath.Controls.Add(this.textBox_Path2);
            this.groupBox_SelectInitialSourcePath.Controls.Add(this.button_Path1);
            this.groupBox_SelectInitialSourcePath.Location = new System.Drawing.Point(12, 12);
            this.groupBox_SelectInitialSourcePath.Name = "groupBox_SelectInitialSourcePath";
            this.groupBox_SelectInitialSourcePath.Size = new System.Drawing.Size(657, 75);
            this.groupBox_SelectInitialSourcePath.TabIndex = 6;
            this.groupBox_SelectInitialSourcePath.TabStop = false;
            this.groupBox_SelectInitialSourcePath.Text = "Select source folder for first sync:";
            // 
            // radioButton_SecondPath
            // 
            this.radioButton_SecondPath.AutoSize = true;
            this.radioButton_SecondPath.Location = new System.Drawing.Point(146, 46);
            this.radioButton_SecondPath.Name = "radioButton_SecondPath";
            this.radioButton_SecondPath.Size = new System.Drawing.Size(14, 13);
            this.radioButton_SecondPath.TabIndex = 1;
            this.radioButton_SecondPath.UseVisualStyleBackColor = true;
            // 
            // radioButton_FirstPath
            // 
            this.radioButton_FirstPath.AutoSize = true;
            this.radioButton_FirstPath.Checked = true;
            this.radioButton_FirstPath.Location = new System.Drawing.Point(146, 20);
            this.radioButton_FirstPath.Name = "radioButton_FirstPath";
            this.radioButton_FirstPath.Size = new System.Drawing.Size(14, 13);
            this.radioButton_FirstPath.TabIndex = 0;
            this.radioButton_FirstPath.TabStop = true;
            this.radioButton_FirstPath.UseVisualStyleBackColor = true;
            // 
            // button_Stop
            // 
            this.button_Stop.Enabled = false;
            this.button_Stop.Location = new System.Drawing.Point(93, 93);
            this.button_Stop.Name = "button_Stop";
            this.button_Stop.Size = new System.Drawing.Size(75, 23);
            this.button_Stop.TabIndex = 7;
            this.button_Stop.Text = "Stop";
            this.button_Stop.UseVisualStyleBackColor = true;
            this.button_Stop.Click += new System.EventHandler(this.Button_Stop_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "FolderSync";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(687, 127);
            this.Controls.Add(this.button_Stop);
            this.Controls.Add(this.groupBox_SelectInitialSourcePath);
            this.Controls.Add(this.button_Apply);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Folder Sync";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.groupBox_SelectInitialSourcePath.ResumeLayout(false);
            this.groupBox_SelectInitialSourcePath.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Path1;
        private System.Windows.Forms.TextBox textBox_Path2;
        private System.Windows.Forms.Button button_Path1;
        private System.Windows.Forms.Button button_Path2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button_Apply;
        private System.Windows.Forms.GroupBox groupBox_SelectInitialSourcePath;
        private System.Windows.Forms.RadioButton radioButton_SecondPath;
        private System.Windows.Forms.RadioButton radioButton_FirstPath;
        private System.Windows.Forms.Button button_Stop;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

