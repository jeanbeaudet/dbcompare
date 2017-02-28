﻿

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

#endregion

namespace DataJuggler.Core.UltimateHelper
{

    #region class DateHelper
    /// <summary>
    /// This class has methods to help parse date safely.
    /// </summary>
    public class DateHelper
    {
        
        #region Methods
    
            #region GetFileNameWithTimestamp(string filename)
            /// <summary>
            /// This method Get File Name With Timestamp
            /// </summary>
            public static string GetFileNameWithTimestamp(string filename)
            {
                // initial value
                string fileNameWithTimeStamp = "";

                // If the filename string exists
                if (TextHelper.Exists(filename))
                {
                    // Get the timestamp
                    string timestamp = GetTimestamp(DateTime.Now);

                    // Create a fileInfo object
                    FileInfo fileInfo = new FileInfo(filename);

                    // Get the name of the file
                    string fileNameWithoutExtension = fileInfo.Name.Substring(0, fileInfo.Name.LastIndexOf("."));

                    // set the return value
                    fileNameWithTimeStamp = fileNameWithoutExtension + " - " + timestamp + fileInfo.Extension;

                    // Add the folder back
                    fileNameWithTimeStamp = Path.Combine(fileInfo.DirectoryName, fileNameWithTimeStamp);
                }
                
                // return value
                return fileNameWithTimeStamp;
            }
            #endregion
            
            #region GetTwoDigitDay(DateTime date)
            /// <summary>
            /// This method returns a two digit day for the date given.
            /// </summary>
            /// <param name="date"></param>
            /// <returns></returns>
            public static string GetTwoDigitDay(DateTime date)
            {
                // initial value
                string twoDigitDay = "";

                // set the twoDigitDay
                twoDigitDay = date.Day.ToString();

                // if the Length is less than two
                if (twoDigitDay.Length < 2)
                {
                    // prepend a zero to the Day
                    twoDigitDay = "0" + date.Day.ToString();
                }

                // return value
                return twoDigitDay;
            }
            #endregion

            #region GetTwoDigitHour(DateTime date)
            /// <summary>
            /// This method returns a two digit hour for the date given.
            /// </summary>
            /// <param name="date"></param>
            /// <returns></returns>
            public static string GetTwoDigitHour(DateTime date)
            {
                // initial value
                string twoDigitHour = "";

                // set the twoDigitHour
                twoDigitHour = date.Hour.ToString();

                // if the Length is less than two
                if (twoDigitHour.Length < 2)
                {
                    // prepend a zero to the Hour
                    twoDigitHour = "0" + date.Hour.ToString();
                }

                // return value
                return twoDigitHour;
            }
            #endregion

            #region GetTwoDigitMinute(DateTime date)
            /// <summary>
            /// This method returns a two digit minute for the date given.
            /// </summary>
            /// <param name="date"></param>
            /// <returns></returns>
            public static string GetTwoDigitMinute(DateTime date)
            {
                // initial value
                string twoDigitMinute = "";

                // set the twoDigitMinute
                twoDigitMinute = date.Minute.ToString();

                // if the Length is less than two
                if (twoDigitMinute.Length < 2)
                {
                    // prepend a zero to the Minute
                    twoDigitMinute = "0" + date.Minute.ToString();
                }

                // return value
                return twoDigitMinute;
            }
            #endregion
   
            #region GetTwoDigitMonth(DateTime date)
            /// <summary>
            /// This method returns the month for the date given.
            /// </summary>
            /// <param name="date"></param>
            /// <returns></returns>
            public static string GetTwoDigitMonth(DateTime date)
            {
                // initial value
                string twoDigitMonth = "";

                // set the twoDigitMonth
                twoDigitMonth = date.Month.ToString();

                // if the Length is less than two
                if (twoDigitMonth.Length < 2)
                {
                    // prepend a zero to the month
                    twoDigitMonth = "0" + date.Month.ToString();
                }

                // return value
                return twoDigitMonth;
            }
            #endregion

            #region GetMonthEnd(int year = 0, int month = 0)
            /// <summary>
            /// This method returns date at the end of the month.
            /// <param name="year">The year used to return the start date and end date</param>
            /// <param name="month">The month to return the start date and end date</param>
            /// </summary>
            public static DateTime GetMonthEnd(int year = 0, int month = 0)
            {
                // initial value
                DateTime monthEnd;

                // locals
                int day = 0;
                int hour = 23;
                int min = 59;
                int sec = 59;

                // update the params for Year and Month if not supplied
                if (year == 0)
                {
                    // Set the value for year
                    year = DateTime.Now.Year;
                }

                // Set the value for month
                if (month == 0)
                {
                    // Set the value for month
                    month = DateTime.Now.Year;
                }

                // set the value for day
                day = DateTime.DaysInMonth(year, month);

                // now create the monthEnd date
                monthEnd = new DateTime(year, month, day, hour, min, sec);

                // return value
                return monthEnd;
            }
            #endregion
            
            #region GetMonthStart(int year = 0, int month = 0)
            /// <summary>
            /// This method returns the Month Start
            /// </summary>
            public static DateTime GetMonthStart(int year = 0, int month = 0)
            {
                // initial value
                DateTime monthStart;

                // locals
                int day = 1;
                int hour = 0;
                int min = 0;
                int sec = 0;

                // update the params for Year and Month if not supplied
                if (year == 0)
                {
                    // Set the value for year
                    year = DateTime.Now.Year;
                }

                // Set the value for month
                if (month == 0)
                {
                    // Set the value for month
                    month = DateTime.Now.Year;
                }

                // now create the monthStart date
                monthStart = new DateTime(year, month, day, hour, min, sec);

                // return value
                return monthStart;
            }
            #endregion
            
            #region GetShortDateTime(DateTime date)
            /// <summary>
            /// This method returns the Time and Date of the string given.
            /// </summary>
            public static string GetShortDateTime(DateTime date)
            {
                // initial value
                string time = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

                // return value
                return time;
            }
            #endregion

            #region GetTimestamp(DateTime date)
            /// <summary>
            /// This method returns the date plus the time
            /// </summary>
            public static string GetTimestamp(DateTime date)
            {
                // initial value
                string timestamp = "";

                // Get the month
                string month = GetTwoDigitMonth(date);
                string hour = GetTwoDigitHour(date);
                string day = GetTwoDigitDay(date);
                string minute = GetTwoDigitMinute(date);

                // Create a string builder
                StringBuilder sb = new StringBuilder();

                // Add each section of the timestamp
                sb.Append(month);
                sb.Append(day);
                sb.Append(date.Year);
                sb.Append(hour);
                sb.Append(minute);

                // Setup the Timestamp
                timestamp = sb.ToString();
                
                // return value
                return timestamp;
            }
            #endregion
            
            #region IsAfter(DateTime sourceDate, DateTime targetDate, bool includeTime = true)
            /// <summary>
            /// This method returns true if the TargetDate comes AFTER the sourceDate.
            /// </summary>
            public static bool IsAfter(DateTime sourceDate, DateTime targetDate, bool includeTime = true)
            {
                // initial value
                bool isAfter = false;

                // if time should be Not be included in the comparison
                if (!includeTime)
                {
                    // recreate the sourceDate without the time
                    sourceDate = new DateTime(sourceDate.Year, sourceDate.Month, sourceDate.Day);

                    // recreate the targetDate without the time
                    targetDate = new DateTime(targetDate.Year, targetDate.Month, targetDate.Day);
                }

                // compare just the dates
                isAfter = (sourceDate.Date > targetDate.Date);

                // return value
                return isAfter;
            }
            #endregion

            #region IsBefore(DateTime sourceDate, DateTime targetDate, bool includeTime = true)
            /// <summary>
            /// This method returns true if the sourceDate comes BEFORE the targetDate.
            /// </summary>
            public static bool IsBefore(DateTime sourceDate, DateTime targetDate, bool includeTime = true)
            {
                // initial value
                bool isBefore = false;

                // if time should be Not be included in the comparison
                if (!includeTime)
                {
                    // recreate the sourceDate without the time
                    sourceDate = new DateTime(sourceDate.Year, sourceDate.Month, sourceDate.Day);

                    // recreate the targetDate without the time
                    targetDate = new DateTime(targetDate.Year, targetDate.Month, targetDate.Day);
                }

                // compare just the dates
                isBefore = (sourceDate.Date < targetDate.Date);

                // return value
                return isBefore;
            }
            #endregion

            #region IsDate(string date)
            /// <summary>
            /// This method returns true if the string can be parsed into a date.
            /// </summary>
            /// <param name="date"></param>
            /// <returns></returns>
            public static bool IsDate(string date)
            {
                // initial value
                bool isDate = false;

                // If the date string exists
                if (TextHelper.Exists(date))
                {
                    try
                    {
                        // local
                        DateTime actualDate = new DateTime(1900, 1, 1);

                        // try and parse the date
                        isDate = DateTime.TryParse(date, out actualDate);

                        // if the value for isDate is true
                        if (isDate)
                        {
                            // if the date did not parse to a valid range
                            if ((actualDate.Year < 1900) || (actualDate.Year > DateTime.Now.Year))
                            {
                                // not a valid date
                                isDate = false;
                            }
                        }
                    }
                    catch (Exception error)
                    {
                        // for debugging only
                        string err = error.ToString();
                    }
                }

                // return value
                return isDate;
            }
            #endregion

            #region IsDateInRange(DateTime sourceDate, int allowedDays)
            /// <summary>
            /// This method returns true if the sourceDate is not any older than the allowedDays
            /// </summary>
            public static bool IsDateInRange(DateTime sourceDate, int allowedDays)
            {
                // initial value
                bool isDateInRange = false;

                try
                {
                    // subtract the number of days
                    TimeSpan ts = DateTime.Now.Subtract(sourceDate);

                    // set the return value
                    isDateInRange = (ts.TotalDays <= allowedDays);
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }

                // return value
                return isDateInRange;
            }
            #endregion

            #region IsSameDay(DateTime sourceDate, DateTime targetDate)
            /// <summary>
            /// This method returns true if the targetDate is the same date as the sourceDate.
            /// With this override Time is automatically removed so Time is NOT A FACTOR.
            /// </summary>
            public static bool IsSameDay(DateTime sourceDate, DateTime targetDate)
            {
                // initial value
                bool isSameDay = false;

                // remove time values in case they are included
                sourceDate = new DateTime(sourceDate.Year, sourceDate.Month, sourceDate.Day);
                targetDate = new DateTime(targetDate.Year, targetDate.Month, targetDate.Day);

                // set the return value
                isSameDay = (sourceDate == targetDate);

                // return value
                return isSameDay;
            }
            #endregion

            #region ParseDate(string sourceString, DateTime defaultValue, DateTime errorValue)
            /// <summary>
            /// This method is used to safely parse a string
            /// </summary>
            public static DateTime ParseDate(string sourceString, DateTime defaultValue, DateTime errorValue)
            {
                // initial value
                DateTime returnValue = defaultValue;

                try
                {
                    // if the sourceString exists
                    if (!String.IsNullOrEmpty(sourceString))
                    {
                        // perform the parse
                        returnValue = Convert.ToDateTime(sourceString);
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();

                    // set the value to the errorValue
                    returnValue = errorValue;
                }

                // return value
                return returnValue;
            }
            #endregion
            
        #endregion
            
    }
    #endregion

}