﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nomadic.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Local : ContentPage
    {
        public Local()
        {
            InitializeComponent();
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var tapped = e.Item as Models.Article;

            await Navigation.PushAsync(new WebPage(tapped));
        }
    }
}