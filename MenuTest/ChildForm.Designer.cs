namespace MenuTest
{
    partial class ChildForm
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
            this.ctrlPenSizeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ctrlPenSizeLabel
            // 
            this.ctrlPenSizeLabel.AutoSize = true;
            this.ctrlPenSizeLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctrlPenSizeLabel.Location = new System.Drawing.Point(0, 254);
            this.ctrlPenSizeLabel.Name = "ctrlPenSizeLabel";
            this.ctrlPenSizeLabel.Size = new System.Drawing.Size(11, 12);
            this.ctrlPenSizeLabel.TabIndex = 3;
            this.ctrlPenSizeLabel.Text = "1";
            // 
            // ChildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.ctrlPenSizeLabel);
            this.Name = "ChildForm";
            this.Text = "MapForm";
            this.Deactivate += new System.EventHandler(this.MapForm_Deactivate);
            this.Activated += new System.EventHandler(this.MapForm_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ctrlPenSizeLabel;
    }
}