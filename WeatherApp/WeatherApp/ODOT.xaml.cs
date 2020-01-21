using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup;

namespace WeatherApp

    //this app only needs to have road temp readings pull in the data from odot with a dropdown menu for specific items 
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ODOT : ContentPage
    {
         

        public ODOT()
        {
            InitializeComponent();
            webView.Source = "https://www.buckeyetraffic.org/reporting/RWIS/results.aspx";
        }
        //need this method to pull the html from the web page then have it parse out essential data through some kind of loop
        //private async void Task ObtainData()
        //{
        //    try
        //    {
        //        var webPage = await new System.Net.Http.HttpClient().GetStringAsync(new Uri("https://www.buckeyetraffic.org/reporting/RWIS/results.aspx"));
        //        await Page.DisplayAlert("alert", webPage, "ok", "cancel");
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //}

        void webviewNavigating(object sender, WebNavigatingEventArgs e)
        {
            labelLoading.IsVisible = true;
        }

        void webviewNavigated(object sender, WebNavigatedEventArgs e)
        {
            labelLoading.IsVisible = false;
        }

        void SearchBar_TextChanged(object sender, WebNavigatedEventArgs e)
        {

        }
    }
}