using System.ComponentModel;
using System.Diagnostics;

namespace quizGame
{
    public partial class MainForm : Form
    {
        private int seconds = 10;
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

        private List<Question> questions;
        private List<Question> remainingQuestions;
        private Question currentQuestion;

        private int numOfRounds;

        private int playedRounds;

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
            this.Text = "Quiz game";
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

        private async void roundSelect(object? sender, EventArgs e)
        {
            this.Controls.Clear();

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
            btnOption1.Click += (s, e) => GenerateRounds(5);

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
            btnOption2.Click += (s, e) => GenerateRounds(7);

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
            btnOption3.Click += (s, e) => GenerateRounds(12);

            this.Controls.Add(btnOption1);
            this.Controls.Add(btnOption2);
            this.Controls.Add(btnOption3);
        }

        private void GenerateRounds(int numOfRounds)
        {
            playedRounds = 0;
            this.numOfRounds = numOfRounds;
            this.Controls.Clear();
            remainingQuestions = new List<Question>(questions);
            Game();
        }

        private async void Game()
        {
            this.Controls.Clear();

            playedRounds += 1;

            //Checking played number of rounds

            if (playedRounds > numOfRounds)
            {
                MessageBox.Show("Game Over! You've answered all the questions.");
                this.Controls.Clear();
                ShowStarterScreen();
                return;
            }

            //Adding the Timer

            lblCountdown = new Label
            {
                Font = new Font("Showcard Gothic", 30),
                Location = new Point(250, 0),
                Size = new Size(100, 100),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Black,
                Text = seconds.ToString("D2")
            };

            lblCountdown.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(new Pen(Color.White, 6), 0, 0, lblCountdown.Width - 0, lblCountdown.Height - 0);
            };

            Controls.Add(lblCountdown);

            timer1 = new System.Windows.Forms.Timer
            {
                Interval = 1000
            };
            timer1.Tick += Timer1_Tick;
            timer1.Start();

            //Whole Game

            for (int i = 0; i < numOfRounds; i++)
            {
                Random rand = new Random();
                int questionIndex = rand.Next(remainingQuestions.Count);
                currentQuestion = remainingQuestions[questionIndex];

                //Adding the Question

                Label lblQuestion = new Label
                {
                    Text = currentQuestion.Text,
                    Font = new Font("Showcard Gothic", 20),
                    ForeColor = Color.White,
                    BackColor = Color.Transparent,
                    Size = new Size(500, 100),
                    Location = new Point(50, 200),
                };

                this.Controls.Add(lblQuestion);

                //Adding the Answer Buttons

                Button[] answerButtons = new Button[4];
                for (int h = 0; h < 4; h++)
                {
                    answerButtons[h] = new Button
                    {
                        Text = currentQuestion.Answers[h],
                        Font = new Font("Arial", 14),
                        Size = new Size(400, 50),
                        Location = new Point(50, 300 + (h * 60)),
                        Tag = h
                    };
                    answerButtons[h].Click += (sender, e) =>
                    {
                        Button clickedButton = (Button)sender;
                        int selectedAnswerIndex = (int)clickedButton.Tag;

                        //Checking if answer is correct

                        if (selectedAnswerIndex == currentQuestion.CorrectAnswerIndex)
                        {
                            MessageBox.Show("Correct answer!");
                            remainingQuestions.RemoveAt(questionIndex);
                            Game();
                        }
                        else
                        {
                            MessageBox.Show("Incorrect answer!");
                            remainingQuestions.RemoveAt(questionIndex);
                            Game();
                        }
                    };
                    this.Controls.Add(answerButtons[h]);
                }
            }
        }

        private void Timer1_Tick(object? sender, EventArgs e)
        {
            if (seconds > 0)
            {
                seconds--;
                lblCountdown.Text = seconds.ToString("D2");
            }
            else
            {
                lblCountdown.Text = "00";
                timer1.Stop();
            }
        }
    }
}
