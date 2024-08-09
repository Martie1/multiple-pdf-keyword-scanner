namespace CV_Scanner
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelChoose = new Label();
            btnBrowse = new Button();
            textDirectory = new TextBox();
            textKeywords = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnApply = new Button();
            checkBox = new CheckBox();
            SuspendLayout();
            // 
            // labelChoose
            // 
            labelChoose.AutoSize = true;
            labelChoose.Location = new Point(38, 52);
            labelChoose.Name = "labelChoose";
            labelChoose.Size = new Size(180, 15);
            labelChoose.TabIndex = 0;
            labelChoose.Text = "Choose the location of directory.";
            labelChoose.TextAlign = ContentAlignment.BottomLeft;
            labelChoose.Click += label1_Click;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(475, 75);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 1;
            btnBrowse.Text = "Browse...";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // textDirectory
            // 
            textDirectory.Location = new Point(38, 76);
            textDirectory.Name = "textDirectory";
            textDirectory.Size = new Size(431, 23);
            textDirectory.TabIndex = 2;
            // 
            // textKeywords
            // 
            textKeywords.Location = new Point(38, 143);
            textKeywords.Name = "textKeywords";
            textKeywords.Size = new Size(431, 23);
            textKeywords.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(38, 125);
            label1.Name = "label1";
            label1.Size = new Size(87, 15);
            label1.TabIndex = 6;
            label1.Text = "Enter keywords";
            label1.TextAlign = ContentAlignment.BottomLeft;
            label1.Click += label1_Click_2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(38, 169);
            label2.Name = "label2";
            label2.Size = new Size(499, 15);
            label2.TabIndex = 7;
            label2.Text = "*Please use one word keywords and ',' as a separator. Size of letters will not impact the search ";
            label2.TextAlign = ContentAlignment.BottomLeft;
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(38, 353);
            label3.Name = "label3";
            label3.Size = new Size(370, 15);
            label3.TabIndex = 8;
            label3.Text = "*By default the program finds files with ANY of the entered keywords";
            label3.TextAlign = ContentAlignment.MiddleRight;
            label3.Click += label3_Click;
            // 
            // btnApply
            // 
            btnApply.Location = new Point(575, 346);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(90, 29);
            btnApply.TabIndex = 9;
            btnApply.Text = "Apply";
            btnApply.UseVisualStyleBackColor = true;
            btnApply.Click += btnApply_Click;
            // 
            // checkBox
            // 
            checkBox.AutoSize = true;
            checkBox.Location = new Point(38, 331);
            checkBox.Name = "checkBox";
            checkBox.Size = new Size(268, 19);
            checkBox.TabIndex = 10;
            checkBox.Text = "Find the files containing ALL of the keywords*";
            checkBox.UseVisualStyleBackColor = true;
            checkBox.CheckedChanged += checkBox_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(687, 401);
            Controls.Add(checkBox);
            Controls.Add(btnApply);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textKeywords);
            Controls.Add(textDirectory);
            Controls.Add(btnBrowse);
            Controls.Add(labelChoose);
            Name = "Form1";
            Text = "PDF Keyword Scanner";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelChoose;
        private Button btnBrowse;
        private TextBox textDirectory;
        private TextBox textKeywords;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnApply;
        private CheckBox checkBox;
    }
}
