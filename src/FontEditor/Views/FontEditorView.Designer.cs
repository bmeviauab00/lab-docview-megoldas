namespace FontEditor
{
    partial class FontEditorView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblEditedChar = new System.Windows.Forms.Label();
            this.bZoomIn = new System.Windows.Forms.Button();
            this.bZoomOut = new System.Windows.Forms.Button();
            this.bClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblEditedChar
            // 
            this.lblEditedChar.AutoSize = true;
            this.lblEditedChar.Location = new System.Drawing.Point(1, 0);
            this.lblEditedChar.Name = "lblEditedChar";
            this.lblEditedChar.Size = new System.Drawing.Size(14, 13);
            this.lblEditedChar.TabIndex = 0;
            this.lblEditedChar.Text = "A";
            // 
            // bZoomIn
            // 
            this.bZoomIn.Location = new System.Drawing.Point(93, -1);
            this.bZoomIn.Name = "bZoomIn";
            this.bZoomIn.Size = new System.Drawing.Size(20, 21);
            this.bZoomIn.TabIndex = 1;
            this.bZoomIn.Text = "+";
            this.bZoomIn.UseVisualStyleBackColor = true;
            this.bZoomIn.Click += new System.EventHandler(this.bZoomIn_Click);
            // 
            // bZoomOut
            // 
            this.bZoomOut.Location = new System.Drawing.Point(113, -1);
            this.bZoomOut.Name = "bZoomOut";
            this.bZoomOut.Size = new System.Drawing.Size(20, 21);
            this.bZoomOut.TabIndex = 2;
            this.bZoomOut.Text = "-";
            this.bZoomOut.UseVisualStyleBackColor = true;
            this.bZoomOut.Click += new System.EventHandler(this.bZoomOut_Click);
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(67, -1);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(20, 21);
            this.bClear.TabIndex = 1;
            this.bClear.Text = "c";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // FontEditorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.bZoomOut);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.bZoomIn);
            this.Controls.Add(this.lblEditedChar);
            this.Name = "FontEditorView";
            this.Size = new System.Drawing.Size(134, 125);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FontEditorView_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEditedChar;
        private System.Windows.Forms.Button bZoomIn;
        private System.Windows.Forms.Button bZoomOut;
        private System.Windows.Forms.Button bClear;
    }
}
