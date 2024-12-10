namespace quizGame
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.Size = new Size(600, 800);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Text = "Quiz game";

            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.FlatAppearance.MouseOverBackColor = Color.DarkSlateBlue;
            btnStart.FlatAppearance.MouseDownBackColor = Color.Indigo;

            btnStart.Click += BtnStart_Click;
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            this.Hide();

            GameForm gameForm = new GameForm();
            gameForm.Show();
        }
    }
}
