using Billionaires.Universal.Mvvm;

namespace Billionaires.Universal.Models
{
    public class Name : ViewModelBase
    {
        private string _sort;
        private string _last;
        private string _full;

        public string Full
        {
            get { return _full; }
            set { Set(ref _full, value); }
        }

        public string Last
        {
            get { return _last; }
            set { Set(ref _last, value); }
        }

        public string Sort
        {
            get { return _sort; }
            set { Set(ref _sort, value); }
        }
    }
}