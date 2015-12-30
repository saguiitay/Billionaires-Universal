using Billionaires.Universal.Mvvm;

namespace Billionaires.Universal.Models
{
    public class BioStats : ViewModelBase
    {
        private string _family;
        private string _birth;
        private string _education;

        public string Family
        {
            get { return _family; }
            set { Set(ref _family, value); }
        }

        public string Birth
        {
            get { return _birth; }
            set { Set(ref _birth, value); }
        }

        public string Education
        {
            get { return _education; }
            set { Set(ref _education, value); }
        }
    }
}