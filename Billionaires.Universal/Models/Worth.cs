using System.Collections.ObjectModel;
using Billionaires.Universal.Mvvm;

namespace Billionaires.Universal.Models
{
    public class Worth : ViewModelBase
    {
        private ObservableCollection<string> _body;
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
            get
            {
                if (_body == null || _body.Count == 0)
                    return "";

                return string.Join("\r\n\r\n", _body);
            }
        }
    }
}