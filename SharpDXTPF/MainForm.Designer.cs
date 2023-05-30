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
            btnScript = new Button();
            btnRenderSquare = new Button();
            btnRenderTwelvePronged = new Button();
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
            pibMain.MouseClick += pibMain_MouseClick;
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
            // btnScript
            // 
            btnScript.Location = new Point(1118, 45);
            btnScript.Name = "btnScript";
            btnScript.Size = new Size(151, 28);
            btnScript.TabIndex = 2;
            btnScript.Text = "Run Something";
            btnScript.UseVisualStyleBackColor = true;
            btnScript.Click += btnScript_Click;
            // 
            // btnRenderSquare
            // 
            btnRenderSquare.Location = new Point(790, 43);
            btnRenderSquare.Name = "btnRenderSquare";
            btnRenderSquare.Size = new Size(191, 32);
            btnRenderSquare.TabIndex = 3;
            btnRenderSquare.Text = "RenderSquare";
            btnRenderSquare.UseVisualStyleBackColor = true;
            btnRenderSquare.Click += btnRenderSquare_Click;
            // 
            // btnRenderTwelvePronged
            // 
            btnRenderTwelvePronged.Location = new Point(571, 41);
            btnRenderTwelvePronged.Name = "btnRenderTwelvePronged";
            btnRenderTwelvePronged.Size = new Size(191, 32);
            btnRenderTwelvePronged.TabIndex = 4;
            btnRenderTwelvePronged.Text = "Render 12 Pronged";
            btnRenderTwelvePronged.UseVisualStyleBackColor = true;
            btnRenderTwelvePronged.Click += btnRenderTwelvePronged_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1620, 1055);
            Controls.Add(btnRenderTwelvePronged);
            Controls.Add(btnRenderSquare);
            Controls.Add(btnScript);
            Controls.Add(btnRender);
            Controls.Add(pibMain);
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            KeyPress += MainForm_KeyPress;
            ((System.ComponentModel.ISupportInitialize)pibMain).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pibMain;
        private Button btnRender;
        private Button btnScript;
        private Button btnRenderSquare;
        private Button btnRenderTwelvePronged;
    }
}