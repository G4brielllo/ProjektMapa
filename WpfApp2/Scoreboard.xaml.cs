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
    /// Logika interakcji dla klasy Scoreboard.xaml
    /// </summary>
    public partial class Scoreboard : Window
    {
        public string pathToFile = "ScoreBoardData.bin";
        public Scoreboard()
        {
            InitializeComponent();
            List<ScoreboardRecord> recordsFromFile = ReadFromFile(pathToFile);
            ShowRecords(recordsFromFile);
        }
        private List<ScoreboardRecord> ReadFromFile(string path) {
            List<ScoreboardRecord> tempList = new List<ScoreboardRecord>();
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (var br = new BinaryReader(fs, Encoding.UTF8, false))
                {
                    try
                    {
                        while (true)
                        {
                            ScoreboardRecord record = new ScoreboardRecord();
                            record.SetPlayerName(br.ReadString());
                            record.SetTime(br.ReadString());
                            record.SetErrors(br.ReadInt32());
                            tempList.Add(record);
                        }
                    }catch (EndOfStreamException ex){}
                }
            }
            return tempList;
        }
        private void ShowRecords(List<ScoreboardRecord> list)
        {
            foreach(var record in list)
            {
                listBox.Items.Add($"Gracz: {record.GetPlayerName()}, Czas: {record.GetTime()}, Ilość błędów: {record.GetErrors()}\n");
            }
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            MainMenu menu = Application.Current.Windows.OfType<MainMenu>().FirstOrDefault();//.SingleOrDefault();
            if (menu != null)
                menu.Show();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Hide();
        }
    }
}
