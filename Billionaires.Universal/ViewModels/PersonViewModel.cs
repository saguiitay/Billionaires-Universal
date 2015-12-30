using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Billionaires.Universal.Models;

namespace Billionaires.Universal.ViewModels
{
    public class PersonViewModel : Mvvm.ViewModelBase
    {
        private Person _person;
        private BitmapImage _image;
        private RectangleGeometry _clip;
        private int _left;

        public Person Person
        {
            get { return _person; }
            set { Set(ref _person, value); }
        }

        public BitmapImage Image
        {
            get { return _image; }
            set { Set(ref _image, value); }
        }

        public RectangleGeometry Clip
        {
            get { return _clip; }
            set { Set(ref _clip, value); }
        }

        public int Left
        {
            get { return _left; }
            set { Set(ref _left, value); }
        }

    }
}