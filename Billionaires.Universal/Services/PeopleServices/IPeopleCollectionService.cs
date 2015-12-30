using Billionaires.Universal.Models;
using Billionaires.Universal.Mvvm;
using Billionaires.Universal.ViewModels;
using System;

namespace Billionaires.Universal.Services.PeopleServices
{
    public interface IPeopleCollectionService
    {
        event EventHandler Loaded;

        OptimizedObservableCollection<PersonViewModel> PeopleByName { get; }
        OptimizedObservableCollection<PersonViewModel> PeopleByRank { get; }
        OptimizedObservableCollection<AlphaKeyGroup> PeopleBySource { get; }
        OptimizedObservableCollection<AlphaKeyGroup> PeopleByCountry { get; }
    }
}