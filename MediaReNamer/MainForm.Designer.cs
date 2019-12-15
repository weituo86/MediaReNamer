namespace MediaReNamer
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.srcText = new System.Windows.Forms.TextBox();
            this.dstText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCopy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(379, 24);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(112, 52);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "开始移动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // srcText
            // 
            this.srcText.AllowDrop = true;
            this.srcText.Location = new System.Drawing.Point(96, 40);
            this.srcText.Name = "srcText";
            this.srcText.Size = new System.Drawing.Size(233, 21);
            this.srcText.TabIndex = 1;
            this.srcText.DragDrop += new System.Windows.Forms.DragEventHandler(this.srcText_DragDrop);
            this.srcText.DragEnter += new System.Windows.Forms.DragEventHandler(this.Text_DragEnter);
            // 
            // dstText
            // 
            this.dstText.AllowDrop = true;
            this.dstText.Location = new System.Drawing.Point(96, 119);
            this.dstText.Name = "dstText";
            this.dstText.Size = new System.Drawing.Size(233, 21);
            this.dstText.TabIndex = 2;
            this.dstText.DragDrop += new System.Windows.Forms.DragEventHandler(this.dstText_DragDrop);
            this.dstText.DragEnter += new System.Windows.Forms.DragEventHandler(this.Text_DragEnter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 44);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "源文件夹";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 123);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "目标文件夹";
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(379, 102);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(112, 52);
            this.btnCopy.TabIndex = 0;
            this.btnCopy.Text = "开始复制";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 181);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dstText);
            this.Controls.Add(this.srcText);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnStart);
            this.Name = "MainForm";
            this.Text = "媒体重命名";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox srcText;
        private System.Windows.Forms.TextBox dstText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCopy;
    }
}

