using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nomadic.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Explore : ContentPage
    {
        public Explore()
        {
            InitializeComponent();
            BindingContext = ViewModels.InterestsViewModel.Instance;
        }

        private async void Boton1_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Respuesta Correcta", "18","Siguiente");
        }
    }
}
