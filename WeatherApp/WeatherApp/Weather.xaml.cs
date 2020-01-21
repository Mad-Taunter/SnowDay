using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WeatherApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    //needs the current temperature and weather conditions, use openweatherapi because it's easy to code this 
    public partial class Weather : ContentPage
    {
        //URL https://api.weather.gov/
        private System.Net.Http.HttpClient _client = new System.Net.Http.HttpClient();
        //var post = new { Title = "Title " + DateTime.Now.Ticks };
        //var content = JsonConvert.SerializeObject(post);
        //await _client.PostAsync(URL, content);
        public Weather()
        {
            InitializeComponent();
            this.BindingContext = this;
        }

        public List<WeatherSetter> Weathers { get => WeatherData(); }
        //public List<WeatherSetter> Weathers { get => WeatherData().Result; }
        //public List<WeatherSetter> Weathers = Task.Run(async () => await WeatherData()).Result; 
        public List<WeatherSetter> WeatherData()
        {
            JObject JsonData = FindWeather().Result;
            //creates a functioning c# object of the json values that can then be found below by listing each of the fields and indexs like a directory
            string tmrwTemp = (string)JsonData["properties"]["periods"][2]["temperature"];
            string image = (string)JsonData["properties"]["periods"][2]["icon"];
            //System.Diagnostics.Debug.WriteLine(tmrwTemp);

            var tempList = new List<WeatherSetter>();
            /*foreach (JToken result in JsonData)
            {
                WeatherSetter apiThing = result.ToObject<WeatherSetter>();
                tempList.Add(apiThing);
            }*/
            tempList.Add(new WeatherSetter { Temperature = tmrwTemp, Icon = image, Date = DateTime.Now.AddDays(1).ToString()  });
            //tempList.Add(new WeatherSetter { Temperature = "21" });
            //tempList.Add(new WeatherSetter { Temperature = "20" });
            //tempList.Add(new WeatherSetter { Temperature = "12"});
            //tempList.Add(new WeatherSetter { Temperature = "17" });
            //tempList.Add(new WeatherSetter { Temperature = "20", Date = "Friday 21", Icon = "weather.png" });

            return tempList;

            //https://www.newtonsoft.com/json/help/html/SerializingJSONFragments.htm
            /*IList<WeatherSetter> apiThings = new List<WeatherSetter>();
            foreach (JToken result in results)
            {
                WeatherSetter apiThing = result.ToObject<WeatherSetter>();
                apiThings.Add(apiThing);
            }
            return apiThings;*/
        }

        public async Task<JObject> FindWeather()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync("https://api.weather.gov/gridpoints/CLE/117,46/forecast");
            JObject results = JObject.Parse(response);
            return results;
        }

        async void Menu_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ODOT());
        }

        /*public async void Locate(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    lat.Text += location.Latitude.ToString();
                    longr.Text += location.Longitude.ToString();
                    alt.Text += location.Altitude.ToString();
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Console.WriteLine(fnsEx);
                // Handle not supported on device exception  
            }
            catch (PermissionException pEx)
            {
                Console.WriteLine(pEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //        try
            //        {

            //            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            //            var location = await Geolocation.GetLocationAsync(request);

            //            System.Diagnostics.Debug.WriteLine(location.ToString());
            //            new PlaceVars { Position = location.ToString() };

            //            if (location != null)
            //            {
            //                System.Diagnostics.Debug.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
            //            }
            //        }
            //        catch (FeatureNotSupportedException fnsEx)
            //        {
            //            Console.WriteLine(fnsEx);
            //        }
            //        catch (FeatureNotEnabledException fneEx)
            //        {
            //            Console.WriteLine(fneEx);
            //        }
            //        catch (PermissionException pEx)
            //        {
            //            Console.WriteLine(pEx);
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex);
            //        }
            //    }
        }*/
    }

    public class WeatherSetter
    {
        public string Temperature { get; set; }
        public string Date { get; set; }
        public string Icon { get; set; }

        //public string ShortForcast { get; set; }
    }

    public class LocationVal
    {
        public string coord { get; set; }
        public string gridp { get; set; }
    }

}
