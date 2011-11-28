namespace MenuTest
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrlTopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ctrlLeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.SuspendLayout();
            // 
            // ctrlTopToolStripPanel
            // 
            this.ctrlTopToolStripPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlTopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.ctrlTopToolStripPanel.Name = "ctrlTopToolStripPanel";
            this.ctrlTopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.ctrlTopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.ctrlTopToolStripPanel.Size = new System.Drawing.Size(354, 0);
            // 
            // ctrlLeftToolStripPanel
            // 
            this.ctrlLeftToolStripPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctrlLeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.ctrlLeftToolStripPanel.Name = "ctrlLeftToolStripPanel";
            this.ctrlLeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.ctrlLeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.ctrlLeftToolStripPanel.Size = new System.Drawing.Size(0, 246);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 246);
            this.Controls.Add(this.ctrlLeftToolStripPanel);
            this.Controls.Add(this.ctrlTopToolStripPanel);
            this.IsMdiContainer = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripPanel ctrlTopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel ctrlLeftToolStripPanel;


    }
}