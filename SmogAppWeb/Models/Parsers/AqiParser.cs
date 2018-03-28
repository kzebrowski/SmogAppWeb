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
    public class AqiParser
    {
        private static String ApiKey = "b400ae316b49cb64eadabb9f033eaf26f67b2b08";
        private JObject json;
        private MeasurementModel measurement;

        public MeasurementModel ParseAirQuality(LocationModel location)
        {
            String lat = location.Latitude.ToString().Replace(",", "."); 
            String lon = location.Longtitude.ToString().Replace(",", ".");
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            String json = client.DownloadString("http://api.waqi.info/feed/geo:" + lat + ";" + lon + "/?token=" + ApiKey);
            this.json = JObject.Parse(json);
            this.measurement = new MeasurementModel();
            measurement.Location = location;

            SetMeasurementParameters(); //while no data parameter will be set to -1 
            return measurement;
        }

        private void SetMeasurementParameters()
        {
            ParseStationName();
            ParseAqi();
            ParseMesureTime();
            ParseOzone();
            ParsePm25();
            ParsePm100();
            ParseNitroDioxide();
        }

        private void ParseStationName()
        {
            String name = (string)json.SelectToken("data.city.name");
            if (name == null) name = "Brak danych";
            measurement.StationName = name;
        }

        public void ParseAqi()
        {
            String aqi = (string)json.SelectToken("data.aqi");
            
            measurement.Aqi = parseStringToDecimal(aqi);
        }

        private void ParseMesureTime()
        {
            String time = (string)json.SelectToken("data.time.s");
            if (time == null)
            {
                time = "Brak danych";
                return;
            }
            measurement.MesureTime = time;
        }

        private void ParseNitroDioxide()
        {
            String no2 = (string)json.SelectToken("data.iaqi.no2.v");

            measurement.NitroDioxide = parseStringToDecimal(no2); 
        }

        private void ParsePm100()
        {
            String pm100 = (string)json.SelectToken("data.iaqi.pm10.v");

            measurement.Pm100 = parseStringToDecimal(pm100); 
        }

        private void ParsePm25()
        {
            String pm25 = (string)json.SelectToken("data.iaqi.pm25.v");

            measurement.Pm25 = parseStringToDecimal(pm25);
        }

        private void ParseOzone()
        {
            String o3 = (string)json.SelectToken("data.iaqi.o3.v");

            measurement.Ozone = parseStringToDecimal(o3);
        }

        private double parseStringToDecimal(String input)
        {
            double tmp;
            if (double.TryParse(input, NumberStyles.Any, new CultureInfo("en-US"), out tmp))
            {
                return tmp;
            }
            return -1;
        }
    }
}