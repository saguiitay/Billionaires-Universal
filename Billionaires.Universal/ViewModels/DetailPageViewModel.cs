using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using Template10.Utils;
using Billionaires.Universal.Views;
using System.Windows.Input;
using Windows.UI.Xaml;
using Template10.Mvvm;

namespace Billionaires.Universal.ViewModels
{
    public class DetailPageViewModel : Mvvm.ViewModelBase
    {
        private readonly Random _random = new Random();

        private PersonViewModel _personViewModel;
        private PersonViewModel _prev;
        private PersonViewModel _next;
        private bool _isLoading;

        private ICommand _navToPersonCommand;

        public DetailPageViewModel()
        {
            _navToPersonCommand = new DelegateCommand<PersonViewModel>(DisplayPerson, p => p != null);

            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                _personViewModel = new PersonViewModel
                {
                    Person = new Models.Person
                    {
                        Age = 60,
                        Details = new Models.PersonDetails
                        {
                            Bio = new Models.Bio
                            {
                                Body = new System.Collections.ObjectModel.ObservableCollection<string> {
                                    "Born to successful parents -- his father was a lawyer, his mother was president of the local United Way -- Gates developed an interest in computers shortly after his admittance to Seattle's Lakeside School. After scoring a 1590 on his SATs (out of a possible 1600), he enrolled at Harvard University, where his Lakeside friend, Paul Allen, persuaded him to take a leave of absence to help him build an operating system for the Altair 8800, a new computer developed by Micro Instrumentation and Telemetry Systems. The result: Microsoft, now the world's largest software maker by revenue. ", "In 1994, Gates married Melinda French, a Microsoft employee whom he had met seven years earlier at a press event in New York. Six years later, after his Microsoft stake topped $100 billion, Gates stepped down as the company's chief executive and began focusing on his philanthropic efforts. To date, he has donated more than $28 billion to the Bill & Melinda Gates Foundation, which so far has given away $26 billion in grants to fight hunger, disease and poverty. He intends to give the majority of his wealth to charity, a commitment he reaffirmed by signing the Giving Pledge in 2010. "
                                },
                                Milestones = new System.Collections.ObjectModel.ObservableCollection<Models.Milestone>
                                {
                                    new Models.Milestone { Year = 1955, Event = "William Henry Gates III is born in Seattle." },
                                    new Models.Milestone { Year = 1968, Event = "Begins writing code on a Lakeside School computer." },
                                    new Models.Milestone { Year = 1973, Event = "Enrolls at Harvard University, where he meets Steve Ballmer." },
                                    new Models.Milestone { Year = 1975, Event = "Leaves Harvard to create Micro-Soft with Paul Allen." },
                                    new Models.Milestone { Year = 1981, Event = "Incorporates Microsoft in the state of Washington." },
                                    new Models.Milestone { Year = 1985, Event = "Software maker releases the first consumer version of Windows." },
                                    new Models.Milestone { Year = 1986, Event = "Microsoft completes IPO, one day after Oracle's public offering." },
                                    new Models.Milestone { Year = 1987, Event = "Gates's fortune tops $1 billion when Microsoft stock reaches $90." },
                                    new Models.Milestone { Year = 1994, Event = "Marries Microsoft employee Melinda French in Hawaii." },
                                    new Models.Milestone { Year = 1998, Event = "Testifies before Congress as part of a government antitrust case." },
                                    new Models.Milestone { Year = 1999, Event = "The value of his Microsoft holdings cross $100 billion." },
                                    new Models.Milestone { Year = 2000, Event = "Steps down as Microsoft chief executive officer." },
                                    new Models.Milestone { Year = 2000, Event = "The Bill & Melinda Gates Foundation is established." },
                                    new Models.Milestone { Year = 2003, Event = "Microsoft pays its first dividend." },
                                    new Models.Milestone { Year = 2010, Event = "Signs the Giving Pledge." }
                                },
                                Stats = new Models.BioStats
                                {
                                    Birth = "1955/10/28",
                                    Education = "Harvard University, 1973-1975, Harvard University, 1973-1975, Harvard University, 1973-1975",
                                    Family = "Married, 3 children, Married, 3 children, Married, 3 children"
                                }
                            },
                            Confidence = new Models.Confidence
                            {
                                Stars = 4
                            },
                            News = new Template10.Controls.ObservableItemCollection<Models.News>
                            {
                                new Models.News { Date = "Nov 4, 2015",Title="Corbis Said Cutting About 15% of Workers Amid Photo Price War", Link="http://www.bloomberg.com/news/articles/2015-11-04/corbis-said-cutting-about-15-of-workers-amid-photo-price-war" },
                                new Models.News { Date = "Nov 2, 2015",Title="One Man's Plan to Build Singapore in India Sends Land Soaring", Thumb="http://assets.bwbx.io/images/iCSLubtg12n4/v1/220x220.jpg", Link="http://www.bloomberg.com/news/articles/2015-11-02/one-man-s-plan-to-build-singapore-in-india-sends-land-soaring" },
                                new Models.News { Date = "Jul 8, 2015",Title="Microsoft to Cut Jobs, Take $7.6 Billion Writedown on Nokia", Thumb="http://assets.bwbx.io/images/iva6u5cuU6UA/v1/220x220.jpg", Link="http://www.bloomberg.com/news/articles/2015-07-08/microsoft-to-cut-7-800-jobs-as-it-restructures-phone-business" },
                            },
                            Overview = new Models.Overview
                            {
                                Intel = new System.Collections.ObjectModel.ObservableCollection<string>
                                {
                                    "Counts Nobel Prize-winning physicist Richard Feynman as a hero.", "Paid the Rolling Stones $10 million for the rights to \"Start Me Up.\"", "Served as a Democratic page for the U.S. Congress in 1972.", "Purchased the Bettmann Archive of 19 million photographs in 1995.", "Took singing lessons with his wife, Melinda.", "Is the largest shareholder of Canada's biggest railroad operator.", "Antagonist in Da Vinci's Notebook's satirical song \"The Gates.\"", "Told Reddit in 2014 that he washes his own dishes every night.", "Cascade's Corbis business manages Steve McQueen's likeness."
                                },
                                Body = new System.Collections.ObjectModel.ObservableCollection<string>
                                {
                                    "The world's richest person is co-founder and technology adviser of Microsoft, the world's biggest software maker. Gates's 3 percent stake of the Redmond, Washington-based company represents about a tenth of his total wealth. The rest of his fortune lies in Cascade Investment, an entity that has stakes in dozens of companies. He's donated $30 billion to his foundation."
                                }
                            },
                            Owns = new System.Collections.ObjectModel.ObservableCollection<Models.Own>
                            {
                                new Models.Own { Title = "Microsoft's main campus is in Redmond, Washington.", Credit="Photographer: Mike Kane/Bloomberg", Image="http://www.bloomberg.com/image/iUchHZ2rIFXg.jpg", Width=360, Height=240 },
                                new Models.Own { Title = "MITS Altair 8800, which ran Microsoft's first product.", Credit="Photographer: Microsoft Corp. via Bloomberg", Image="http://www.bloomberg.com/image/ij05mfGWO98E.jpg", Width=360, Height=240 },
                                new Models.Own { Title = "Biographical comic book published by Bluewater Productions.", Credit="Photograph: Bluewater Productions/Joe Phillips", Image="http://www.bloomberg.com/image/iMEpmNHmVyG0.jpg", Width=154, Height=240 }
                            },
                            Portfolio = new Models.Portfolio
                            {
                                Holdings = new Models.Holdings
                                {
                                    Cash = 45750000000,
                                    Private = 2225000000,
                                    Public = 37805959565,
                                    Liabilities = 0
                                }
                            },
                            Worth = new Models.Worth
                            {
                                Body = new System.Collections.ObjectModel.ObservableCollection<string>
                                {
                                    "The majority of Gates's fortune is derived from Cascade Investment, a holding company that was created with the proceeds of Microsoft stock sales and dividends. Since the software maker's initial public offering in 1986, the billionaire has sold about $36 billion in stock and collected more than $7 billion in dividends, including a $3.3 billion payout in 2004, which he donated to his philanthropic foundation. Shares held by the Bill & Melinda Gates Foundation, a charitable enterprise that supports poverty eradication, medical research and education initiatives, aren't included in his net worth calculation.", "Cascade owns stakes in about three dozen public companies, including AutoNation, Canadian National Railway and Strategic Hotels & Resorts. It also invests in closely held businesses, including real estate (Four Seasons Hotels) and energy (Sapphire Energy). The billionaire directly owns about 3 percent of Microsoft.", "Gates also founded Corbis, a photo archive company. It's valued based on the market value of Shutterstock and the 2008 leveraged buyout of Getty Images, its two closest peers.", "Bridgitt Arnold, a spokeswoman for Gates, said he declined to comment on his net worth."
                                }
                            }
                        },
                        Gender = "Male",
                        Industry = "Technology",
                        Source = "Self-made",
                        Name = new Models.Name
                        {
                            Full = "Bill Gates",
                            Last = "Gates",
                            Sort = "Gates, Bill"
                        },
                        Place = "United States",
                        Stats = new Models.Stats
                        {
                            Rank = 1,
                            Hold = new List<int> { 5332, 4408, 259 },
                            Last = -26103,
                            Last_rel = -30,
                            Net = 8580.0F,
                            Ytd = -80114,
                            Ytd_rel = -93
                        }
                    }
                };
                // designtime data
                return;
            }
        }

        public override void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (state.Any())
            {
                // clear any cache
                state.Clear();
            }
            else
            {
                // use navigation parameter
                var locator = XamlUtil.GetResource<ViewModelLocator>("Locator", null);
                var peopleService = locator.PeopleService;

                var people = peopleService.People;

                _personViewModel = people.FirstOrDefault(p => p.Person.Id == parameter.ToString());

                if (_personViewModel != null)
                {
                    IsLoading = true;
                    Shell.SetBusyVisibility(Visibility.Visible, "Loading...");
                    peopleService.LoadDetails(_personViewModel.Person)
                        .ContinueWith(t =>
                            {
                                IsLoading = false;
                                Shell.SetBusyVisibility(Visibility.Collapsed);
                            });

                    var currentRank = _personViewModel.Person.Stats.Rank;
                    _prev = people.FirstOrDefault(p => p.Person.Stats.Rank == currentRank - 1);
                    _next = people.FirstOrDefault(p => p.Person.Stats.Rank == currentRank + 1);
                }
            }
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            if (suspending)
            {
                // persist into cache
                //state[nameof(PersonViewModel)] = PersonViewModel;
            }
            return base.OnNavigatedFromAsync(state, suspending);
        }

        public override void OnNavigatingFrom(NavigatingEventArgs args)
        {
            args.Cancel = false;
        }

        public PersonViewModel PersonViewModel
        {
            get { return _personViewModel; }
            set { Set(ref _personViewModel, value); }
        }

        public PersonViewModel Prev
        {
            get { return _prev; }
            set
            {
                Set(ref _prev, value);
                RaisePropertyChanged(nameof(ShowPrevNextSeparator));
            }
        }

        public PersonViewModel Next
        {
            get { return _next; }
            set
            {
                Set(ref _next, value);
                RaisePropertyChanged(nameof(ShowPrevNextSeparator));
            }
        }

        public bool ShowPrevNextSeparator => Prev != null && Next != null;

        public bool IsLoading
        {
            get {  return _isLoading;}
            set { Set(ref _isLoading, value); }
        }
        public ICommand NavToPersonCommand
        {
            get { return _navToPersonCommand; }
            set { Set(ref _navToPersonCommand, value); }
        }

        public string RandomIntel
        {
            get
            {
                var intel = _personViewModel?.Person?.Details?.Overview?.Intel;
                if (intel == null)
                    return string.Empty;

                var index = _random.Next(0, intel.Count);
                return intel[index];
            }
        }

        public void DisplayPerson(PersonViewModel personViewModel)
        {
            NavigationService.Navigate(typeof(DetailPage), personViewModel.Person.Id);
        }
    }
}

