using System.Collections.Generic;
using System.Threading.Tasks;
using Billionaires.Universal.Models;
using Billionaires.Universal.Mvvm;
using Billionaires.Universal.ViewModels;
using System;

namespace Billionaires.Universal.Services.PeopleServices
{
    public interface IPeopleService
    {
        OptimizedObservableCollection<PersonViewModel> People { get; }

        event EventHandler Loaded;
        bool IsLoaded { get; }
        void Load();

        Task<IEnumerable<Person>> LoadData();

        Task LoadRanking(IEnumerable<Person> people);

        Task<IEnumerable<PortraitsData>> LoadImages(IEnumerable<Person> people);

        Task LoadDetails(Person person);
    }
}