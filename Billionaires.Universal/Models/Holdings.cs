using Billionaires.Universal.Mvvm;

namespace Billionaires.Universal.Models
{
    public class Holdings : ViewModelBase
    {
        private long _public;
        private long _private;
        private long _cash;
        private long _liabilities;

        public long Public
        {
            get { return _public; }
            set { Set(ref _public, value); }
        }

        public long Private
        {
            get { return _private; }
            set { Set(ref _private, value); }
        }

        public long Cash
        {
            get { return _cash; }
            set { Set(ref _cash, value); }
        }

        public long Liabilities
        {
            get { return _liabilities; }
            set { Set(ref _liabilities, value); }
        }
    }
}