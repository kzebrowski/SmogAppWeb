using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmogAppWeb.Models
{
    public class MeasurementModel
    {
        [Required]
        private LocationModel location;
        [Required]
        private String stationName;
        private String mesureTime;
        private double aqi; //air quality index
        private double ozone;
        private double pm25;
        private double pm100;
        private double nitroDioxide;

        public LocationModel Location {get ;set; }
        public String StationName { get; set; }
        public String MesureTime { get; set; }
        public double Aqi { get; set; }
        public double Ozone { get; set; }
        public double Pm25 { get; set; }
        public double Pm100 { get; set; }
        public double NitroDioxide { get; set; }
    }

    public class LocationModel
    {
        [Required]
        private double latitude;
        [Required]
        private double longtitude;
        [Required(ErrorMessage = "Pole adresu nie może być puste.")]
        private String address;

        public double Latitude { get; set;}
        public double Longtitude { get; set; }
        public String Address{ get; set; }
    }
}