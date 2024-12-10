namespace quizGame
{
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();

            //Form
            this.Size = new Size(600, 800);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Text = "Quiz game";

        }
    }
}
