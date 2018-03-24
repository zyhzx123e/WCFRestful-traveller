using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace wcfapi.Models
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public class weather
    {
        [DataMember]
        public string nowTempC { get; set; }//1

        [DataMember]
        public string todayDate { get; set; }//2

        [DataMember]
        public string todayMaxTempC { get; set; }//3

        [DataMember]
        public string todayMinTempC { get; set; }//4

        [DataMember]
        public string todaySunRiseTime { get; set; }//5

        [DataMember]
        public string todaySunSetTime { get; set; }//6

        [DataMember]
        public string todayHumidity { get; set; }//7

        [DataMember]
        public string todayWeatherDesc { get; set; }//8

        [DataMember]
        public string todayWeatherIconUrl { get; set; }//9

        [DataMember]
        public string todayWindSpeedKmph { get; set; }//10


        [DataMember]
        public string tmrDate { get; set; }//11

        [DataMember]
        public string tmrMaxTempC { get; set; }//12

        [DataMember]
        public string tmrMinTempC { get; set; }//13

        [DataMember]
        public string tmrSunRiseTime { get; set; }//14

        [DataMember]
        public string tmrSunSetTime { get; set; }//15


        public weather(string nowTempC, string todayDate, string todayMaxTempC, string todayMinTempC, string todaySunRiseTime,
            string todaySunSetTime, string todayHumidity, string todayWeatherDesc, string todayWeatherIconUrl,
            string todayWindSpeedKmph, string tmrDate, string tmrMaxTempC, string tmrMinTempC, string tmrSunRiseTime, string tmrSunSetTime)
        {

            this.nowTempC = nowTempC;
            this.todayDate = todayDate;
            this.todayMaxTempC = todayMaxTempC;
            this.todayMinTempC = todayMinTempC;
            this.todaySunRiseTime = todaySunRiseTime;
            this.todaySunSetTime = todaySunSetTime;
            this.todayHumidity = todayHumidity;
            this.todayWeatherDesc = todayWeatherDesc;
            this.todayWeatherIconUrl = todayWeatherIconUrl;
            this.todayWindSpeedKmph = todayWindSpeedKmph;

            this.tmrDate = tmrDate;
            this.tmrMaxTempC = tmrMaxTempC;
            this.tmrMinTempC = tmrMinTempC;
            this.tmrSunRiseTime = tmrSunRiseTime;
            this.tmrSunSetTime = tmrSunSetTime;

        }

        public weather()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public List<weather> UnauthorizedWeather()
        {


            List<weather> lstbal = new List<weather>();

            weather w = new weather("You are not authorized to access the nowTempC data!",
                "You are not authorized to access the todayDate data!",
                "You are not authorized to access the todayMaxTempC data!",
                 "You are not authorized to access the todayMinTempC data!",
                "You are not authorized to access the todaySunRiseTime data!",
                 "You are not authorized to access the todaySunSetTime data!",
                "You are not authorized to access the todayHumidity data!",
                 "You are not authorized to access the todayWeatherDesc data!",
                "You are not authorized to access the todayWeatherDesc data!",
                 "You are not authorized to access the todayWeatherIconUrl data!",
                "You are not authorized to access the todayWindSpeedKmph data!",
                 "You are not authorized to access the tmrMaxTempC data!",
                "You are not authorized to access the tmrMinTempC data!",
                 "You are not authorized to access the tmrSunRiseTime data!",
                "You are not authorized to access the tmrSunSetTime data!");

            /*
             * 
            this.nowTempC = nowTempC;
            this.todayDate = todayDate;
            this.todayMaxTempC = todayMaxTempC;
            this.todayMinTempC = todayMinTempC;
            this.todaySunRiseTime = todaySunRiseTime;
            this.todaySunSetTime = todaySunSetTime;
            this.todayHumidity = todayHumidity;
            this.todayWeatherDesc = todayWeatherDesc;
            this.todayWeatherIconUrl = todayWeatherIconUrl;
            this.todayWindSpeedKmph = todayWindSpeedKmph;

            this.tmrDate = tmrDate;
            this.tmrMaxTempC = tmrMaxTempC;
            this.tmrMinTempC = tmrMinTempC;
            this.tmrSunRiseTime = tmrSunRiseTime;
            this.tmrSunSetTime = tmrSunSetTime;

             */
            lstbal.Add(w);

            return lstbal;

        }

    }
}