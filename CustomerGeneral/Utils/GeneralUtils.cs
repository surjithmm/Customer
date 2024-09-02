using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace CustomerGeneral.Utils
{
    public static class GeneralUtils
    {
        private static string DateFormatForView = "dd-MMM-yyyy";
        private static string DateFormatForEdit = "dd-MM-yyyy";
        private static string DateTimeFormatForEdit = "dd-MM-yyyy hh:mm tt";
        private static string DateTimeFormatForView = "dd-MMM-yyyy hh:mm tt";
        private static string DateTimeExternalFormat = "yyyy-MM-ddTHH:mm:ss";
        private static string ToDateTimeExternalFormat = "MM/dd/yyyy HH:mm:ss";
        private static string TimeFormatEdit = "hh:mm tt";
        public static TimeSpan ToTimeNULL(string time)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(time))
                    throw new ArgumentException("Input time cannot be null or empty.");

                int hours = 0;
                int minutes = 0;
                int seconds = 0;

                string[] timeParts = time.Split(' ');
                string[] hmsParts = timeParts[0].Split(':');

                hours = Convert.ToInt32(hmsParts[0]);
                minutes = Convert.ToInt32(hmsParts[1]);

                if (hmsParts.Length > 2)
                {
                    seconds = Convert.ToInt32(hmsParts[2]);
                }

                if (timeParts.Length > 1)
                {
                    string period = timeParts[1].Trim().ToLower();
                    if (period == "PM" && hours < 12)
                    {
                        hours += 12;
                    }
                    else if (period == "AM" && hours == 12)
                    {
                        hours = 0;
                    }
                }

                return new TimeSpan(hours, minutes, seconds);
            }
            catch (FormatException e)
            {
                throw new FormatException("Invalid time format.", e);
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while parsing the time.", e);
            }
        }
        public static string FormatTimeSpanNULL(TimeSpan? time, bool use24HourFormat = false)
        {
            if (time == null)
            {
                return null;
            }
            if (use24HourFormat)
            {
                // Return time in 24-hour format
                return time.Value.ToString(@"hh\:mm\:ss");
            }
            else
            {
                // Return time in 12-hour format with AM/PM
                DateTime dateTime = DateTime.Today.Add(time.Value);
                return dateTime.ToString(TimeFormatEdit);
            }
        }
        public static DateTime ToDateTime(string date, string type = "date")
        {
            string format = DateFormatForEdit;
            if (type == "datetime")
            {
                format = DateTimeFormatForEdit;
            }

            try
            {
                var r = DateTime.ParseExact(
                    s: date,
                    format: format,
                    provider: null);
                return r;
            }
            catch (FormatException e)
            {
                throw e;
            }

        }

        public static DateTime? ToDateTimeNull(string date, string type = "date")
        {
            string format = DateFormatForEdit;
            if (type == "datetime")
            {
                format = DateTimeFormatForEdit;
            }

            try
            {
                var r = DateTime.ParseExact(
                    s: date,
                    format: format,
                    provider: null);
                return r;
            }
            catch (FormatException e)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static string FormatDateNull(DateTime? date, string mode = "edit")
        {
            if (date == null)
            {
                return "";
            }
            if (mode.Equals("view"))
            {
                return ((DateTime)date).ToString(DateFormatForView);
            }
            else
            {
                return ((DateTime)date).ToString(DateFormatForEdit);
            }

        }
        public static DateTime ExternalFormmatToDateTime(string date)
        {
            try
            {

                var r = DateTime.ParseExact(
                    s: date,
                    format: ToDateTimeExternalFormat,
                    provider: null);
                return r;
            }
            catch (FormatException e)
            {
                throw e;
            }

        }
        public static string ExternalFormatFormatDate(DateTime date)
        {
            return date.ToString(DateTimeExternalFormat);

        }
        public static string FormatDate(DateTime date, string mode = "edit")
        {
            if (mode.Equals("view"))
            {
                return date.ToString(DateFormatForView);
            }
            else
            {
                return date.ToString(DateFormatForEdit);
            }

        }

        public static string FormatDateTime(DateTime date, string mode = "edit")
        {
            if (mode.Equals("view"))
            {
                return date.ToString(DateTimeFormatForView);
            }
            else
            {
                return date.ToString(DateTimeFormatForEdit);
            }

        }

        public static string FormatDateTimeNull(DateTime? date, string mode = "edit")
        {
            if (date == null)
            {
                return "";
            }
            if (mode.Equals("view"))
            {
                return ((DateTime)date).ToString(DateTimeFormatForView);
            }
            else
            {
                return ((DateTime)date).ToString(DateTimeFormatForEdit);
            }

        }
        public static List<string> ToStringArrayList(this string stringArray)
        {

            try
            {
                List<string> ArrayList = stringArray.Split(',').ToList();
                for (int i = 0; i < ArrayList.Count; i++)
                {
                    ArrayList[i] = HttpUtility.HtmlDecode(ArrayList[i]);
                }
                return ArrayList;
            }
            catch
            {
                return new List<string>();
            }

        }
        public static string JsonDataFromList<T>(this List<T> list)
        {
            try
            {

                return JsonConvert.SerializeObject(list);
            }
            catch
            {
                return "[{}]";
            }
        }
        public static List<T> JsonDataToList<T>(this string JsonData)
        {
            try
            {

                return JsonConvert.DeserializeObject<List<T>>(JsonData);
            }
            catch
            {
                return new List<T>();
            }

        }
        public static List<int> ToIntArrayList(this string intArray)
        {

            try
            {
                return intArray.Split(',').Select(x => Int32.Parse(x)).ToList();
            }
            catch
            {
                return new List<int>();
            }

        }
        public static string FromArrayList(this List<int> ArrayList)
        {

            try
            {
                return string.Join(",", ArrayList);
            }
            catch
            {
                return "";
            }

        }
        public static string FromArrayList(this List<string> ArrayList)
        {

            try
            {
                for (int i = 0; i < ArrayList.Count; i++)
                {
                    ArrayList[i] = HttpUtility.HtmlDecode(ArrayList[i]);
                }
                return string.Join(",", ArrayList);
            }
            catch
            {
                return "";
            }

        }

        public static string Split(string s)
        {
            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);
            return r.Replace(s, " ");
        }
        public static DateTime FirstDayOfMonth
        {
            get
            {
                DateTime Today = DateTime.Today;
                return new DateTime(Today.Year, Today.Month, 1);
            }
        }
    }
}
