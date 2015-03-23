namespace ReactorBreeder
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.xTb = new System.Windows.Forms.TextBox();
            this.yTb = new System.Windows.Forms.TextBox();
            this.zTb = new System.Windows.Forms.TextBox();
            this.cyclesTb = new System.Windows.Forms.TextBox();
            this.resultLbl = new System.Windows.Forms.Label();
            this.startBtn = new System.Windows.Forms.Button();
            this.resultTb = new System.Windows.Forms.TextBox();
            this.progressPb = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.sendCb = new System.Windows.Forms.CheckBox();
            this.continousCb = new System.Windows.Forms.CheckBox();
            this.timeLb = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "MaxX";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "MaxY";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "MaxZ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Cycles";
            // 
            // xTb
            // 
            this.xTb.Location = new System.Drawing.Point(80, 6);
            this.xTb.Name = "xTb";
            this.xTb.Size = new System.Drawing.Size(400, 20);
            this.xTb.TabIndex = 1;
            this.xTb.TextChanged += new System.EventHandler(this.xTb_TextChanged);
            // 
            // yTb
            // 
            this.yTb.Location = new System.Drawing.Point(80, 32);
            this.yTb.Name = "yTb";
            this.yTb.Size = new System.Drawing.Size(400, 20);
            this.yTb.TabIndex = 2;
            this.yTb.TextChanged += new System.EventHandler(this.xTb_TextChanged);
            // 
            // zTb
            // 
            this.zTb.Location = new System.Drawing.Point(80, 58);
            this.zTb.Name = "zTb";
            this.zTb.Size = new System.Drawing.Size(400, 20);
            this.zTb.TabIndex = 3;
            this.zTb.TextChanged += new System.EventHandler(this.xTb_TextChanged);
            // 
            // cyclesTb
            // 
            this.cyclesTb.Location = new System.Drawing.Point(80, 84);
            this.cyclesTb.Name = "cyclesTb";
            this.cyclesTb.Size = new System.Drawing.Size(400, 20);
            this.cyclesTb.TabIndex = 4;
            // 
            // resultLbl
            // 
            this.resultLbl.AutoSize = true;
            this.resultLbl.Location = new System.Drawing.Point(15, 118);
            this.resultLbl.Name = "resultLbl";
            this.resultLbl.Size = new System.Drawing.Size(0, 13);
            this.resultLbl.TabIndex = 2;
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(13, 135);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(467, 23);
            this.startBtn.TabIndex = 5;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // resultTb
            // 
            this.resultTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultTb.Location = new System.Drawing.Point(486, 6);
            this.resultTb.Multiline = true;
            this.resultTb.Name = "resultTb";
            this.resultTb.ReadOnly = true;
            this.resultTb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resultTb.Size = new System.Drawing.Size(381, 388);
            this.resultTb.TabIndex = 10;
            // 
            // progressPb
            // 
            this.progressPb.Location = new System.Drawing.Point(15, 165);
            this.progressPb.Name = "progressPb";
            this.progressPb.Size = new System.Drawing.Size(465, 23);
            this.progressPb.TabIndex = 5;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // sendCb
            // 
            this.sendCb.AutoSize = true;
            this.sendCb.Location = new System.Drawing.Point(18, 195);
            this.sendCb.Name = "sendCb";
            this.sendCb.Size = new System.Drawing.Size(151, 17);
            this.sendCb.TabIndex = 6;
            this.sendCb.Text = "Send best results to server";
            this.sendCb.UseVisualStyleBackColor = true;
            // 
            // continousCb
            // 
            this.continousCb.AutoSize = true;
            this.continousCb.Location = new System.Drawing.Point(18, 218);
            this.continousCb.Name = "continousCb";
            this.continousCb.Size = new System.Drawing.Size(73, 17);
            this.continousCb.TabIndex = 7;
            this.continousCb.Text = "Continuos";
            this.continousCb.UseVisualStyleBackColor = true;
            // 
            // timeLb
            // 
            this.timeLb.AutoSize = true;
            this.timeLb.Location = new System.Drawing.Point(15, 242);
            this.timeLb.Name = "timeLb";
            this.timeLb.Size = new System.Drawing.Size(174, 13);
            this.timeLb.TabIndex = 11;
            this.timeLb.Text = "Current speed: 0 cycles per second";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 406);
            this.Controls.Add(this.timeLb);
            this.Controls.Add(this.continousCb);
            this.Controls.Add(this.sendCb);
            this.Controls.Add(this.progressPb);
            this.Controls.Add(this.resultTb);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.resultLbl);
            this.Controls.Add(this.cyclesTb);
            this.Controls.Add(this.zTb);
            this.Controls.Add(this.yTb);
            this.Controls.Add(this.xTb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox xTb;
        private System.Windows.Forms.TextBox yTb;
        private System.Windows.Forms.TextBox zTb;
        private System.Windows.Forms.TextBox cyclesTb;
        private System.Windows.Forms.Label resultLbl;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.TextBox resultTb;
        private System.Windows.Forms.ProgressBar progressPb;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox sendCb;
        private System.Windows.Forms.CheckBox continousCb;
        private System.Windows.Forms.Label timeLb;
    }
}

