using Billionaires.Universal.Mvvm;
using Billionaires.Universal.ViewModels;
using System;
using System.Globalization;
using System.Linq;

namespace Billionaires.Universal.Services.PeopleServices
{
    public class PeopleCollectionService : IPeopleCollectionService
    {
        public event EventHandler Loaded;
        public OptimizedObservableCollection<PersonViewModel> PeopleByName { get; private set; }
        public OptimizedObservableCollection<PersonViewModel> PeopleByRank { get; private set; }
        public OptimizedObservableCollection<AlphaKeyGroup> PeopleBySource { get; private set; }
        public OptimizedObservableCollection<AlphaKeyGroup> PeopleByCountry { get; private set; }

        private readonly IPeopleService _peopleService;

        public PeopleCollectionService(IPeopleService peopleService)
        {
            _peopleService = peopleService;

            if (_peopleService.IsLoaded)
                Configure();
            else
            {
                _peopleService.Load();
                _peopleService.Loaded += PeopleServiceOnLoaded;
            }
        }

        private void PeopleServiceOnLoaded(object sender, EventArgs eventArgs)
        {
            _peopleService.Loaded -= PeopleServiceOnLoaded;
            Configure();
        }

        private void Configure()
        {
            PeopleByName = new OptimizedObservableCollection<PersonViewModel>(_peopleService.People.OrderBy(p => p.Person.Name.Sort));

            PeopleByRank = new OptimizedObservableCollection<PersonViewModel>(_peopleService.People.OrderBy(p => p.Person.Stats.Rank));

            PeopleBySource = new OptimizedObservableCollection<AlphaKeyGroup>(AlphaKeyGroup.CreateGroups(_peopleService.People, CultureInfo.CurrentUICulture,
                x => ((PersonViewModel)x).Person.Name.Sort));

            PeopleByCountry = new OptimizedObservableCollection<AlphaKeyGroup>(AlphaKeyGroup.CreateGroups(_peopleService.People, CultureInfo.CurrentUICulture,
                x => ((PersonViewModel)x).Person.Place));

            Loaded?.Invoke(this, EventArgs.Empty);
        }
    }
}