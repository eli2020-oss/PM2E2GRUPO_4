using PM2E2GRUPO_4.Controller;
using PM2E2GRUPO_4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E2GRUPO_4.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UbicacionesDetailsPage : ContentPage
    {
        FirebaseHelper services;
        public UbicacionesDetailsPage(Ubicaciones ubicaciones)
        {
            InitializeComponent();
            BindingContext = ubicaciones;
            services = new FirebaseHelper();
        }
        public async void BtnDelete_Ubicaciones(object sender, ItemTappedEventArgs args)
        {
            await services.DeleteUbicacion(latitud.Text, longitud.Text, descripcion.Text);
            await Navigation.PushAsync(new UbicacionesPage());

        }
        public async void BtnUpdate_Ubicaciones(object sender, ItemTappedEventArgs args)
        {
            await services.UpdateUbicaciones(latitud.Text, longitud.Text, descripcion.Text);
            await Navigation.PushAsync(new UbicacionesPage());

        }
    }
}