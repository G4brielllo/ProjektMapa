using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp2
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class QuizWindow : Window
    {
        ScoreboardRecord record;
        DispatcherTimer timer;
        private int secondsElapsed = 0;
        private int currPoints = 0;
        private int currErrors = 0;
        private int errorsToSwitch = 0;
        Stack<string> counteriesStack;
        List<string> countries = new List<string>
        {
            "Albania", "Austria",
            "Białoruś", "Belgia", "Bośnia i Hercegowina", "Bułgaria",
            "Chorwacja","Czechy", "Dania", "Estonia",
            "Finlandia", "Francja", "Niemcy", "Grecja", "Węgry",
            "Islandia", "Irlandia", "Włochy", "Kosowo", "Łotwa",
            "Litwa", "Luksemburg", "Mołdawia",
            "Czarnogóra", "Holandia", "Macedonia Północna", "Norwegia",
            "Polska", "Portugalia", "Rumunia", "Rosja",
            "Serbia", "Słowacja", "Słowenia", "Hiszpania", "Szwecja", "Szwajcaria",
            "Ukraina", "Wielka Brytania",
        };

        public QuizWindow()
        {
            InitializeComponent();
            initLables();

            CreateAndStartTimer();
            ShuffleList(countries);
            counteriesStack = new Stack<string>(countries);
            FirstQuestion();
        }
        private void initLables()
        {
            initPointLable();
            initCounterLable();
            initErrorLable();
        }
        private void initPointLable()
        {
            pointsLabel.Content = currPoints;
        }
        private void initErrorLable()
        {
            errorsLabel.Content = currErrors;
        }
        private void initCounterLable()
        {
            countryLable.Content = "0";
        }
        private void NextQuestionAfterCorrect()
        {
            counteriesStack.Pop();
            if (counteriesStack.Count > 0)
                countryLable.Content = counteriesStack.ElementAt(0);
        }
        private void NextQuestionAfterWrong()
        {
            List<string> tempList = counteriesStack.ToList();
            ShuffleList(tempList);
            counteriesStack = new Stack<string>(tempList);

            countryLable.Content = counteriesStack.ElementAt(0);
        }
        private void addPoint()
        {
            pointsLabel.Content = ++currPoints;
        }
        private void addError()
        {
            errorsLabel.Content = ++currErrors;
        }
        private void FirstQuestion()
        {
            countryLable.Content = counteriesStack.ElementAt(0);
        }
        private void ShuffleList(List<string> list)
        {
            Random random = new Random();
            int n = list.Count;

            for (int i = n - 1; i > 0; i--)
            {
                int randomIndex = random.Next(0, i + 1);

                string temp = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }
        private void CorrectAnswer()
        {
            addPoint();
            NextQuestionAfterCorrect();
            errorsToSwitch = 0;
            CheckForGameFinish();

            //FinishGame(); ////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }
        private void CheckForGameFinish()
        {
            if (currPoints == countries.Count)
            {
                FinishGame();
            }
        }
        private void FinishGame()
        {
            StopTimer();

            record = new ScoreboardRecord();
            record.SetErrors(currErrors);
            record.SetTime(timeLabel.Content.ToString());
            GetPlayerName postGameWindow = new GetPlayerName(record);
            postGameWindow.ShowDialog();
        }
        private void StopTimer()
        {
            timer.Stop();
        }
        private void WrongAnswer()
        {
            addError();
            ShowWrongAnswer();
            errorsToSwitch++;
        }
        private void CheckIfErrorSwitch()
        {
            if (errorsToSwitch >= 3)
            {
                NextQuestionAfterWrong();
                errorsToSwitch = 0;
            }
        }
        private void ShowWrongAnswer()
        {
            MessageBox.Show("Zła odpowiedź!");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            secondsElapsed++;
            TimeSpan timeElapsed = TimeSpan.FromSeconds(secondsElapsed);
            string formattedTime = $"{(int)timeElapsed.TotalHours:D2}:{timeElapsed.Minutes:D2}:{timeElapsed.Seconds:D2}";
            timeLabel.Content = formattedTime;
        }
        private void CreateAndStartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Path path)
            {
                if (path.Name == "Albania" && countryLable.Content == "Albania")
                {
                    //MessageBox.Show("Kliknięto na Albania!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Austria" && countryLable.Content == "Austria")
                {
                    //MessageBox.Show("Kliknięto na Austria!");
                    path.Fill = Brushes.LightGreen;

                    CorrectAnswer();
                }
                else if (path.Name == "Belgia" && countryLable.Content == "Belgia")
                {
                    //MessageBox.Show("Kliknięto na Belgia!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Bialorus" && countryLable.Content == "Białoruś")
                {
                    //MessageBox.Show("Kliknięto na Białoruś!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "BosniaIHercegowina" && countryLable.Content == "Bośnia i Hercegowina")
                {
                    //MessageBox.Show("Kliknięto na Bośnia i Hercegowina!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Bulgaria" && countryLable.Content == "Bułgaria")
                {
                    //MessageBox.Show("Kliknięto na Bułgaria!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Chorwacja" && countryLable.Content == "Chorwacja")
                {
                    //MessageBox.Show("Kliknięto na Chorwacja!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Czarnogora" && countryLable.Content == "Czarnogóra")
                {
                    //MessageBox.Show("Kliknięto na Czarnogóra!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Czechy" && countryLable.Content == "Czechy")
                {
                    //MessageBox.Show("Kliknięto na Czechy!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if ((path.Name == "Dania1" || path.Name == "Dania2") && countryLable.Content == "Dania")
                {
                    //MessageBox.Show("Kliknięto na Dania!");
                    Dania1.Fill = Brushes.LightGreen;
                    Dania2.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Estonia" && countryLable.Content == "Estonia")
                {
                    //MessageBox.Show("Kliknięto na Estonię!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Francja" && countryLable.Content == "Francja")//elementy
                {
                    //MessageBox.Show("Kliknięto na Francja!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Finlandia" && countryLable.Content == "Finlandia")
                {
                    //MessageBox.Show("Kliknięto na Finlandia!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if ((path.Name == "Grecja1" || path.Name == "Grecja2" || path.Name == "Grecja3" || path.Name == "Grecja4" ||
                    path.Name == "Grecja5" || path.Name == "Grecja6" || path.Name == "Grecja7" || path.Name == "Grecja8") && countryLable.Content == "Grecja")//elementy
                {
                    //MessageBox.Show("Kliknięto na Grecja!");
                    Grecja1.Fill = Brushes.LightGreen;
                    Grecja2.Fill = Brushes.LightGreen;
                    Grecja3.Fill = Brushes.LightGreen;
                    Grecja4.Fill = Brushes.LightGreen;
                    Grecja5.Fill = Brushes.LightGreen;
                    Grecja6.Fill = Brushes.LightGreen;
                    Grecja7.Fill = Brushes.LightGreen;
                    Grecja8.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Hiszpania" && countryLable.Content == "Hiszpania")
                {
                    //MessageBox.Show("Kliknięto na Hiszpania!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Holandia" && countryLable.Content == "Holandia")
                {
                    //MessageBox.Show("Kliknięto na Holandia!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Islandia" && countryLable.Content == "Islandia")
                {
                    //MessageBox.Show("Kliknięto na Islandia!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Irlandia" && countryLable.Content == "Irlandia")
                {
                    //MessageBox.Show("Kliknięto na Irlandia!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Kosowo" && countryLable.Content == "Kosowo")
                {
                    //MessageBox.Show("Kliknięto na Kosowo!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Holandia" && countryLable.Content == "Holandia")
                {
                    //MessageBox.Show("Kliknięto na Holandia!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Litwa" && countryLable.Content == "Litwa")
                {
                    //MessageBox.Show("Kliknięto na Litwa!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "MacedoniaPolnocna" && countryLable.Content == "Macedonia Północna")
                {
                    //MessageBox.Show("Kliknięto na Macedonia Północna!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Moldawia" && countryLable.Content == "Mołdawia")
                {
                    //MessageBox.Show("Kliknięto na Mołdawia!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Niemcy" && countryLable.Content == "Niemcy")
                {
                    //MessageBox.Show("Kliknięto na Niemcy!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Norwegia" && countryLable.Content == "Norwegia")
                {
                    //MessageBox.Show("Kliknięto na Norwegia!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Polska" && countryLable.Content == "Polska")
                {
                    //MessageBox.Show("Kliknięto na Polska!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Portugalia" && countryLable.Content == "Portugalia")
                {
                    //MessageBox.Show("Kliknięto na Portugalia!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if ((path.Name == "Rosja1" || path.Name == "Rosja2") && countryLable.Content == "Rosja")//elementy
                {
                    //MessageBox.Show("Kliknięto na Rosja!");
                    Rosja1.Fill = Brushes.LightGreen;
                    Rosja2.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Rumunia" && countryLable.Content == "Rumunia")
                {
                    //MessageBox.Show("Kliknięto na Rumunia!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Serbia" && countryLable.Content == "Serbia")
                {
                    //MessageBox.Show("Kliknięto na Serbia!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Szwajcaria" && countryLable.Content == "Szwajcaria")
                {
                    //MessageBox.Show("Kliknięto na Szwajcaria!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Szwecja" && countryLable.Content == "Szwecja")
                {
                    //MessageBox.Show("Kliknięto na Szwecja!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Slowacja" && countryLable.Content == "Słowacja")
                {
                    //MessageBox.Show("Kliknięto na Słowacja!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Slowenia" && countryLable.Content == "Słowenia")
                {
                    //MessageBox.Show("Kliknięto na Słowenia!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Ukraina" && countryLable.Content == "Ukraina")
                {
                    //MessageBox.Show("Kliknięto na Ukraina!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if ((path.Name == "WielkaBrytania1" || path.Name == "WielkaBrytania2" || path.Name == "WielkaBrytania3" ||
                    path.Name == "WielkaBrytania4" || path.Name == "WielkaBrytania5" || path.Name == "WielkaBrytania6" ||
                    path.Name == "WielkaBrytania7" || path.Name == "WielkaBrytania8") && countryLable.Content == "Wielka Brytania")//elementy
                {
                    //MessageBox.Show("Kliknięto na Wielka Brytania!");
                    WielkaBrytania1.Fill = Brushes.LightGreen;
                    WielkaBrytania2.Fill = Brushes.LightGreen;
                    WielkaBrytania3.Fill = Brushes.LightGreen;
                    WielkaBrytania4.Fill = Brushes.LightGreen;
                    WielkaBrytania5.Fill = Brushes.LightGreen;
                    WielkaBrytania6.Fill = Brushes.LightGreen;
                    WielkaBrytania7.Fill = Brushes.LightGreen;
                    WielkaBrytania8.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Wegry" && countryLable.Content == "Węgry")
                {
                    //MessageBox.Show("Kliknięto na Węgry!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if ((path.Name == "Wlochy1" || path.Name == "Wlochy2" || path.Name == "Wlochy3") && countryLable.Content == "Włochy")//elementy
                {
                    //MessageBox.Show("Kliknięto na Włochy!");
                    Wlochy1.Fill = Brushes.LightGreen;
                    Wlochy2.Fill = Brushes.LightGreen;
                    Wlochy3.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Lotwa" && countryLable.Content == "Łotwa")
                {
                    //MessageBox.Show("Kliknięto na Łotwa!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Name == "Luksemburg" && countryLable.Content == "Luksemburg")
                {
                    //MessageBox.Show("Kliknięto na Luksemburg!");
                    path.Fill = Brushes.LightGreen;
                    CorrectAnswer();
                }
                else if (path.Fill == Brushes.LightGreen){}
                else
                {
                    WrongAnswer();
                    CheckIfErrorSwitch();
                }
            }
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            OpenMenu();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OnClosed(e);
            CloseQuiz();
            OpenMenu();
        }
        private void CloseQuiz()
        {
            QuizWindow quiz = Application.Current.Windows.OfType<QuizWindow>().FirstOrDefault();
            while (quiz != null)
            {
                quiz = Application.Current.Windows.OfType<QuizWindow>().FirstOrDefault();
                if (quiz == null)
                    break;
                quiz.Close();
            }
        }
        private void OpenMenu()
        {
            MainMenu menu = Application.Current.Windows.OfType<MainMenu>().FirstOrDefault();
            if (menu != null)
                menu.Show();
        }
    }
}
