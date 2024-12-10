using System.ComponentModel;
using System.Diagnostics;

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

            //Round buttons
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
            //Generating rounds, nulling some variables and filling remainingQuestions List
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
                await Task.Delay(5000);
                this.Controls.Clear();
                ShowStarterScreen();
                return;
            }

            this.Controls.Clear();

            seconds = 10;
            playedRounds += 1;

            //Wrong counters
            Label lblWrong1 = new Label
            {
                Text = "",
                Font = new Font("Showcard Gothic", 40),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Black,
                Size = new Size(100, 100),
                Location = new Point(100, 350),
            };

            lblWrong1.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(new Pen(Color.White, 6), 0, 0, lblWrong1.Width - 0, lblWrong1.Height - 0);
            };

            this.Controls.Add(lblWrong1);

            Label lblWrong2 = new Label
            {
                Text = "",
                Font = new Font("Showcard Gothic", 40),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Black,
                Size = new Size(100, 100),
                Location = new Point(250, 350),
            };

            lblWrong2.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(new Pen(Color.White, 6), 0, 0, lblWrong2.Width - 0, lblWrong2.Height - 0);
            };

            this.Controls.Add(lblWrong2);

            lblWrong3 = new Label
            {
                Text = "",
                Font = new Font("Showcard Gothic", 40),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Black,
                Size = new Size(100, 100),
                Location = new Point(400, 350),
            };

            lblWrong3.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(new Pen(Color.White, 6), 0, 0, lblWrong3.Width - 0, lblWrong3.Height - 0);
            };

            this.Controls.Add(lblWrong3);

            if (wrongCounter >= 1) lblWrong1.Text = "X";
            if (wrongCounter >= 2) lblWrong2.Text = "X";

            //Checking played number of rounds

            if (playedRounds > numOfRounds)
            {
                MessageBox.Show("You've answered all the questions!\nYou Won!");
                this.Controls.Clear();
                ShowStarterScreen();
                return;
            }

            ChangeBackground();

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
            Random rand = new Random();
            questionIndex = rand.Next(remainingQuestions.Count);
            currentQuestion = remainingQuestions[questionIndex];

            //Adding the Question
            lblQuestion = new Label
            {
                Text = currentQuestion.Text,
                Font = new Font("Showcard Gothic", 20),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Black,
                Size = new Size(500, 150),
                Location = new Point(50, 150),
            };

            lblQuestion.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(new Pen(Color.White, 6), 0, 0, lblQuestion.Width - 0, lblQuestion.Height - 0);
            };

            this.Controls.Add(lblQuestion);

            // Answer buttons
            Button btnAnswer1 = new Button
            {
                Text = currentQuestion.Answers[0],
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Black,
                Cursor = Cursors.Hand,
                Font = new Font("Arial", 14),
                Size = new Size(200, 100),
                Location = new Point(50, 500),
                Tag = 0
            };

            btnAnswer1.FlatStyle = FlatStyle.Flat;
            btnAnswer1.FlatAppearance.BorderSize = 2;
            btnAnswer1.FlatAppearance.BorderColor = Color.White;
            btnAnswer1.FlatAppearance.MouseOverBackColor = Color.Gray;
            btnAnswer1.FlatAppearance.MouseDownBackColor = Color.DarkGray;

            btnAnswer1.Click += AnswerButton_Click;
            this.Controls.Add(btnAnswer1);

            Button btnAnswer2 = new Button
            {
                Text = currentQuestion.Answers[1],
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Black,
                Cursor = Cursors.Hand,
                Font = new Font("Arial", 14),
                Size = new Size(200, 100),
                Location = new Point(50, 650),
                Tag = 1
            };

            btnAnswer2.FlatStyle = FlatStyle.Flat;
            btnAnswer2.FlatAppearance.BorderSize = 2;
            btnAnswer2.FlatAppearance.BorderColor = Color.White;
            btnAnswer2.FlatAppearance.MouseOverBackColor = Color.Gray;
            btnAnswer2.FlatAppearance.MouseDownBackColor = Color.DarkGray;

            btnAnswer2.Click += AnswerButton_Click;
            this.Controls.Add(btnAnswer2);

            Button btnAnswer3 = new Button
            {
                Text = currentQuestion.Answers[2],
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Black,
                Cursor = Cursors.Hand,
                Font = new Font("Arial", 14),
                Size = new Size(200, 100),
                Location = new Point(350, 500),
                Tag = 2
            };

            btnAnswer3.FlatStyle = FlatStyle.Flat;
            btnAnswer3.FlatAppearance.BorderSize = 2;
            btnAnswer3.FlatAppearance.BorderColor = Color.White;
            btnAnswer3.FlatAppearance.MouseOverBackColor = Color.Gray;
            btnAnswer3.FlatAppearance.MouseDownBackColor = Color.DarkGray;

            btnAnswer3.Click += AnswerButton_Click;
            this.Controls.Add(btnAnswer3);

            Button btnAnswer4 = new Button
            {
                Text = currentQuestion.Answers[3],
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Black,
                Cursor = Cursors.Hand,
                Font = new Font("Arial", 14),
                Size = new Size(200, 100),
                Location = new Point(350, 650),
                Tag = 3
            };
            btnAnswer4.FlatStyle = FlatStyle.Flat;
            btnAnswer4.FlatAppearance.BorderSize = 2;
            btnAnswer4.FlatAppearance.BorderColor = Color.White;
            btnAnswer4.FlatAppearance.MouseOverBackColor = Color.Gray;
            btnAnswer4.FlatAppearance.MouseDownBackColor = Color.DarkGray;

            btnAnswer4.Click += AnswerButton_Click;
            this.Controls.Add(btnAnswer4);
        }

        private async void AnswerButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Button clickedButton = (Button)sender;
            int selectedAnswerIndex = (int)clickedButton.Tag;

            // Update lblQuestion's text to indicate whether the answer is correct or incorrect
            if (selectedAnswerIndex == currentQuestion.CorrectAnswerIndex)
            {
                lblQuestion.Text = "Correct!";
            }
            else
            {
                lblQuestion.Text = "Incorrect!";
                wrongCounter += 1;
            }

            // Remove the current question from the list
            remainingQuestions.RemoveAt(questionIndex);

            // Wait for 1.5 seconds before moving to the next question
            await Task.Delay(1500);

            // Proceed to the next question or end the game
            Game();
        }


        private void Timer1_Tick(object? sender, EventArgs e)
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
                MessageBox.Show("Ran out of time!!");
                remainingQuestions.RemoveAt(questionIndex);
                wrongCounter += 1;
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