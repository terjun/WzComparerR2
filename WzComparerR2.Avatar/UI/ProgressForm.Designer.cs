namespace WzComparerR2.Avatar.UI
{
    partial class ProgressForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressForm));
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.pText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(13, 13);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(259, 23);
            this.pBar.TabIndex = 0;
            // 
            // pText
            // 
            this.pText.AutoSize = true;
            this.pText.Location = new System.Drawing.Point(13, 43);
            this.pText.Name = "pText";
            this.pText.Size = new System.Drawing.Size(69, 12);
            this.pText.TabIndex = 1;
            this.pText.Text = "강원기 사퇴";
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 65);
            this.ControlBox = false;
            this.Controls.Add(this.pText);
            this.Controls.Add(this.pBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressForm";
            this.ShowInTaskbar = false;
            this.Text = "저장 중";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.Label pText;
    }
}