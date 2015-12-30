using Billionaires.Universal.Mvvm;

namespace Billionaires.Universal.Models
{
    public class Confidence : ViewModelBase
    {
        private int _stars;
        public int Stars
        {
            get { return _stars; }
            set { Set(ref _stars, value); }
        }
    }
}