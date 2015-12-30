using System.Collections.ObjectModel;
using Billionaires.Universal.Mvvm;

namespace Billionaires.Universal.Models
{
    public class Bio : ViewModelBase
    {
        private ObservableCollection<string> _body;
        private ObservableCollection<Milestone> _milestones;
        private BioStats _stats;

        public ObservableCollection<string> Body
        {
            get { return _body; }
            set
            {
                Set(ref _body, value);
                RaisePropertyChanged(nameof(BodyText));
            }
        }

        public string BodyText => string.Join("\r\n\r\n", _body);

        public ObservableCollection<Milestone> Milestones
        {
            get { return _milestones; }
            set
            {
                Set(ref _milestones, value);
            }
        }

        public BioStats Stats
        {
            get { return _stats; }
            set { Set(ref _stats, value); }
        }
    }
}