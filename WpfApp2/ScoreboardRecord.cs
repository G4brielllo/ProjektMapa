using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class ScoreboardRecord
    {
        private string _playerName;
        private string _time;
        private int _errors;

        public ScoreboardRecord() { }
        public ScoreboardRecord(string playerName, string time, int errors)
        {
            SetPlayerName(playerName);
            SetTime(time);
            SetErrors(errors);
        }

        public string GetTime()
        {
            return _time;
        }
        public int GetErrors()
        {
            return _errors;
        }
        public string GetPlayerName()
        {
            return _playerName;
        }
        public void SetTime(string time)
        {
            _time = time;
        }
        public void SetErrors(int errors)
        {
            _errors = errors;
        }
        public void SetPlayerName(string name)
        {
            _playerName = name;
        }
    }
}
