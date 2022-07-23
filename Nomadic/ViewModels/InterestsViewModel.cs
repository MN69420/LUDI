using NewsAPI.Constants;
using Newtonsoft.Json;
using Nomadic.AppSettings;
using Nomadic.Helpers;
using Nomadic.Models;
using Plugin.CloudFirestore;
using PSC.Xamarin.MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace Nomadic.ViewModels
{
    public class InterestsViewModel : BaseViewModel
    {
        #region Properties

        /// <summary>
        /// List of Interests
        /// </summary>
        ObservableRangeCollection<Interest> _interests;
        public ObservableRangeCollection<Interest> Interests
        {
            get { return _interests; }
            set
            {
                _interests = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// List of Interests to be used in Explore page for search
        /// </summary>
        ObservableRangeCollection<Interest> _interestsSearchList;
        public ObservableRangeCollection<Interest> InterestsSearchList
        {
            get { return _interestsSearchList; }
            set
            {
                _interestsSearchList = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Search text bindinded to Explore SearchBar
        /// </summary>
        string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged();
                InterestsSearchList = FilteredRecords(SearchText);
            }
        }

        /// <summary>
        /// Selected Interet Tab
        /// </summary>
        Tab _currentItem;
        public Tab CurrentItem
        {
            get { return _currentItem; }
            set
            {
                _currentItem = value;
                OnPropertyChanged();
                _ = LoadTabData(CurrentItem);
            }
        }

        /// <summary>
        /// Selected Interest to be displayed in InterestArticlesPage 
        /// </summary>
        Interest _currentInterest;
        public Interest CurrentInterest
        {
            get { return _currentInterest; }
            set
            {
                _currentInterest = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public InterestsViewModel()
        {
            _ = LoadInterestsList();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Function to get user interests
        /// </summary>
        /// <returns>Returns a list of interests</returns>
        public async Task LoadInterestsList()
        {
            IsBusy = true;

            await Task.Run(() => 
            {
                Interests = new ObservableRangeCollection<Interest>
                {
                    new Interest
                    {
                        Title = "MATEMÁTICAS",
                        UrlToImage = "https://i.imgur.com/0hQx6cI.png"
                    },
                    new Interest
                    {
                        Title = "ESPAÑOL",
                        UrlToImage = "https://i.imgur.com/QsZDAMr.png"
                    },
                    new Interest
                    {
                        Title = "HISTORIA",
                        UrlToImage = "https://i.imgur.com/kIgWVrx.png"
                    },
                    new Interest
                    {
                        Title = "GEOGRAFÍA",
                        UrlToImage = "https://i.imgur.com/40M6jkk.png"
                    },
                    new Interest
                    {
                        Title = "INGLÉS",
                        UrlToImage = "https://i.imgur.com/Koc9XNc.png"
                    },
                    new Interest
                    {
                        Title = "CIENCIAS N.",
                        UrlToImage = "https://i.imgur.com/nxpXedq.png"
                    },
                    new Interest
                    {
                        Title = "CIVICA Y ETICA",
                        UrlToImage = "https://i.imgur.com/MxD5nlG.png"
                    },
                    new Interest
                    {
                        Title = "ARTES",
                        UrlToImage = "https://i.imgur.com/YeYqTaG.png"
                    },
                    new Interest
                    {
                        Title = "DEPORTES",
                        UrlToImage = "https://i.imgur.com/FtHEsY1.png"
                    },
                    new Interest
                    {
                        Title = "BIOLOGÍA",
                        UrlToImage = "https://i.imgur.com/dZBB8zp.png"
                    },
                    new Interest
                    {
                        Title = "FÍSICA",
                        UrlToImage = "https://i.imgur.com/scyWO35.png"
                    },
                    new Interest
                    {
                        Title = "QUÍMICA",
                        UrlToImage = "https://i.imgur.com/SkhEzRH.png"
                    },
                    new Interest
                    {
                        Title = "ALGEBRA",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Frelationships.png?alt=media&token=b503a2d6-8a1b-4872-a1b9-70a9ad8e245f"
                    },
                    new Interest
                    {
                        Title = "TRAVEL",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Ftravel.png?alt=media&token=7c0e89a2-e65a-438c-93ac-250675ab36c0"
                    },
                    new Interest
                    {
                        Title = "AUTO",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Fauto.png?alt=media&token=a881c40d-f535-48fd-9c78-a5db312f9fce"
                    },
                    new Interest
                    {
                        Title = "COOKING",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Fcooking.png?alt=media&token=b5e841f0-1515-4e61-8177-39f60e3796ca"
                    },
                    new Interest
                    {
                        Title = "WEIGHT LOSS",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Fweight_loss.png?alt=media&token=d9d4da10-ec8b-4eb4-97ce-8c138b229956"
                    },
                    new Interest
                    {
                        Title = "DRINKS",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Fdrinks.png?alt=media&token=c54355a7-e7f1-4132-9d25-a40040f4ba66"
                    },
                };

                var interestsList = DatabaseHelper.GetSavedInterestsList();

                if (interestsList != null && interestsList.Any())
                {
                    foreach (var interest in Interests)
                    {
                        interest.IsInterestAdded = interestsList.Where(s => s.Title.ToLower().Equals(interest.Title.ToLower())).Any();
                    }
                }
                else
                {
                    foreach (var interest in Interests)
                    {
                        interest.IsInterestAdded = MainFeedViewModel.Instance.TabItems.Where(s => s.Title.ToLower().Equals(interest.Title.ToLower())).Any();
                    }
                }

                InterestsSearchList = Interests;

                IsBusy = false;

            });
        }

        /// <summary>
        /// Method to Handle fetching of Articles from NewsAPI 
        /// depending on the user Interests
        /// </summary>
        /// <param name="tab">Takes in a Tab Item</param>
        /// <param name="isRefreshing">True if page is being refreshed</param>
        async Task LoadTabData(Tab tab, bool isRefreshing = false)
        {
            switch (tab.Title.ToLower())
            {
                case "headlines":
                    await GetTopHeadlines(tab, isRefreshing).ConfigureAwait(false);
                    break;
                case "business":
                    await GetWorldTopHeadlinesByCategory(tab, Categories.Business, isRefreshing).ConfigureAwait(false);
                    break;
                case "technology":
                    await GetWorldTopHeadlinesByCategory(tab, Categories.Technology, isRefreshing).ConfigureAwait(false);
                    break;
                case "entertainment":
                    await GetWorldTopHeadlinesByCategory(tab, Categories.Entertainment, isRefreshing).ConfigureAwait(false);
                    break;
                case "sports":
                    await GetWorldTopHeadlinesByCategory(tab, Categories.Sports, isRefreshing).ConfigureAwait(false);
                    break;
                case "science":
                    await GetWorldTopHeadlinesByCategory(tab, Categories.Science, isRefreshing).ConfigureAwait(false);
                    break;
                case "health":
                    await GetWorldTopHeadlinesByCategory(tab, Categories.Health, isRefreshing).ConfigureAwait(false);
                    break;
                default:
                    await SearchArticles(tab).ConfigureAwait(false);
                    break;
            }
        }

        /// <summary>
        /// Method to fetch TopHeadlines
        /// </summary>
        /// <param name="tab">Takes in a Tab Item</param>
        /// <param name="isRefreshing">True if page is being refreshed</param>
        async Task GetTopHeadlines(Tab tab, bool isRefreshing = false)
        {
            tab.HasError = false;

            try
            {
                if (tab.Articles.Count == 0)
                {
                    tab.IsBusy = true;

                    var articles = await NewsApiHelper.GetWorldTopHeadlines(page: tab.ArticlePage);

                    tab.Articles.AddRange(articles);

                    tab.IsBusy = false;
                }
                else if (isRefreshing)
                {
                    tab.IsRefreshing = true;

                    var articles = await NewsApiHelper.GetWorldTopHeadlines(page: tab.ArticlePage);

                    tab.Articles.ReplaceRange(articles);

                    tab.IsRefreshing = false;
                }

                if (tab.Articles.Count == 0)
                {
                    tab.IsRefreshing = false;
                    tab.IsBusy = false;
                    tab.HasError = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                tab.IsRefreshing = false;
                tab.IsBusy = false;
                tab.HasError = true;
            }
        }

        /// <summary>
        /// Method to fetch TopHeadlines by Category
        /// </summary>
        /// <param name="tab">Takes in a Tab Item</param>
        /// <param name="isRefreshing">True if page is being refreshed</param>
        async Task GetWorldTopHeadlinesByCategory(Tab tab, Categories category, bool isRefreshing = false)
        {
            tab.HasError = false;

            try
            {
                if (tab.Articles.Count == 0)
                {
                    tab.IsBusy = true;

                    var articles = await NewsApiHelper.GetWorldTopHeadlinesByCategory(category: category, page: tab.ArticlePage);

                    tab.Articles.AddRange(articles);

                    tab.IsBusy = false;
                }
                else if (isRefreshing)
                {
                    tab.IsRefreshing = true;

                    var articles = await NewsApiHelper.GetWorldTopHeadlinesByCategory(category: category, page: tab.ArticlePage);

                    tab.Articles.ReplaceRange(articles);

                    tab.IsRefreshing = false;
                }

                if (tab.Articles.Count == 0)
                {
                    tab.IsRefreshing = false;
                    tab.IsBusy = false;
                    tab.HasError = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                tab.IsRefreshing = false;
                tab.IsBusy = false;
                tab.HasError = true;
            }
        }

        /// <summary>
        /// Method to search articles depending on Interest Key words
        /// </summary>
        /// <param name="tab">Takes in a Tab Item</param>
        /// <param name="isRefreshing">True if page is being refreshed</param>
        async Task SearchArticles(Tab tab, bool isRefreshing = false)
        {
            tab.HasError = false;

            try
            {
                if (tab.Articles.Count == 0)
                {
                    tab.IsBusy = true;

                    var articles = await NewsApiHelper.SearchArticles(new string[] { tab.Title }, page: tab.ArticlePage);

                    tab.Articles.AddRange(articles);

                    tab.IsBusy = false;
                }
                else if (isRefreshing)
                {
                    tab.IsRefreshing = true;

                    var articles = await NewsApiHelper.SearchArticles(new string[] { tab.Title }, tab.ArticlePage);

                    tab.Articles.ReplaceRange(articles);

                    tab.IsRefreshing = false;
                }

                if (tab.Articles.Count == 0)
                {
                    tab.IsRefreshing = false;
                    tab.IsBusy = false;
                    tab.HasError = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                tab.IsRefreshing = false;
                tab.IsBusy = false;
                tab.HasError = true;
            }
        }

        /// <summary>
        /// Function to filter interests
        /// </summary>
        ObservableRangeCollection<Interest> FilteredRecords(string NewTextValue)
        {
            ObservableRangeCollection<Interest> filter = new ObservableRangeCollection<Interest>();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                for (int i = 0; i < Interests.Count; i++)
                {
                    Interest _interest = Interests[i];

                    if (_interest.Title.ToLower().Contains(NewTextValue.ToLower()))
                    {
                        filter.Add(_interest);
                    }
                }
            });

            return filter;
        }

        /// <summary>
        /// Reloads page data in InterestArticles page
        /// </summary>
        async Task Reload()
        {
            await LoadTabData(CurrentItem, isRefreshing: true);
        }

        /// <summary>
        /// Saves users Interests in the database and locally
        /// </summary>
        /// <param name="interest"></param>
        public async Task AddUserInterest(Interest interest)
        {
            try
            {
                interest.Title = interest.Title.ToLower();

                var userInterestsList = DatabaseHelper.GetSavedInterestsList();

                if (userInterestsList == null)
                {
                    userInterestsList = new List<Interest>();
                }

                userInterestsList.Add(interest);

                string userInterestsJson = JsonConvert.SerializeObject(userInterestsList);
                Settings.AddSetting(Settings.AppPrefrences.Interests, userInterestsJson);

                var addableTabItem = new Tab { Title = interest.Title, ArticlePage = 1 };
                var articles = await NewsApiHelper.SearchArticles(new string[] { interest.Title.ToLower() });
                addableTabItem.Articles.AddRange(articles);
                addableTabItem.IsBusy = false;

                MainFeedViewModel.Instance.TabItems.Insert(MainFeedViewModel.Instance.TabItems.IndexOf(MainFeedViewModel.Instance.TabItems.LastOrDefault()) + 1, addableTabItem);

                string isloggedIn = Settings.GetSetting(Settings.AppPrefrences.IsLoggedIn);

                if (isloggedIn != null || isloggedIn != "False")
                {
                    await DatabaseHelper.UpdateUserInterests(userInterestsList).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Removes users interest from the database
        /// </summary>
        /// <param name="interest"></param>
        /// <returns></returns>
        public async Task RemoveUserInterest(Interest interest)
        {
            try
            {
                var userInterestsList = DatabaseHelper.GetSavedInterestsList();

                userInterestsList.Remove(userInterestsList.Where(s => s.Title.ToLower().Equals(interest.Title.ToLower())).FirstOrDefault());

                string userInterestsJson = JsonConvert.SerializeObject(userInterestsList);
                Settings.AddSetting(Settings.AppPrefrences.Interests, userInterestsJson);

                var removableTabItem = MainFeedViewModel.Instance.TabItems.Where(s => s.Title.ToLower().Equals(interest.Title.ToLower())).FirstOrDefault();

                MainFeedViewModel.Instance.TabItems.Remove(removableTabItem);

                string isloggedIn = Settings.GetSetting(Settings.AppPrefrences.IsLoggedIn);

                if (isloggedIn != null || isloggedIn != "False")
                {
                    await DatabaseHelper.UpdateUserInterests(userInterestsList).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Frunction to save user Interest from popup
        /// </summary>
        /// <returns></returns>
        async Task SaveInterest()
        {
            if (CurrentInterest.IsInterestAdded)
            {
                CurrentInterest.IsInterestAdded = false;
                Interests.Where(s => s.Equals(CurrentInterest)).FirstOrDefault().IsInterestAdded = false;
                await RemoveUserInterest(CurrentInterest);
            }
            else
            {
                CurrentInterest.IsInterestAdded = true;
                Interests.Where(s => s.Equals(CurrentInterest)).FirstOrDefault().IsInterestAdded = true;
                await AddUserInterest(CurrentInterest);
            }
        }

        /// <summary>
        /// Command to refresh InsterestAricles page data
        /// </summary>
        ICommand _refreshCommand = null;

        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand =
                                          new Xamarin.Forms.Command(async (object obj) => await Reload()));
            }
        }

        /// <summary>
        /// Command to save user interest
        /// </summary>
        ICommand _saveInterestCommand = null;

        public ICommand SaveInterestCommand
        {
            get
            {
                return _saveInterestCommand ?? (_saveInterestCommand =
                                          new Xamarin.Forms.Command(async (object obj) => await SaveInterest()));
            }
        }

        #endregion

        /// <summary>
        /// Gets an Instance of this class
        /// </summary>
        public static InterestsViewModel Instance { get; } = new InterestsViewModel();
    }
}
