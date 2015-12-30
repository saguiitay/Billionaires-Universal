using Billionaires.Universal.ViewModels;
using Windows.UI.Xaml.Controls;

namespace Billionaires.Universal.Views
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
        }

        // strongly-typed view models enable x:bind
        public MainPageViewModel ViewModel => this.DataContext as MainPageViewModel;

        private void Item_Click(object sender, ItemClickEventArgs e)
        {
            ViewModel.DisplayPerson(e.ClickedItem as PersonViewModel);
        }
    }
}
