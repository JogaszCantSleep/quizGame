using Microsoft.VisualBasic.Devices;
using System.ComponentModel;
using System.Diagnostics;
using System.Media;
using System.Numerics;

namespace quizGame
{
    public partial class MainForm : Form
    {
        private int seconds;
        private Label lblCountdown;
        private System.Windows.Forms.Timer timer1;

        private class Question
        {
            public string Text { get; set; }
            public string[] Answers { get; set; }
            public int CorrectAnswerIndex { get; set; }

            public Question(string text, string[] answers, int correctAnswerIndex)
            {
                Text = text;
                Answers = answers;
                CorrectAnswerIndex = correctAnswerIndex;
            }
        }

        //Random backgrounds
        private List<Image> randomBackgrounds = new List<Image>
        {
            Image.FromFile(Path.Combine(Application.StartupPath, "pics", "randomBackgrounds", "possibleBackground_1.jpg")),
            Image.FromFile(Path.Combine(Application.StartupPath, "pics", "randomBackgrounds", "possibleBackground_2.jpg")),
            Image.FromFile(Path.Combine(Application.StartupPath, "pics", "randomBackgrounds", "possibleBackground_3.jpg")),
            Image.FromFile(Path.Combine(Application.StartupPath, "pics", "randomBackgrounds", "possibleBackground_4.jpg")),
            Image.FromFile(Path.Combine(Application.StartupPath, "pics", "randomBackgrounds", "possibleBackground_5.jpg")),
            Image.FromFile(Path.Combine(Application.StartupPath, "pics", "randomBackgrounds", "possibleBackground_6.jpg")),
            Image.FromFile(Path.Combine(Application.StartupPath, "pics", "randomBackgrounds", "possibleBackground_7.jpg"))
        };

        private List<Question> questions;
        private List<Question> remainingQuestions;
        private int questionIndex;
        private Question currentQuestion;
        private int wrongCounter;
        private int numOfRounds;
        private int playedRounds;
        private Label lblQuestion;
        private Label lblWrong3;
        private SoundPlayer ticking;


        public MainForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            InitializeForm();
            ShowStarterScreen();
        }

        //Form
        private void InitializeForm()
        {
            this.ClientSize = new Size(600, 800);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Text = "Quizimodo";
        }

        private void ShowStarterScreen()
        {
            //Questions
            questions = new List<Question>
            {
                new Question("What is the capital of France?", new string[] { "Berlin", "Madrid", "Paris", "Rome" }, 2),
                new Question("Which planet is known as the Red Planet?", new string[] { "Earth", "Mars", "Jupiter", "Venus" }, 1),
                new Question("What is the largest ocean on Earth?", new string[] { "Atlantic", "Indian", "Arctic", "Pacific" }, 3),
                new Question("Who developed the theory of relativity?", new string[] { "Newton", "Einstein", "Galileo", "Tesla" }, 1),
                new Question("What is the hardest natural substance on Earth?", new string[] { "Gold", "Iron", "Diamond", "Platinum" }, 2),
                new Question("What is the tallest mountain in the world?", new string[] { "Mount Everest", "K2", "Mount Kilimanjaro", "Mount Fuji" }, 0),
                new Question("Which element has the chemical symbol 'O'?", new string[] { "Oxygen", "Osmium", "Ozone", "Opium" }, 0),
                new Question("Who painted the Mona Lisa?", new string[] { "Vincent van Gogh", "Pablo Picasso", "Leonardo da Vinci", "Claude Monet" }, 2),
                new Question("What is the largest continent?", new string[] { "Africa", "Asia", "Europe", "North America" }, 1),
                new Question("Which country is the Great Barrier Reef located in?", new string[] { "Australia", "Canada", "United States", "Mexico" }, 0),
                new Question("In which year did World War II end?", new string[] { "1939", "1945", "1950", "1960" }, 1),
                new Question("Which instrument is used to measure temperature?", new string[] { "Barometer", "Thermometer", "Seismograph", "Odometer" }, 1),
                new Question("What is the chemical formula for water?", new string[] { "CO2", "H2O", "O2", "H2" }, 1),
                new Question("Which animal is known as the King of the Jungle?", new string[] { "Lion", "Elephant", "Tiger", "Bear" }, 0),
                new Question("Who is the author of 'Harry Potter'?", new string[] { "J.R.R. Tolkien", "J.K. Rowling", "George R.R. Martin", "C.S. Lewis" }, 1),
                new Question("Which city is known as the Big Apple?", new string[] { "Los Angeles", "Chicago", "New York", "Miami" }, 2),
                new Question("What is the largest desert in the world?", new string[] { "Sahara", "Kalahari", "Gobi", "Antarctic" }, 3),
                new Question("How many continents are there?", new string[] { "5", "6", "7", "8" }, 2),
                new Question("Which famous ship sank in 1912?", new string[] { "Titanic", "Olympic", "Lusitania", "Carpathia" }, 0),
                new Question("What is the currency of Japan?", new string[] { "Yuan", "Won", "Yen", "Ringgit" }, 2),
                new Question("What is the largest planet in our solar system?", new string[] { "Earth", "Mars", "Jupiter", "Saturn" }, 2),
                new Question("Which famous scientist developed the theory of evolution?", new string[] { "Isaac Newton", "Charles Darwin", "Albert Einstein", "Nikola Tesla" }, 1),
                new Question("Which ocean is the smallest?", new string[] { "Atlantic", "Indian", "Arctic", "Southern" }, 2),
                new Question("What is the capital of Canada?", new string[] { "Vancouver", "Toronto", "Ottawa", "Montreal" }, 2),
                new Question("What is the longest river in the world?", new string[] { "Amazon", "Nile", "Yangtze", "Mississippi" }, 1),
                new Question("Which country is known as the Land of the Rising Sun?", new string[] { "China", "South Korea", "Japan", "Vietnam" }, 2),
                new Question("Who was the first president of the United States?", new string[] { "George Washington", "Thomas Jefferson", "Abraham Lincoln", "John Adams" }, 0),
                new Question("Which planet is closest to the Sun?", new string[] { "Mercury", "Venus", "Earth", "Mars" }, 0),
                new Question("Which language has the most native speakers?", new string[] { "English", "Mandarin", "Spanish", "Hindi" }, 1),
                new Question("What is the largest organ in the human body?", new string[] { "Heart", "Liver", "Brain", "Skin" }, 3),
                new Question("Who discovered electricity?", new string[] { "Nikola Tesla", "Benjamin Franklin", "Thomas Edison", "Alexander Graham Bell" }, 1),
                new Question("Which planet is known for its rings?", new string[] { "Saturn", "Jupiter", "Uranus", "Neptune" }, 0),
                new Question("What is the hardest rock?", new string[] { "Granite", "Marble", "Diamond", "Quartz" }, 2),
                new Question("Which is the smallest country in the world?", new string[] { "Monaco", "Vatican City", "San Marino", "Liechtenstein" }, 1),
                new Question("Which city is famous for its canals?", new string[] { "Venice", "Amsterdam", "Paris", "Berlin" }, 0),
                new Question("What is the longest mountain range in the world?", new string[] { "Himalayas", "Rockies", "Andes", "Alps" }, 2),
                new Question("Which gas makes up most of the Earth's atmosphere?", new string[] { "Oxygen", "Carbon Dioxide", "Nitrogen", "Hydrogen" }, 2),
                new Question("Which of these is a fruit?", new string[] { "Carrot", "Lettuce", "Tomato", "Cucumber" }, 2),
                new Question("What is the capital of Italy?", new string[] { "Rome", "Venice", "Florence", "Milan" }, 0),
                new Question("Which continent is known as the 'Dark Continent'?", new string[] { "Africa", "Asia", "Europe", "South America" }, 0),
                new Question("Which animal is the largest mammal?", new string[] { "Elephant", "Blue Whale", "Giraffe", "Shark" }, 1),
                new Question("Which country invented paper?", new string[] { "China", "Egypt", "India", "Greece" }, 0),
                new Question("Which famous scientist invented the light bulb?", new string[] { "Nikola Tesla", "Thomas Edison", "Michael Faraday", "James Watt" }, 1),
                new Question("Which country is the Eiffel Tower located in?", new string[] { "France", "Italy", "Germany", "United Kingdom" }, 0),
                new Question("What is the largest island in the world?", new string[] { "Greenland", "Australia", "New Guinea", "Borneo" }, 0),
                new Question("What is the smallest bone in the human body?", new string[] { "Stapes", "Femur", "Tibia", "Radius" }, 0),
                new Question("Which sea is the saltiest?", new string[] { "Red Sea", "Caspian Sea", "Dead Sea", "Black Sea" }, 2),
                new Question("Which country is known for its pyramids?", new string[] { "Mexico", "Greece", "Egypt", "Italy" }, 2),
                new Question("Which country is the largest by area?", new string[] { "China", "United States", "Canada", "Russia" }, 3),
                new Question("Which type of tree produces acorns?", new string[] { "Pine", "Oak", "Maple", "Birch" }, 1),
                new Question("What is the most common gas in the Earth's atmosphere?", new string[] { "Carbon Dioxide", "Oxygen", "Nitrogen", "Argon" }, 2)
            };

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

            //Start button
            Button btnStart = new Button
            {
                Text = "",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Showcard Gothic", 40),
                BackColor = Color.Transparent,
                ForeColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Size = new Size(400, 150),
                Location = new Point(100, 500),
            };

            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.FlatAppearance.BorderSize = 0;

            string btnStartDefault = Path.Combine(Application.StartupPath, "pics", "btnStart", "default.png");
            btnStart.BackgroundImage = Image.FromFile(btnStartDefault);
            btnStart.BackgroundImageLayout = ImageLayout.Stretch;

            btnStart.MouseEnter += (s, e) =>
            {
                string btnStartHover = Path.Combine(Application.StartupPath, "pics", "btnStart", "hover.png");
                btnStart.BackgroundImage = Image.FromFile(btnStartHover);
                btnStart.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnStart.MouseLeave += (s, e) =>
            {
                string btnStartDefault = Path.Combine(Application.StartupPath, "pics", "btnStart", "default.png");
                btnStart.BackgroundImage = Image.FromFile(btnStartDefault);
                btnStart.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnStart.MouseDown += (s, e) =>
            {
                string btnStartClick = Path.Combine(Application.StartupPath, "pics", "btnStart", "click.png");
                btnStart.BackgroundImage = Image.FromFile(btnStartClick);
                btnStart.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnStart.MouseUp += (s, e) =>
            {
                string btnStartHover = Path.Combine(Application.StartupPath, "pics", "btnStart", "hover.png");
                btnStart.BackgroundImage = Image.FromFile(btnStartHover);
                btnStart.BackgroundImageLayout = ImageLayout.Stretch;
            };

            this.Controls.Add(lblTitle);
            this.Controls.Add(btnStart);

            btnStart.Click += roundSelect;
        }

        private async void roundSelect(object? sender, EventArgs e)
        {
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string soundFilePath = Path.Combine(basePath, "audios", "start.wav");
                SoundPlayer player = new SoundPlayer(soundFilePath);
                player.Play();
            }
            catch
            {
                MessageBox.Show("Error playing sound!");
            }

            this.Controls.Clear();

            //Round buttons
            Button btnRound5 = new Button
            {
                Text = "",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Showcard Gothic", 50),
                BackColor = Color.Transparent,
                ForeColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Size = new Size(400, 150),
                Location = new Point(100, 100),
            };

            btnRound5.FlatStyle = FlatStyle.Flat;
            btnRound5.FlatAppearance.BorderSize = 0;

            string btnRound5default = Path.Combine(Application.StartupPath, "pics", "btnRound", "round5default.png");
            btnRound5.BackgroundImage = Image.FromFile(btnRound5default);
            btnRound5.BackgroundImageLayout = ImageLayout.Stretch;

            btnRound5.MouseEnter += (s, e) =>
            {
                string btnRound5hover = Path.Combine(Application.StartupPath, "pics", "btnRound", "round5hover.png");
                btnRound5.BackgroundImage = Image.FromFile(btnRound5hover);
                btnRound5.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnRound5.MouseLeave += (s, e) =>
            {
                string btnRound5default = Path.Combine(Application.StartupPath, "pics", "btnRound", "round5default.png");
                btnRound5.BackgroundImage = Image.FromFile(btnRound5default);
                btnRound5.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnRound5.MouseDown += (s, e) =>
            {
                string btnRound5click = Path.Combine(Application.StartupPath, "pics", "btnRound", "round5click.png");
                btnRound5.BackgroundImage = Image.FromFile(btnRound5click);
                btnRound5.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnRound5.MouseUp += (s, e) =>
            {
                string btnRound5hover = Path.Combine(Application.StartupPath, "pics", "btnRound", "round5hover.png");
                btnRound5.BackgroundImage = Image.FromFile(btnRound5hover);
                btnRound5.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnRound5.Click += (s, e) => GenerateRounds(5);

            Button btnRound7 = new Button
            {
                Text = "",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Showcard Gothic", 50),
                BackColor = Color.Transparent,
                ForeColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Size = new Size(400, 150),
                Location = new Point(100, 300),
            };

            btnRound7.FlatStyle = FlatStyle.Flat;
            btnRound7.FlatAppearance.BorderSize = 0;

            string btnRound7default = Path.Combine(Application.StartupPath, "pics", "btnRound", "round7default.png");
            btnRound7.BackgroundImage = Image.FromFile(btnRound7default);
            btnRound7.BackgroundImageLayout = ImageLayout.Stretch;

            btnRound7.MouseEnter += (s, e) =>
            {
                string btnRound7hover = Path.Combine(Application.StartupPath, "pics", "btnRound", "round7hover.png");
                btnRound7.BackgroundImage = Image.FromFile(btnRound7hover);
                btnRound7.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnRound7.MouseLeave += (s, e) =>
            {
                string btnRound7default = Path.Combine(Application.StartupPath, "pics", "btnRound", "round7default.png");
                btnRound7.BackgroundImage = Image.FromFile(btnRound7default);
                btnRound7.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnRound7.MouseDown += (s, e) =>
            {
                string btnRound7click = Path.Combine(Application.StartupPath, "pics", "btnRound", "round7click.png");
                btnRound7.BackgroundImage = Image.FromFile(btnRound7click);
                btnRound7.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnRound7.MouseUp += (s, e) =>
            {
                string btnRound7hover = Path.Combine(Application.StartupPath, "pics", "btnRound", "round7hover.png");
                btnRound7.BackgroundImage = Image.FromFile(btnRound7hover);
                btnRound7.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnRound7.Click += (s, e) => GenerateRounds(7);

            Button btnRound12 = new Button
            {
                Text = "",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Showcard Gothic", 50),
                BackColor = Color.Transparent,
                ForeColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Size = new Size(400, 150),
                Location = new Point(100, 500),
            };

            btnRound12.FlatStyle = FlatStyle.Flat;
            btnRound12.FlatAppearance.BorderSize = 0;

            string btnRound12default = Path.Combine(Application.StartupPath, "pics", "btnRound", "round12default.png");
            btnRound12.BackgroundImage = Image.FromFile(btnRound12default);
            btnRound12.BackgroundImageLayout = ImageLayout.Stretch;

            btnRound12.MouseEnter += (s, e) =>
            {
                string btnRound12hover = Path.Combine(Application.StartupPath, "pics", "btnRound", "round12hover.png");
                btnRound12.BackgroundImage = Image.FromFile(btnRound12hover);
                btnRound12.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnRound12.MouseLeave += (s, e) =>
            {
                string btnRound12default = Path.Combine(Application.StartupPath, "pics", "btnRound", "round12default.png");
                btnRound12.BackgroundImage = Image.FromFile(btnRound12default);
                btnRound12.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnRound12.MouseDown += (s, e) =>
            {
                string btnRound12click = Path.Combine(Application.StartupPath, "pics", "btnRound", "round12click.png");
                btnRound12.BackgroundImage = Image.FromFile(btnRound12click);
                btnRound12.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnRound12.MouseUp += (s, e) =>
            {
                string btnRound12hover = Path.Combine(Application.StartupPath, "pics", "btnRound", "round12hover.png");
                btnRound12.BackgroundImage = Image.FromFile(btnRound12hover);
                btnRound12.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnRound12.Click += (s, e) => GenerateRounds(12);

            this.Controls.Add(btnRound5);
            this.Controls.Add(btnRound7);
            this.Controls.Add(btnRound12);
        }

        private void GenerateRounds(int numOfRounds)
        {
            //Generating rounds, nulling some variables and filling remainingQuestions List
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string soundFilePath = Path.Combine(basePath, "audios", "round.wav");
                SoundPlayer player = new SoundPlayer(soundFilePath);
                player.PlaySync();
            }
            catch
            {
                MessageBox.Show("Error playing sound!");
            }
            playedRounds = 0;
            wrongCounter = 0;
            this.numOfRounds = numOfRounds;
            this.Controls.Clear();
            remainingQuestions = new List<Question>(questions);
            Game();
        }

        private async void Game()
        {
            if (wrongCounter == 3)
            {
                lblWrong3.Text = "X";
                lblQuestion.Text = "you've got 3 incorrect points!\nGame Over!";
                await Task.Delay(1);
                try
                {
                    string basePath = AppDomain.CurrentDomain.BaseDirectory;
                    string soundFilePath = Path.Combine(basePath, "audios", "game_over.wav");
                    SoundPlayer player = new SoundPlayer(soundFilePath);
                    player.Play();
                }
                catch
                {
                    MessageBox.Show("Error playing sound!");
                }
                await Task.Delay(5000);
                this.Controls.Clear();
                ShowStarterScreen();
                return;
            }

            if (playedRounds == numOfRounds)
            {
                lblQuestion.Text = "You Won!";
                await Task.Delay(1);
                try
                {
                    string basePath = AppDomain.CurrentDomain.BaseDirectory;
                    string soundFilePath = Path.Combine(basePath, "audios", "win.wav");
                    SoundPlayer player = new SoundPlayer(soundFilePath);
                    player.Play();
                }
                catch
                {
                    MessageBox.Show("Error playing sound!");
                }
                await Task.Delay(5000);
                this.Controls.Clear();
                ShowStarterScreen();
                return;
            }

            this.Controls.Clear();

            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string soundFilePath = Path.Combine(basePath, "audios", "time_passing.wav");
                ticking = new SoundPlayer(soundFilePath);
                ticking.Play();
            }
            catch
            {
                MessageBox.Show("Error playing sound!");
            }

            seconds = 10;
            playedRounds += 1;

            //Progress
            Label lblProgress = new Label
            {
                Text = $"{playedRounds}/{numOfRounds}",
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Black,
                Cursor = Cursors.Hand,
                Font = new Font("Showcard Gothic", 30),
                Size = new Size(100, 60),
                Location = new Point(500, 0),
                Tag = 0
            };

            lblProgress.FlatStyle = FlatStyle.Flat;

            this.Controls.Add(lblProgress);

            //Wrong counters
            Label lblWrong1 = new Label
            {
                Text = "",
                Font = new Font("Showcard Gothic", 60),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Red,
                BackColor = Color.Transparent,
                Size = new Size(100, 100),
                Location = new Point(100, 350),
                Padding = new Padding(4, 2, 0, 0)
            };

            lblWrong1.FlatStyle = FlatStyle.Flat;

            string wrong1ImagePath = Path.Combine(Application.StartupPath, "pics", "wrong.png");
            lblWrong1.BackgroundImage = Image.FromFile(wrong1ImagePath);
            lblWrong1.BackgroundImageLayout = ImageLayout.Stretch;

            this.Controls.Add(lblWrong1);

            Label lblWrong2 = new Label
            {
                Text = "",
                Font = new Font("Showcard Gothic", 60),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Red,
                BackColor = Color.Transparent,
                Size = new Size(100, 100),
                Location = new Point(250, 350),
                Padding = new Padding(4, 2, 0, 0)
            };

            lblWrong2.FlatStyle = FlatStyle.Flat;

            string wrong2ImagePath = Path.Combine(Application.StartupPath, "pics", "wrong.png");
            lblWrong2.BackgroundImage = Image.FromFile(wrong2ImagePath);
            lblWrong2.BackgroundImageLayout = ImageLayout.Stretch;

            this.Controls.Add(lblWrong2);

            lblWrong3 = new Label
            {
                Text = "",
                Font = new Font("Showcard Gothic", 60),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Red,
                BackColor = Color.Transparent,
                Size = new Size(100, 100),
                Location = new Point(400, 350),
                Padding = new Padding(4, 2, 0, 0)
            };

            lblWrong3.FlatStyle = FlatStyle.Flat;

            string wrong3ImagePath = Path.Combine(Application.StartupPath, "pics", "wrong.png");
            lblWrong3.BackgroundImage = Image.FromFile(wrong3ImagePath);
            lblWrong3.BackgroundImageLayout = ImageLayout.Stretch;

            this.Controls.Add(lblWrong3);

            if (wrongCounter >= 1) lblWrong1.Text = "X";
            if (wrongCounter >= 2) lblWrong2.Text = "X";

            ChangeBackground();

            //Adding the Timer
            lblCountdown = new Label
            {
                Font = new Font("Showcard Gothic", 30),
                Location = new Point(250, 0),
                Size = new Size(100, 80),
                TextAlign = ContentAlignment.TopCenter,
                ForeColor = Color.Black,
                BackColor = Color.Transparent,
                Text = seconds.ToString("D2")
            };

            lblCountdown.FlatStyle = FlatStyle.Flat;

            string timerImagePath = Path.Combine(Application.StartupPath, "pics", "clock.png");
            lblCountdown.BackgroundImage = Image.FromFile(timerImagePath);
            lblCountdown.BackgroundImageLayout = ImageLayout.Stretch;

            Controls.Add(lblCountdown);

            timer1 = new System.Windows.Forms.Timer
            {
                Interval = 1000
            };
            timer1.Tick += Timer1_Tick;
            timer1.Start();

            //Whole Game
            Random rand = new Random();
            questionIndex = rand.Next(remainingQuestions.Count);
            currentQuestion = remainingQuestions[questionIndex];

            //Adding the Question
            lblQuestion = new Label
            {
                Text = currentQuestion.Text,
                Font = new Font("Showcard Gothic", 20),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black,
                BackColor = Color.Transparent,
                Size = new Size(500, 200),
                Location = new Point(50, 100),
                Padding = new Padding(10, 10, 10, 10)
            };

            lblCountdown.FlatStyle = FlatStyle.Flat;

            string questionImagePath = Path.Combine(Application.StartupPath, "pics", "question.png");
            lblQuestion.BackgroundImage = Image.FromFile(questionImagePath);
            lblQuestion.BackgroundImageLayout = ImageLayout.Stretch;

            this.Controls.Add(lblQuestion);

            // Answer buttons
            Button btnAnswer1 = new Button
            {
                Text = currentQuestion.Answers[0],
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Font = new Font("Showcard Gothic", 14),
                Size = new Size(200, 100),
                Location = new Point(50, 500),
                Tag = 0
            };

            btnAnswer1.FlatStyle = FlatStyle.Flat;
            btnAnswer1.FlatAppearance.BorderSize = 0;

            string btnAnswerDefault = Path.Combine(Application.StartupPath, "pics", "btnAnswer", "default.png");
            btnAnswer1.BackgroundImage = Image.FromFile(btnAnswerDefault);
            btnAnswer1.BackgroundImageLayout = ImageLayout.Stretch;

            btnAnswer1.MouseEnter += (s, e) =>
            {
                string btnAnswerHover = Path.Combine(Application.StartupPath, "pics", "btnAnswer", "hover.png");
                btnAnswer1.BackgroundImage = Image.FromFile(btnAnswerHover);
                btnAnswer1.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnAnswer1.MouseLeave += (s, e) =>
            {
                string btnAnswerDefault = Path.Combine(Application.StartupPath, "pics", "btnAnswer", "default.png");
                btnAnswer1.BackgroundImage = Image.FromFile(btnAnswerDefault);
                btnAnswer1.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnAnswer1.MouseDown += (s, e) =>
            {
                string btnAnswerClick = Path.Combine(Application.StartupPath, "pics", "btnAnswer", "click.png");
                btnAnswer1.BackgroundImage = Image.FromFile(btnAnswerClick);
                btnAnswer1.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnAnswer1.MouseUp += (s, e) =>
            {
                string btnAnswerHover = Path.Combine(Application.StartupPath, "pics", "btnAnswer", "hover.png");
                btnAnswer1.BackgroundImage = Image.FromFile(btnAnswerHover);
                btnAnswer1.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnAnswer1.Click += AnswerButton_Click;
            this.Controls.Add(btnAnswer1);

            Button btnAnswer2 = new Button
            {
                Text = currentQuestion.Answers[1],
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Font = new Font("Showcard Gothic", 14),
                Size = new Size(200, 100),
                Location = new Point(50, 650),
                Tag = 1
            };

            btnAnswer2.FlatStyle = FlatStyle.Flat;
            btnAnswer2.FlatAppearance.BorderSize = 0;

            btnAnswerDefault = Path.Combine(Application.StartupPath, "pics", "btnAnswer", "default.png");
            btnAnswer2.BackgroundImage = Image.FromFile(btnAnswerDefault);
            btnAnswer2.BackgroundImageLayout = ImageLayout.Stretch;

            btnAnswer2.MouseEnter += (s, e) =>
            {
                string btnAnswerHover = Path.Combine(Application.StartupPath, "pics", "btnAnswer", "hover.png");
                btnAnswer2.BackgroundImage = Image.FromFile(btnAnswerHover);
                btnAnswer2.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnAnswer2.MouseLeave += (s, e) =>
            {
                string btnAnswerDefault = Path.Combine(Application.StartupPath, "pics", "btnAnswer", "default.png");
                btnAnswer2.BackgroundImage = Image.FromFile(btnAnswerDefault);
                btnAnswer2.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnAnswer2.MouseDown += (s, e) =>
            {
                string btnAnswerClick = Path.Combine(Application.StartupPath, "pics", "btnAnswer", "click.png");
                btnAnswer2.BackgroundImage = Image.FromFile(btnAnswerClick);
                btnAnswer2.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnAnswer2.MouseUp += (s, e) =>
            {
                string btnAnswerHover = Path.Combine(Application.StartupPath, "pics", "btnAnswer", "hover.png");
                btnAnswer2.BackgroundImage = Image.FromFile(btnAnswerHover);
                btnAnswer2.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnAnswer2.Click += AnswerButton_Click;
            this.Controls.Add(btnAnswer2);

            Button btnAnswer3 = new Button
            {
                Text = currentQuestion.Answers[2],
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Black,
                Cursor = Cursors.Hand,
                Font = new Font("Showcard Gothic", 14),
                Size = new Size(200, 100),
                Location = new Point(350, 500),
                Tag = 2
            };

            btnAnswer3.FlatStyle = FlatStyle.Flat;
            btnAnswer3.FlatAppearance.BorderSize = 0;

            btnAnswerDefault = Path.Combine(Application.StartupPath, "pics", "btnAnswer", "default.png");
            btnAnswer3.BackgroundImage = Image.FromFile(btnAnswerDefault);
            btnAnswer3.BackgroundImageLayout = ImageLayout.Stretch;

            btnAnswer3.MouseEnter += (s, e) =>
            {
                string btnAnswerHover = Path.Combine(Application.StartupPath, "pics", "btnAnswer", "hover.png");
                btnAnswer3.BackgroundImage = Image.FromFile(btnAnswerHover);
                btnAnswer3.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnAnswer3.MouseLeave += (s, e) =>
            {
                string btnAnswerDefault = Path.Combine(Application.StartupPath, "pics", "btnAnswer", "default.png");
                btnAnswer3.BackgroundImage = Image.FromFile(btnAnswerDefault);
                btnAnswer3.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnAnswer3.MouseDown += (s, e) =>
            {
                string btnAnswerClick = Path.Combine(Application.StartupPath, "pics", "btnAnswer", "click.png");
                btnAnswer3.BackgroundImage = Image.FromFile(btnAnswerClick);
                btnAnswer3.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnAnswer3.MouseUp += (s, e) =>
            {
                string btnAnswerHover = Path.Combine(Application.StartupPath, "pics", "btnAnswer", "hover.png");
                btnAnswer3.BackgroundImage = Image.FromFile(btnAnswerHover);
                btnAnswer3.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnAnswer3.Click += AnswerButton_Click;
            this.Controls.Add(btnAnswer3);

            Button btnAnswer4 = new Button
            {
                Text = currentQuestion.Answers[3],
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Font = new Font("Showcard Gothic", 14),
                Size = new Size(200, 100),
                Location = new Point(350, 650),
                Tag = 3
            };

            btnAnswer4.FlatStyle = FlatStyle.Flat;
            btnAnswer4.FlatAppearance.BorderSize = 0;

            btnAnswerDefault = Path.Combine(Application.StartupPath, "pics", "btnAnswer", "default.png");
            btnAnswer4.BackgroundImage = Image.FromFile(btnAnswerDefault);
            btnAnswer4.BackgroundImageLayout = ImageLayout.Stretch;

            btnAnswer4.MouseEnter += (s, e) =>
            {
                string btnAnswerHover = Path.Combine(Application.StartupPath, "pics", "btnAnswer", "hover.png");
                btnAnswer4.BackgroundImage = Image.FromFile(btnAnswerHover);
                btnAnswer4.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnAnswer4.MouseLeave += (s, e) =>
            {
                string btnAnswerDefault = Path.Combine(Application.StartupPath, "pics", "btnAnswer", "default.png");
                btnAnswer4.BackgroundImage = Image.FromFile(btnAnswerDefault);
                btnAnswer4.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnAnswer4.MouseDown += (s, e) =>
            {
                string btnAnswerClick = Path.Combine(Application.StartupPath, "pics", "btnAnswer", "click.png");
                btnAnswer4.BackgroundImage = Image.FromFile(btnAnswerClick);
                btnAnswer4.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnAnswer4.MouseUp += (s, e) =>
            {
                string btnAnswerHover = Path.Combine(Application.StartupPath, "pics", "btnAnswer", "hover.png");
                btnAnswer4.BackgroundImage = Image.FromFile(btnAnswerHover);
                btnAnswer4.BackgroundImageLayout = ImageLayout.Stretch;
            };

            btnAnswer4.Click += AnswerButton_Click;
            this.Controls.Add(btnAnswer4);
        }

        private async void AnswerButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Button clickedButton = (Button)sender;
            int selectedAnswerIndex = (int)clickedButton.Tag;

            if (selectedAnswerIndex == currentQuestion.CorrectAnswerIndex)
            {
                lblQuestion.Text = "Correct!";
                try
                {
                    string basePath = AppDomain.CurrentDomain.BaseDirectory;
                    string soundFilePath = Path.Combine(basePath, "audios", "correct.wav");
                    SoundPlayer player = new SoundPlayer(soundFilePath);
                    player.Play();
                }
                catch
                {
                    MessageBox.Show("Error playing sound!");
                }
            }
            else
            {
                lblQuestion.Text = "Incorrect!";
                try
                {
                    string basePath = AppDomain.CurrentDomain.BaseDirectory;
                    string soundFilePath = Path.Combine(basePath, "audios", "wrong.wav");
                    SoundPlayer player = new SoundPlayer(soundFilePath);
                    player.Play();
                }
                catch
                {
                    MessageBox.Show("Error playing sound!");
                }
                wrongCounter += 1;
            }

            remainingQuestions.RemoveAt(questionIndex);
            await Task.Delay(3000);
            Game();
        }


        private async void Timer1_Tick(object? sender, EventArgs e)
        {
            //Handling timer
            if (seconds > 0)
            {
                seconds--;
                lblCountdown.Text = seconds.ToString("D2");
            }
            else
            {
                lblCountdown.Text = "00";
                timer1.Stop();
                try
                {
                    string basePath = AppDomain.CurrentDomain.BaseDirectory;
                    string soundFilePath = Path.Combine(basePath, "audios", "time.wav");
                    SoundPlayer player = new SoundPlayer(soundFilePath);
                    player.Play();
                }
                catch
                {
                    MessageBox.Show("Error playing sound!");
                }
                wrongCounter += 1;
                lblQuestion.Text = "Ran out of time!";
                await Task.Delay(3000);
                remainingQuestions.RemoveAt(questionIndex);
                Game();
            }
        }

        private void ChangeBackground()
        {
            Random rand = new Random();
            int randomIndex = rand.Next(randomBackgrounds.Count);
            this.BackgroundImage = randomBackgrounds[randomIndex];
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
}