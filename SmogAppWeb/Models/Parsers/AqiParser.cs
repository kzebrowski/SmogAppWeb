using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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

            SetMeasurementParameters();
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
            aqi = aqi.Replace(".", ",");
            double numAqi;
            if (double.TryParse(aqi, out numAqi))
            {
                measurement.Aqi = numAqi;
                return;
            }
            measurement.Aqi = -1;
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
            if (no2 != null && no2.IndexOf(".") != -1)
                no2 = no2.Replace(".", ",");

            double numNo2;
            if (double.TryParse(no2, out numNo2))
            {
                measurement.NitroDioxide = numNo2;
                return;
            }
            measurement.NitroDioxide = -1;
        }

        private void ParsePm100()
        {
            String pm100 = (string)json.SelectToken("data.iaqi.pm10.v");
            if (pm100 != null && pm100.IndexOf(".") != -1)
                pm100 = pm100.Replace(".", ",");

            double numPm100;
            if (double.TryParse(pm100, out numPm100))
            {
                measurement.Pm100 = numPm100;
                return;
            }
            measurement.Pm100 = -1;
        }

        private void ParsePm25()
        {
            String pm25 = (string)json.SelectToken("data.iaqi.pm25.v");
            if (pm25 != null && pm25.IndexOf(".") != -1)
                pm25 = pm25.Replace(".", ",");

            double numPm25;
            if (double.TryParse(pm25, out numPm25))
            {
                measurement.Pm25 = numPm25;
                return;
            }
            measurement.Pm25 = -1;
        }

        private void ParseOzone()
        {
            String o3 = (string)json.SelectToken("data.iaqi.o3.v");
            if (o3 != null && o3.IndexOf(".") != -1)
                o3 = o3.Replace(".", ",");

            double numO3;
            if (double.TryParse(o3, out numO3))
            {
                measurement.Ozone = numO3;
                return;
            }
            measurement.Ozone = -1;
        }
    }
}