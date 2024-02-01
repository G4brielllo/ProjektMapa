using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Logika interakcji dla klasy Window2.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        private QuizWindow quizWindow;
        public MainMenu()
        {
            InitializeComponent();
            quizWindow = new QuizWindow();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            QuizWindow quizWindow = new QuizWindow();
            quizWindow.Show();
            hideMainMenu();
           
        }
        protected override void OnClosed(EventArgs e)
        {
            Application.Current.Shutdown();
            base.OnClosed(e);
        }
        private void scoreBoardButton_Click(object sender, RoutedEventArgs e)
        {
            Scoreboard scoreboardWindow = new Scoreboard();
            scoreboardWindow.Show();
            hideMainMenu();
        }
        private void hideMainMenu()
        {
            this.Hide();
        }
    }
}
