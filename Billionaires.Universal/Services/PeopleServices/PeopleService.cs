using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Billionaires.Universal.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Template10.Common;
using Billionaires.Universal.Mvvm;
using Windows.UI.Xaml.Media.Imaging;
using Billionaires.Universal.ViewModels;
using Windows.UI.Xaml.Media;
using Windows.Foundation;

namespace Billionaires.Universal.Services.PeopleServices
{
    public class PeopleService : IPeopleService
    {
        private readonly HttpClient _httpClient;
        private readonly IDispatcherWrapper _dispatcherWrapper;

        public OptimizedObservableCollection<PersonViewModel> People { get; } = new OptimizedObservableCollection<PersonViewModel>();

        public event EventHandler Loaded;
        public bool IsLoaded { get; private set; }

        public PeopleService()
        {
            _dispatcherWrapper = WindowWrapper.Current().Dispatcher;

            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
                handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            _httpClient = new HttpClient(handler);
        }

        public void Load()
        {
            if (IsLoaded) return;

            var loadDataTask = LoadData();

            loadDataTask.ContinueWith(x =>
                    {
                        Loaded?.Invoke(this, EventArgs.Empty);
                    }, TaskContinuationOptions.OnlyOnFaulted);

            loadDataTask.ContinueWith(async x =>
                    {
                        var people = x.Result.ToList();

                        await LoadRanking(people).ConfigureAwait(false);

                        people = people.Where(p => p.Stats != null).ToList();

                        var portraitsDatas = LoadImages(people)
                            .ConfigureAwait(false)
                            .GetAwaiter()
                            .GetResult()
                            .ToList();

                        var images = new Dictionary<int, BitmapImage>();

                        for (int i = 0; i < portraitsDatas.Count; i++)
                        {
                            var portraitsData = portraitsDatas[i];
                            var i1 = i;
                            _dispatcherWrapper.Dispatch(() =>
                            {
                                var image = portraitsData.GetBitmapImage();
                                images[i1] = image;
                            });
                        }

                        //List<PersonViewModel> sortedPeople = new List<PersonViewModel>();

                        foreach (var personToAdd in people)
                        {
                            var personViewModel = new PersonViewModel { Person = personToAdd };
                            People.Add(personViewModel);

                            if (personToAdd.ImageInfo == null)
                                continue;

                            _dispatcherWrapper.Dispatch(() =>
                            {
                                var image = images[personToAdd.ImageInfo.Image];
                                personViewModel.Image = image;
                                personViewModel.Clip = new RectangleGeometry
                                {
                                    Rect = new Rect(personToAdd.ImageInfo.Left, 0, 95, 100)
                                };
                                personViewModel.Left = -personToAdd.ImageInfo.Left;
                            });
                        }

                        IsLoaded = true;
                        Loaded?.Invoke(this, EventArgs.Empty);
                    }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }


        public async Task<IEnumerable<Person>> LoadData()
        {
            const string metadataUrl = "http://www.bloomberg.com/billionaires/db/metadata/js";

            var people = new List<Person>();

            var result = await _httpClient.GetStringAsync(metadataUrl);
            if (result == null)
                return people;

            result = result.Substring(7);

            var x2 = JsonConvert.DeserializeObject<JObject>(result);

            var d = x2["people"];
            people.AddRange(ConsecutivePairs(d));

            return people;
        }

        public async Task LoadRanking(IEnumerable<Person> people)
        {
            const string statsUrl = "http://www.bloomberg.com/billionaires/db/stats/{0}";

            for (int daysBack = 0; daysBack < 14; daysBack++)
            {
                try
                {
                    var lastDay = DateTime.Today.AddDays(-daysBack).ToString("yyyy/MM/dd");
                    var result2 = await _httpClient.GetStringAsync(string.Format(statsUrl, lastDay));
                    if (result2 == "{}")
                        continue;

                    var dict = JsonConvert.DeserializeObject<IDictionary<string, Stats>>(result2);
                    if (dict.Count == 0)
                        continue;

                    foreach (var person in people.Where(p => p.Stats == null))
                    {
                        Stats stats;
                        if (dict.TryGetValue(person.Id, out stats))
                        {
                            person.Stats = stats;
                        }
                    }

                    break;
                }
                catch (Exception)
                {
                }
            }
        }

        public async Task<IEnumerable<PortraitsData>> LoadImages(IEnumerable<Person> people)
        {
            const string imagesUrl = "http://www.bloomberg.com/billionaires/3/face-index-9b2a6889962cf514a382b72ebe041fd4.json";
            const string imagesFacesUrlRoot = "http://www.bloomberg.com/billionaires/db/portraits/chunk/3/";

            var imagesResult = await _httpClient.GetStringAsync(imagesUrl);
            var facePrintsIndex = imagesResult.IndexOf("FACE_PRINTS=");
            var facesIndex = imagesResult.IndexOf("FACES=");
            var facePrints = imagesResult.Substring(facePrintsIndex + 12, facesIndex - 13);
            var faces = imagesResult.Substring(facesIndex + 6);
            var facesData = JsonConvert.DeserializeObject<IDictionary<string, List<int>>>(faces);

            foreach (var person in people)
            {
                List<int> imageInfo;
                if (!facesData.TryGetValue(person.Id, out imageInfo))
                    continue;
                person.ImageInfo = new ImageInfo {Image = imageInfo[0], Left = imageInfo[1]*95};
            }

            var facePrintsData = JsonConvert.DeserializeObject<List<string>>(facePrints);

            var loadImageTasks = new List<Task<PortraitsData>>();
            for (int index = 0; index < facePrintsData.Count; index++)
            {
                var printsData = facePrintsData[index];
                int index1 = index;
                var task = _httpClient.GetStringAsync(imagesFacesUrlRoot + printsData)
                    .ContinueWith(t =>
                    {
                        var imageDataResult = t.Result;
                        var imageData = JsonConvert.DeserializeObject<PortraitsData>(imageDataResult);
                        imageData.index = index1;
                        return imageData;
                    });

                loadImageTasks.Add(task);
            }

            await Task.WhenAll(loadImageTasks);

            return loadImageTasks.Select(t => t.Result);

            //var faceLoadingTasks = new List<Task>();

            //var imagesRawData = new List<IRandomAccessStream>();

            //foreach (var task in loadImageTasks)
            //{
            //    var imageData = task.Result;
            //    imagesRawData.Add(imageData.RawData);

            //    //var image = imageData.GetImage();
            //    //imagesTiles.Add(imageData.GetTileImage(image));

            //    faceLoadingTasks.Add(LoadFaces(people, imagesRawData, faces, imageData.index));
            //}

            //await Task.WhenAll(faceLoadingTasks);
        }

        public async Task LoadDetails(Person person)
        {
            const string detailsUrl = "http://www.bloomberg.com/billionaires/db/people/{0}?include=all";

            var data = await _httpClient.GetStringAsync(string.Format(detailsUrl, person.Id));
            var details = JsonConvert.DeserializeObject<PersonDetails>(data);

            person.Details = details;
        }

        private static IEnumerable<Person> ConsecutivePairs(IEnumerable<JToken> sequence)
        {
            var result = new List<Person>();
            foreach (var d2 in sequence)
            {
                var id = d2[0];
                var person = JsonConvert.DeserializeObject<Person>(d2[1].ToString());
                person.Id = id.ToString();

                result.Add(person);
            }
            return result;
        }
    }
}
