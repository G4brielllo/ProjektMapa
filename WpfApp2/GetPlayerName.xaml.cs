using System;
using System.Collections.Generic;
using System.IO;
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
    /// Logika interakcji dla klasy GetPlayerName.xaml
    /// </summary>
    public partial class GetPlayerName : Window
    {
        private ScoreboardRecord record;
        public string pathToFile = "ScoreBoardData.bin";
        public GetPlayerName(ScoreboardRecord receivedRecord)
        {
            InitializeComponent();
            record = receivedRecord;
            FillLabels();
        }
        private void FillLabels()
        {
            timeLabel.Content = record.GetTime();
            errorsLabel.Content = record.GetErrors();
        }
        private bool verifyTextBox(TextBox tb)
        {
            if (string.IsNullOrWhiteSpace(tb.Text))
                return false;
            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CloseWindowAndReturnToMenu();
        }
        private void CloseWindowAndReturnToMenu()
        {
            CloseQuiz();
            this.Close();
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (verifyTextBox(nameTextBox))
            {
                record.SetPlayerName(nameTextBox.Text);
                SaveToFile(pathToFile);

                CloseWindowAndReturnToMenu();
            }
            else { MessageBox.Show("Uzupełnij nazwę gracza!"); }
        }
        private bool FileExists(string path)
        {
            if(File.Exists(path))
                return true;
            return false;
        }
        private void SaveToFile(string path)
        {
            if (!FileExists(path))
                CreateEmptyFile(path);

            AppendToFile(path);
        }
        private void CreateEmptyFile(string path)
        {
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                using (var bw = new BinaryWriter(fs, Encoding.UTF8, false)){}
            }
        }
        private void AppendToFile(string path)
        {
            using (var fs = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                using (var bw = new BinaryWriter(fs, Encoding.UTF8, false))
                {
                    bw.Write(record.GetPlayerName());
                    bw.Write(record.GetTime());
                    bw.Write(record.GetErrors());
                }
            }
        }
    }
}
