// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System.Windows.Input;
using Windows.UI.Xaml.Media.Imaging;
using Template10.Mvvm;
using ViewModelBase = Billionaires.Universal.Mvvm.ViewModelBase;

namespace Billionaires.Universal.Models
{
    public class Person : ViewModelBase
    {
        private Name _name;
        private int? _age;
        private Stats _stats;
        private ImageInfo _imageInfo;
        private WriteableBitmap _image;
        private PersonDetails _details;
        private string _finalDay;
        private string _source;
        private string _industry;
        private string _gender;
        private string _place;
        //private string _status;
        //private int _children;
        private ICommand _navigate;
        public string Id { get; set; }

        public Person()
        {
            Navigate = new DelegateCommand(() => { });
        }

        public ICommand Navigate
        {
            get { return _navigate; }
            set { Set(ref _navigate, value); }
        }


        public Name Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        public int? Age
        {
            get { return _age; }
            set { Set(ref _age, value); }
        }

        //public int Children
        //{
        //    get { return _children; }
        //    set { _children = value; NotifyPropertyChanged(); }
        //}

        //public string Status
        //{
        //    get { return _status; }
        //    set { _status = value; NotifyPropertyChanged(); }
        //}

        public string Place
        {
            get { return _place; }
            set { Set(ref _place, value); }
        }

        public string Gender
        {
            get { return _gender; }
            set { Set(ref _gender, value); }
        }

        public string Industry
        {
            get { return _industry; }
            set { Set(ref _industry, value); }
        }

        public string Source
        {
            get { return _source; }
            set { Set(ref _source, value); }
        }

        public string Final_day
        {
            get { return _finalDay; }
            set { Set(ref _finalDay, value); }
        }

        public Stats Stats
        {
            get { return _stats; }
            set { Set(ref _stats, value); }
        }

        public WriteableBitmap Image
        {
            get { return _image; }
            set { Set(ref _image, value); }
        }

        public PersonDetails Details
        {
            get { return _details; }
            set { Set(ref _details, value); }
        }

        public ImageInfo ImageInfo
        {
            get { return _imageInfo; }
            set { Set(ref _imageInfo, value); }
        }
    }
}
