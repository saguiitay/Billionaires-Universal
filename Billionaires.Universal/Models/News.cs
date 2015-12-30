using System;
using System.Windows.Input;
using Template10.Mvvm;
using ViewModelBase = Billionaires.Universal.Mvvm.ViewModelBase;

namespace Billionaires.Universal.Models
{
    public class News : ViewModelBase
    {
        private string _date;
        private string _link;
        private string _title;
        private string _thumb;

        public News()
        {
            Navigate = new DelegateCommand(() =>
            {
            });
        }

        public string Date
        {
            get { return _date; }
            set { Set(ref _date, value); }
        }

        public string Link
        {
            get { return _link; }
            set { Set(ref _link, value); }
        }

        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        public string Thumb
        {
            get { return _thumb; }
            set { Set(ref _thumb, value); }
        }

        public ICommand Navigate { get; set; }
    }
}