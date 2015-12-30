using System.Collections.ObjectModel;
using Billionaires.Universal.Mvvm;
using Template10.Controls;

namespace Billionaires.Universal.Models
{
    public class PersonDetails : ViewModelBase
    {
        private Overview _overview;
        private Confidence _confidence;
        private Portfolio _portfolio;
        private ObservableItemCollection<News> _news;
        private ObservableCollection<Own> _owns;
        private Worth _worth;
        private Bio _bio;

        public Overview Overview
        {
            get { return _overview; }
            set { Set(ref _overview, value); }
        }

        public Confidence Confidence
        {
            get { return _confidence; }
            set { Set(ref _confidence, value); }
        }

        public Portfolio Portfolio
        {
            get { return _portfolio; }
            set { Set(ref _portfolio, value); }
        }

        public ObservableItemCollection<News> News
        {
            get { return _news; }
            set { Set(ref _news, value); }
        }

        public ObservableCollection<Own> Owns
        {
            get { return _owns; }
            set { Set(ref _owns, value); }
        }

        public Worth Worth
        {
            get { return _worth; }
            set { Set(ref _worth, value); }
        }

        public Bio Bio
        {
            get { return _bio; }
            set { Set(ref _bio, value); }
        }
    }
}