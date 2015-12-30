using System.Collections.Generic;
using System.Globalization;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;
using Billionaires.Universal.Mvvm;
using Billionaires.Universal.Services.PeopleServices;
using Billionaires.Universal.Views;
using System.Windows.Input;
using Template10.Mvvm;
using System.Linq;
using Windows.UI.Xaml;
using Template10.Utils;

namespace Billionaires.Universal.ViewModels
{
    public class MainPageViewModel : Mvvm.ViewModelBase
    {
        private bool _isLoading;
        private CollectionViewSource _viewSource;
        private readonly IPeopleService _peopleService;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { Set(ref _isLoading, value); }
        }

        public ICommand SortByRankCommand { get; }

        public ICommand SortByNameCommand { get; }

        public CollectionViewSource ViewSource
        {
            get { return _viewSource; }
            set { Set(ref _viewSource, value); }
        }

        public MainPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                // designtime data
                return;
            }

            SortByRankCommand = new DelegateCommand(() => ChangeSort(PeopleSort.Rank));
            SortByNameCommand = new DelegateCommand(() => ChangeSort(PeopleSort.Name));

            var locator = XamlUtil.GetResource<ViewModelLocator>("Locator", null);
            _peopleService = locator.PeopleService;


        }

        public override void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (_peopleService.IsLoaded)
                return;

            Shell.SetBusyVisibility(Visibility.Visible, "Loading...");

            _peopleService.Loaded += (s, e) =>
                {
                    ChangeSort(PeopleSort.Rank);
                    Shell.SetBusyVisibility(Visibility.Collapsed);
                };

            _peopleService.Load();
        }


        public void ChangeSort(PeopleSort sort)
        {
            Dispatcher.Dispatch(() =>
            {
                ViewSource = new CollectionViewSource { IsSourceGrouped = false /*sort != PeopleSort.Name && sort != PeopleSort.Rank*/ };

                var people = _peopleService.People;
                switch (sort)
                {
                    case PeopleSort.Name:
                        ViewSource.Source = new OptimizedObservableCollection<PersonViewModel>(people.OrderBy(p => p.Person.Name.Sort));
                        break;
                    case PeopleSort.Rank:
                        ViewSource.Source = new OptimizedObservableCollection<PersonViewModel>(people.OrderBy(p => p.Person.Stats.Rank));
                        break;
                    case PeopleSort.Country:
                        ViewSource.Source =
                            new OptimizedObservableCollection<AlphaKeyGroup>(AlphaKeyGroup.CreateGroups(people,
                                CultureInfo.CurrentUICulture, x => ((PersonViewModel) x).Person.Place));
                        break;
                    case PeopleSort.Source:
                        ViewSource.Source =
                            new OptimizedObservableCollection<AlphaKeyGroup>(AlphaKeyGroup.CreateGroups(people,
                                CultureInfo.CurrentUICulture, x => ((PersonViewModel) x).Person.Name.Sort));
                        break;
                    default:
                        throw new System.ArgumentOutOfRangeException(nameof(sort), sort, null);
                }
            });
        }

        public void DisplayPerson(PersonViewModel personViewModel)
        {
            NavigationService.Navigate(typeof (DetailPage), personViewModel.Person.Id);
        }
    }

    public enum PeopleSort
    {
        Name,
        Rank,
        Source,
        Country
    }
}

