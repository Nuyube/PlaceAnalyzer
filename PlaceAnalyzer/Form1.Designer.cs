namespace PlaceAnalyzer {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.loadStatusLabel = new System.Windows.Forms.Label();
            this.loadStatusTimer = new System.Windows.Forms.Timer(this.components);
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.logTimer = new System.Windows.Forms.Timer(this.components);
            this.recencyNUD = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.diffStatusLabel = new System.Windows.Forms.Label();
            this.diffStatusTimer = new System.Windows.Forms.Timer(this.components);
            this.diffPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.changeCountLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recencyNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(680, 62);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(597, 591);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "r/Place Analyzer";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(18, 81);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(308, 23);
            this.textBox1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Path to data";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 111);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 27);
            this.button1.TabIndex = 4;
            this.button1.Text = "Load";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.LOAD_BUTTON_CLICK);
            // 
            // loadStatusLabel
            // 
            this.loadStatusLabel.AutoSize = true;
            this.loadStatusLabel.Location = new System.Drawing.Point(97, 117);
            this.loadStatusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.loadStatusLabel.Name = "loadStatusLabel";
            this.loadStatusLabel.Size = new System.Drawing.Size(144, 15);
            this.loadStatusLabel.TabIndex = 5;
            this.loadStatusLabel.Text = "Load progress will fill here";
            // 
            // loadStatusTimer
            // 
            this.loadStatusTimer.Tick += new System.EventHandler(this.LOAD_STATUS_TICK);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(18, 144);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(655, 509);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            // 
            // logTimer
            // 
            this.logTimer.Tick += new System.EventHandler(this.LOG_TICK);
            // 
            // recencyNUD
            // 
            this.recencyNUD.Location = new System.Drawing.Point(553, 115);
            this.recencyNUD.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.recencyNUD.Name = "recencyNUD";
            this.recencyNUD.Size = new System.Drawing.Size(120, 23);
            this.recencyNUD.TabIndex = 7;
            this.recencyNUD.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(578, 97);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Diff Recency";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(446, 115);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Generate Diffs";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.CREATE_DIFFS_CLICK);
            // 
            // diffStatusLabel
            // 
            this.diffStatusLabel.AutoSize = true;
            this.diffStatusLabel.Location = new System.Drawing.Point(578, 81);
            this.diffStatusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.diffStatusLabel.Name = "diffStatusLabel";
            this.diffStatusLabel.Size = new System.Drawing.Size(61, 15);
            this.diffStatusLabel.TabIndex = 10;
            this.diffStatusLabel.Text = "Diff Status";
            // 
            // diffStatusTimer
            // 
            this.diffStatusTimer.Tick += new System.EventHandler(this.DIFF_STATUS_TICK);
            // 
            // diffPath
            // 
            this.diffPath.Location = new System.Drawing.Point(365, 54);
            this.diffPath.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.diffPath.Name = "diffPath";
            this.diffPath.Size = new System.Drawing.Size(308, 23);
            this.diffPath.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(365, 36);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 15);
            this.label6.TabIndex = 12;
            this.label6.Text = "Diff dump";
            // 
            // changeCountLabel
            // 
            this.changeCountLabel.AutoSize = true;
            this.changeCountLabel.Location = new System.Drawing.Point(295, 119);
            this.changeCountLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.changeCountLabel.Name = "changeCountLabel";
            this.changeCountLabel.Size = new System.Drawing.Size(84, 15);
            this.changeCountLabel.TabIndex = 13;
            this.changeCountLabel.Text = "Change Count";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1310, 740);
            this.Controls.Add(this.changeCountLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.diffPath);
            this.Controls.Add(this.diffStatusLabel);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.recencyNUD);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.loadStatusLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CLOSE_FORM);
            this.Load += new System.EventHandler(this.LOAD_FORM);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recencyNUD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label loadStatusLabel;
        private System.Windows.Forms.Timer loadStatusTimer;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Timer logTimer;
        private System.Windows.Forms.NumericUpDown recencyNUD;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label diffStatusLabel;
        private System.Windows.Forms.Timer diffStatusTimer;
        private System.Windows.Forms.TextBox diffPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label changeCountLabel;
    }
}

