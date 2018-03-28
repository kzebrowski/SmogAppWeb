using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace SmogAppWeb.Models.Parsers
{
    public class GeocodingParser
    {
        private static String ApiKey = "AIzaSyBTzON4e8Wx3rsb_KcarsjeVgtMxPdOGg8";
        private JObject json;
        private LocationModel location;

        public LocationModel GeocodeFromAddress(String address)
        {
            address = address.Replace(" ", "%20");
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            String json = client.DownloadString("https://maps.googleapis.com/maps/api/geocode/json?&address=" + address + "&key=" + ApiKey);
            this.json = JObject.Parse(json);
            this.location = new LocationModel();

            SetLocationLatitude();
            SetLocationLongtitude();
            SetLocationAddress();

            return location;
        }

        private void SetLocationLatitude()
        {
            String lat = (string)json.SelectToken("results[0].geometry.location.lat");
            if (lat == null)
            {
                throw new FormatException();
                return;
            }
            
            location.Latitude = Double.Parse(lat, new CultureInfo("en-US"));
        }

        private void SetLocationLongtitude()
        {
            String lng = (string)json.SelectToken("results[0].geometry.location.lng");
            if (lng == null)
            {
                throw new FormatException();
                return;
            }
            
            location.Longtitude = Double.Parse(lng, new CultureInfo("en-US"));
        }

        private void SetLocationAddress()
        {
            location.Address = (string)json.SelectToken("results[0].formatted_address");
        }
    }
}