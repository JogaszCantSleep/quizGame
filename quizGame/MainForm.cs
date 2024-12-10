namespace quizGame
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            InitializeForm();

            //Form starter background
            string imagePath = Path.Combine(Application.StartupPath, "pics", "startBackground.jpg");
            this.BackgroundImage = Image.FromFile(imagePath);
            this.BackgroundImageLayout = ImageLayout.Stretch;

            //Title
            Label lblTitle = new Label
            {
                Text = "quizimodo",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Showcard Gothic", 60),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Size = new Size(500, 100),
                Location = new Point(50, 100),
            };

            //Author
            Label lblAuthor = new Label
            {
                Text = "by Jogasz",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Showcard Gothic", 20),
                ForeColor = Color.Gray,
                BackColor = Color.Transparent,
                Size = new Size(200, 50),
                Location = new Point(0, 750),
            };

            //Start button
            Button btnStart = new Button
            {
                Text = "Start",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Showcard Gothic", 40),
                BackColor = Color.Black,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Size = new Size(400, 150),
                Location = new Point(100, 500),
            };

            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.FlatAppearance.BorderSize = 2;
            btnStart.FlatAppearance.BorderColor = Color.White;
            btnStart.FlatAppearance.MouseOverBackColor = Color.Gray;
            btnStart.FlatAppearance.MouseDownBackColor = Color.DarkGray;

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblAuthor);
            this.Controls.Add(btnStart);

            btnStart.Click += roundSelect;
        }

        //Form
        private void InitializeForm()
        {
            this.ClientSize = new Size(600, 800);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Text = "Quiz game";
        }

        private async void roundSelect(object? sender, EventArgs e)
        {
            this.Controls.Clear();

            // Round number options
            Button btnOption1 = new Button
            {
                Text = "5 rounds",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Showcard Gothic", 50),
                BackColor = Color.Black,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Size = new Size(400, 150),
                Location = new Point(100, 100),
            };

            btnOption1.FlatStyle = FlatStyle.Flat;
            btnOption1.FlatAppearance.BorderSize = 2;
            btnOption1.FlatAppearance.BorderColor = Color.White;
            btnOption1.FlatAppearance.MouseOverBackColor = Color.Gray;
            btnOption1.FlatAppearance.MouseDownBackColor = Color.DarkGray;
            btnOption1.Click += (s, e) => Rounds(5);

            Button btnOption2 = new Button
            {
                Text = "7 rounds",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Showcard Gothic", 50),
                BackColor = Color.Black,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Size = new Size(400, 150),
                Location = new Point(100, 300),
            };

            btnOption2.FlatStyle = FlatStyle.Flat;
            btnOption2.FlatAppearance.BorderSize = 2;
            btnOption2.FlatAppearance.BorderColor = Color.White;
            btnOption2.FlatAppearance.MouseOverBackColor = Color.Gray;
            btnOption2.FlatAppearance.MouseDownBackColor = Color.DarkGray;
            btnOption2.Click += (s, e) => Rounds(7);

            Button btnOption3 = new Button
            {
                Text = "12 rounds",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Showcard Gothic", 50),
                BackColor = Color.Black,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Size = new Size(400, 150),
                Location = new Point(100, 500),
            };

            btnOption3.FlatStyle = FlatStyle.Flat;
            btnOption3.FlatAppearance.BorderSize = 2;
            btnOption3.FlatAppearance.BorderColor = Color.White;
            btnOption3.FlatAppearance.MouseOverBackColor = Color.Gray;
            btnOption3.FlatAppearance.MouseDownBackColor = Color.DarkGray;
            btnOption3.Click += (s, e) => Rounds(12);

            this.Controls.Add(btnOption1);
            this.Controls.Add(btnOption2);
            this.Controls.Add(btnOption3);
        }
    }
}
