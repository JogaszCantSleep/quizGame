namespace quizGame
{
    partial class GameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            chkAnswer1 = new CheckBox();
            checkBox2 = new CheckBox();
            SuspendLayout();
            // 
            // chkAnswer1
            // 
            chkAnswer1.AutoSize = true;
            chkAnswer1.Font = new Font("Showcard Gothic", 25F);
            chkAnswer1.Location = new Point(12, 530);
            chkAnswer1.Name = "chkAnswer1";
            chkAnswer1.Size = new Size(234, 47);
            chkAnswer1.TabIndex = 0;
            chkAnswer1.Text = "checkBox1";
            chkAnswer1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Font = new Font("Showcard Gothic", 25F);
            checkBox2.Location = new Point(12, 702);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(236, 47);
            checkBox2.TabIndex = 1;
            checkBox2.Text = "checkBox2";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(584, 761);
            Controls.Add(checkBox2);
            Controls.Add(chkAnswer1);
            Name = "GameForm";
            Text = "GameForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox chkAnswer1;
        private CheckBox checkBox2;
    }
}