using Billionaires.Universal.Mvvm;

namespace Billionaires.Universal.Models
{
    public class Own : ViewModelBase
    {
        private string _title;
        private string _credit;
        private string _image;
        private int _width;
        private int _height;

        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        public string Credit
        {
            get { return _credit; }
            set { Set(ref _credit, value); }
        }

        public string Image
        {
            get { return _image; }
            set { Set(ref _image, value); }
        }

        public int Width
        {
            get { return _width; }
            set { Set(ref _width, value); }
        }

        public int Height
        {
            get { return _height; }
            set { Set(ref _height, value); }
        }
    }
}