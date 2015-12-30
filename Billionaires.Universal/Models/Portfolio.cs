using System.Collections.ObjectModel;
using Billionaires.Universal.Mvvm;

namespace Billionaires.Universal.Models
{
    public class Portfolio : ViewModelBase
    {
        private Holdings _holdings;
        //private ObservableCollection<Public> _public;
        //private ObservableCollection<Private> _private;
        //private ObservableCollection<Cash> _cash;

        public Holdings Holdings
        {
            get { return _holdings; }
            set { Set(ref _holdings, value); }
        }

        //public ObservableCollection<Public> Public
        //{
        //    get { return _public; }
        //    set { Set(ref _public, value); }
        //}

        //public ObservableCollection<Private> Private
        //{
        //    get { return _private; }
        //    set { Set(ref _private, value); }
        //}

        //public ObservableCollection<Cash> Cash
        //{
        //    get { return _cash; }
        //    set { Set(ref _cash, value); }
        //}
    }
}