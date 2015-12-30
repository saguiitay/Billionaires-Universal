using Billionaires.Universal.Services.PeopleServices;
using System;

namespace Billionaires.Universal.ViewModels
{
    public class ViewModelLocator
    {
        private readonly Lazy<MainPageViewModel> _main;
        private readonly Lazy<DetailPageViewModel> _detail;
        private readonly Lazy<IPeopleService> _peopleService;

        public ViewModelLocator()
        {
            _main = new Lazy<MainPageViewModel>(() => new MainPageViewModel());
            _detail = new Lazy<DetailPageViewModel>(() => new DetailPageViewModel());
            _peopleService = new Lazy<IPeopleService>(() => new PeopleService());
        }

        public MainPageViewModel Main => _main.Value;

        public DetailPageViewModel Detail => _detail.Value;

        public IPeopleService PeopleService => _peopleService.Value;
    }
}