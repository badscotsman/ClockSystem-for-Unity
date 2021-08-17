using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.ComponentModel;
using SimpleJSON;
using Clocks;

namespace WorldTimeAPIOrg
{
    /// <summary>
    /// http://worldtimeapi.org/
    /// </summary>
    public class WorldTimeAPI : MonoBehaviour, ITimeProvider
    {
        public event Action<DateTime> ChangeTimeZone;

        public enum TimeZones
        {
            [Description("Africa/Cairo")]
            Africa_Cairo,
            [Description("Africa/Johannesburg")]
            Africa_Johannesburg,
            [Description("Africa/Nairobi")]
            Africa_Nairobi,
            [Description("America/Anchorage")]
            America_Anchorage,
            [Description("America/Argentina_Buenos_Aires")]
            America_Argentina_Buenos_Aires,
            [Description("America/Argentina_San_Juan")]
            America_Argentina_San_Juan,
            [Description("America/Barbados")]
            America_Barbados,
            [Description("America/Belize")]
            America_Belize,
            [Description("America/Bogota")]
            America_Bogota,
            [Description("America/Cancun")]
            America_Cancun,
            [Description("America/Chicago")]
            America_Chicago,
            [Description("America/Costa_Rica")]
            America_Costa_Rica,
            [Description("America/Denver")]
            America_Denver,
            [Description("America/Detroit")]
            America_Detroit,
            [Description("America/El_Salvador")]
            America_El_Salvador,
            [Description("America/Guatemala")]
            America_Guatemala,
            [Description("America/Havana")]
            America_Havana,
            [Description("America/Indiana_Indianapolis")]
            America_Indiana_Indianapolis,
            [Description("America/Jamaica")]
            America_Jamaica,
            [Description("America/Los_Angeles")]
            America_Los_Angeles,
            [Description("America/Mexico_City")]
            America_Mexico_City,
            [Description("America/Nassau")]
            America_Nassau,
            [Description("America/New_York")]
            America_New_York,
            [Description("America/Panama")]
            America_Panama,
            [Description("America/Phoenix")]
            America_Phoenix,
            [Description("America/Puerto_Rico")]
            America_Puerto_Rico,
            [Description("America/Santiago")]
            America_Santiago,
            [Description("America/Sao_Paulo")]
            America_Sao_Paulo,
            [Description("America/Tijuana")]
            America_Tijuana,
            [Description("America/Toronto")]
            America_Toronto,
            [Description("America/Vancouver")]
            America_Vancouver,
            [Description("America/Winnipeg")]
            America_Winnipeg,
            [Description("Antarctica/Casey")]
            Antarctica_Casey,
            [Description("Asia/Baghdad")]
            Asia_Baghdad,
            [Description("Asia/Bangkok")]
            Asia_Bangkok,
            [Description("Asia/Beirut")]
            Asia_Beirut,
            [Description("Asia/Brunei")]
            Asia_Brunei,
            [Description("Asia/Damascus")]
            Asia_Damascus,
            [Description("Asia/Dubai")]
            Asia_Dubai,
            [Description("Asia/Gaza")]
            Asia_Gaza,
            [Description("Asia/Ho_Chi_Minh")]
            Asia_Ho_Chi_Minh,
            [Description("Asia/Hong_Kong")]
            Asia_Hong_Kong,
            [Description("Asia/Jakarta")]
            Asia_Jakarta,
            [Description("Asia/Jerusalem")]
            Asia_Jerusalem,
            [Description("Asia/Kathmandu")]
            Asia_Kathmandu,
            [Description("Asia/Kolkata")]
            Asia_Kolkata,
            [Description("Asia/Kuala_Lumpur")]
            Asia_Kuala_Lumpur,
            [Description("Asia/Macau")]
            Asia_Macau,
            [Description("Asia/Manila")]
            Asia_Manila,
            [Description("Asia/Qatar")]
            Asia_Qatar,
            [Description("Asia/Seoul")]
            Asia_Seoul,
            [Description("Asia/Shanghai")]
            Asia_Shanghai,
            [Description("Asia/Singapore")]
            Asia_Singapore,
            [Description("Asia/Taipei")]
            Asia_Taipei,
            [Description("Asia/Tehran")]
            Asia_Tehran,
            [Description("Asia/Tokyo")]
            Asia_Tokyo,
            [Description("Atlantic/Bermuda")]
            Atlantic_Bermuda,
            [Description("Australia/Brisbane")]
            Australia_Brisbane,
            [Description("Australia/Melbourne")]
            Australia_Melbourne,
            [Description("Australia/Perth")]
            Australia_Perth,
            [Description("Australia/Sydney")]
            Australia_Sydney,
            [Description("Europe/Amsterdam")]
            Europe_Amsterdam,
            [Description("Europe/Athens")]
            Europe_Athens,
            [Description("Europe/Belgrade")]
            Europe_Belgrade,
            [Description("Europe/Berlin")]
            Europe_Berlin,
            [Description("Europe/Brussels")]
            Europe_Brussels,
            [Description("Europe/Bucharest")]
            Europe_Bucharest,
            [Description("Europe/Budapest")]
            Europe_Budapest,
            [Description("Europe/Copenhagen")]
            Europe_Copenhagen,
            [Description("Europe/Dublin")]
            Europe_Dublin,
            [Description("Europe/Helsinki")]
            Europe_Helsinki,
            [Description("Europe/Istanbul")]
            Europe_Istanbul,
            [Description("Europe/Kaliningrad")]
            Europe_Kaliningrad,
            [Description("Europe/Kiev")]
            Europe_Kiev,
            [Description("Europe/Kirov")]
            Europe_Kirov,
            [Description("Europe/Lisbon")]
            Europe_Lisbon,
            [Description("Europe/London")]
            Europe_London,
            [Description("Europe/Luxembourg")]
            Europe_Luxembourg,
            [Description("Europe/Madrid")]
            Europe_Madrid,
            [Description("Europe/Malta")]
            Europe_Malta,
            [Description("Europe/Minsk")]
            Europe_Minsk,
            [Description("Europe/Monaco")]
            Europe_Monaco,
            [Description("Europe/Moscow")]
            Europe_Moscow,
            [Description("Europe/Oslo")]
            Europe_Oslo,
            [Description("Europe/Paris")]
            Europe_Paris,
            [Description("Europe/Prague")]
            Europe_Prague,
            [Description("Europe/Rome")]
            Europe_Rome,
            [Description("Europe/Samara")]
            Europe_Samara,
            [Description("Europe/Saratov")]
            Europe_Saratov,
            [Description("Europe/Stockholm")]
            Europe_Stockholm,
            [Description("Europe/Ulyanovsk")]
            Europe_Ulyanovsk,
            [Description("Europe/Vienna")]
            Europe_Vienna,
            [Description("Europe/Warsaw")]
            Europe_Warsaw,
            [Description("Europe/Zurich")]
            Europe_Zurich,
            [Description("Indian/Christmas")]
            Indian_Christmas,
            [Description("Indian/Cocos")]
            Indian_Cocos,
            [Description("Indian/Maldives")]
            Indian_Maldives,
            [Description("Pacific/Auckland")]
            Pacific_Auckland,
            [Description("Pacific/Chatham")]
            Pacific_Chatham,
            [Description("Pacific/Easter")]
            Pacific_Easter,
            [Description("Pacific/Fakaofo")]
            Pacific_Fakaofo,
            [Description("Pacific/Fiji")]
            Pacific_Fiji,
            [Description("Pacific/Galapagos")]
            Pacific_Galapagos,
            [Description("Pacific/Guam")]
            Pacific_Guam,
            [Description("Pacific/Honolulu")]
            Pacific_Honolulu,
            [Description("Pacific/Pago_Pago")]
            Pacific_Pago_Pago,
            [Description("Pacific/Tahiti")]
            Pacific_Tahiti,
            [Description("Etc/GMT")]
            Etc_GMT,
            [Description("Etc/GMT+1")]
            Etc_GMTplus1,
            [Description("Etc/GMTplus2")]
            Etc_GMTplus2,
            [Description("Etc/GMTplus3")]
            Etc_GMTplus3,
            [Description("Etc/GMTplus4")]
            Etc_GMTplus4,
            [Description("Etc/GMTplus5")]
            Etc_GMTplus5,
            [Description("Etc/GMTplus6")]
            Etc_GMTplus6,
            [Description("Etc/GMTplus7")]
            Etc_GMTplus7,
            [Description("Etc/GMTplus8")]
            Etc_GMTplus8,
            [Description("Etc/GMTplus9")]
            Etc_GMTplus9,
            [Description("Etc/GMTplus10")]
            Etc_GMTplus10,
            [Description("Etc/GMTplus11")]
            Etc_GMTplus11,
            [Description("Etc/GMTplus12")]
            Etc_GMTplus12,
            [Description("Etc/GMT-1")]
            Etc_GMTminus1,
            [Description("Etc/GMTminus2")]
            Etc_GMTminus2,
            [Description("Etc/GMTminus3")]
            Etc_GMTminus3,
            [Description("Etc/GMTminus4")]
            Etc_GMTminus4,
            [Description("Etc/GMTminus5")]
            Etc_GMTminus5,
            [Description("Etc/GMTminus6")]
            Etc_GMTminus6,
            [Description("Etc/GMTminus7")]
            Etc_GMTminus7,
            [Description("Etc/GMTminus8")]
            Etc_GMTminus8,
            [Description("Etc/GMTminus9")]
            Etc_GMTminus9,
            [Description("Etc/GMTminus10")]
            Etc_GMTminus10,
            [Description("Etc/GMTminus11")]
            Etc_GMTminus11,
            [Description("Etc/GMTminus12")]
            Etc_GMTminus12,
            [Description("Etc/GMTminus13")]
            Etc_GMTminus13,
            [Description("Etc/GMTminus14")]
            Etc_GMTminus14,
            [Description("Etc/UTC")]
            Etc_UTC,
            [Description("CET")]
            CET,
            [Description("CST6CDT")]
            CST6CDT,
            [Description("EET")]
            EET,
            [Description("EST")]
            EST,
            [Description("EST5EDT")]
            EST5EDT,
            [Description("HST")]
            HST,
            [Description("MET")]
            MET,
            [Description("MST")]
            MST,
            [Description("MST7MDT")]
            MST7MDT,
            [Description("PST8PDT")]
            PST8PDT
        }
        [SerializeField]
        private TimeZones _timeZone = TimeZones.America_Los_Angeles;

        private const string URI = "http://worldtimeapi.org/api/";


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                StartCoroutine(IEGetCurrentWorldTime(SetCurrentWorldTime));


                void SetCurrentWorldTime(DateTime dateTimeCurrent)
                {
                    ChangeTimeZone?.Invoke(dateTimeCurrent);
                }
            }
        }
#endif


        public void GetTime(Action<DateTime> callback)
        {
            StartCoroutine(IEGetCurrentWorldTime(SetCurrentWorldTime));


            void SetCurrentWorldTime(DateTime dateTimeCurrent)
            {
                callback?.Invoke(dateTimeCurrent);
            }
        }

        private IEnumerator IEGetCurrentWorldTime(Action<DateTime> callback)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(string.Concat(URI, _timeZone.ToDescriptionString())))
            {
                yield return webRequest.SendWebRequest();

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError($"Error: {webRequest.error}");

                        callback.Invoke(DateTime.MinValue);
                        break;

                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError($"HTTP Error: {webRequest.error}");

                        callback.Invoke(DateTime.MinValue);
                        break;

                    case UnityWebRequest.Result.Success:
                        //Debug.Log($"\nReceived: {webRequest.downloadHandler.text}");

                        var timeData = JSON.Parse(webRequest.downloadHandler.text);
                        var dto = DateTimeOffset.Parse(timeData["datetime"].Value, null);
                        callback.Invoke(dto.DateTime);
                        break;
                }
            }
        }
    }



    public static class TimeZonesEnumExtensions
    {
        public static string ToDescriptionString(this WorldTimeAPI.TimeZones val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}