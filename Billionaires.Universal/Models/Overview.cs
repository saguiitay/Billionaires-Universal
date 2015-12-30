using System.Collections.ObjectModel;
using System.Linq;
using Billionaires.Universal.Mvvm;

namespace Billionaires.Universal.Models
{
    public class Overview : ViewModelBase
    {
        private ObservableCollection<string> _body;
        private ObservableCollection<string> _intel;

        public ObservableCollection<string> Body
        {
            get { return _body; }
            set
            {
                Set(ref _body, value);
                RaisePropertyChanged(nameof(BodyText));
            }
        }

        public string BodyText
        {
            get { return string.Join("\r\n\r\n", _body); }
        }

        public ObservableCollection<string> Intel
        {
            get { return _intel; }
            set { Set(ref _intel, value); }
        }
    }
}