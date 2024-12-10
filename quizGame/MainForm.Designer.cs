namespace quizGame
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            lblTitle = new Label();
            lblCreator = new Label();
            btnStart = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Showcard Gothic", 65F);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(42, 84);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(507, 109);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Quiz game";
            // 
            // lblCreator
            // 
            lblCreator.AutoSize = true;
            lblCreator.BackColor = Color.Transparent;
            lblCreator.Font = new Font("Showcard Gothic", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCreator.ForeColor = SystemColors.AppWorkspace;
            lblCreator.Location = new Point(216, 193);
            lblCreator.Name = "lblCreator";
            lblCreator.Size = new Size(154, 33);
            lblCreator.TabIndex = 1;
            lblCreator.Text = "by Jogasz";
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.BlueViolet;
            btnStart.Cursor = Cursors.Hand;
            btnStart.Font = new Font("Showcard Gothic", 60F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnStart.ForeColor = Color.White;
            btnStart.Location = new Point(63, 501);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(464, 154);
            btnStart.TabIndex = 2;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Teal;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(584, 761);
            Controls.Add(btnStart);
            Controls.Add(lblCreator);
            Controls.Add(lblTitle);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblCreator;
        private Button btnStart;
    }
}
