namespace SharpDXTPF
{
    partial class MainForm : Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        ///         
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
            pibMain = new PictureBox();
            btnRender = new Button();
            ((System.ComponentModel.ISupportInitialize)pibMain).BeginInit();
            SuspendLayout();
            // 
            // pibMain
            // 
            pibMain.Location = new Point(1, 81);
            pibMain.Name = "pibMain";
            pibMain.Size = new Size(1621, 971);
            pibMain.TabIndex = 0;
            pibMain.TabStop = false;
            // 
            // btnRender
            // 
            btnRender.Location = new Point(1000, 43);
            btnRender.Name = "btnRender";
            btnRender.Size = new Size(73, 32);
            btnRender.TabIndex = 1;
            btnRender.Text = "Render";
            btnRender.UseVisualStyleBackColor = true;
            btnRender.Click += btnRender_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1620, 1055);
            Controls.Add(btnRender);
            Controls.Add(pibMain);
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)pibMain).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pibMain;
        private Button btnRender;
    }
}