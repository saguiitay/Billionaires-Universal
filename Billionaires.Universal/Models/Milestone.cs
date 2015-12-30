using Billionaires.Universal.Mvvm;

namespace Billionaires.Universal.Models
{
    public class Milestone : ViewModelBase
    {
        private int _year;
        private string _event;

        public int Year
        {
            get { return _year; }
            set { Set(ref _year, value); }
        }

        public string Event
        {
            get { return _event; }
            set { Set(ref _event, value); }
        }
    }
}