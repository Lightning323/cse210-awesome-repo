namespace FinalProject;

using Microsoft.Recognizers.Text.DateTime;
using System;
using System.Collections.Generic;
using Microsoft.Recognizers.Text;

public class TimeUtils
{
    public const string CULTURE = Culture.English;
    public const string DATE_FORMAT = "yyyy-MM-dd hh:mm:ss tt";

    public static DateTime? StringToDateTime(string dateTimeString)
    {
        List<ModelResult> results = DateTimeRecognizer.RecognizeDateTime(dateTimeString, CULTURE);
        foreach (ModelResult result in results)
        {
            if (result.Resolution != null && result.Resolution.TryGetValue("values", out object valuesObj))
            {
                if (valuesObj is List<Dictionary<string, string>> { Count: > 0 } valuesList)
                {
                    var resolution = valuesList[0];

                    //For single date
                    resolution.TryGetValue("value", out string dateValue);
                    if (dateValue != null)
                    {
                        DateTime.TryParse(dateValue, out DateTime dt);
                        return dt;
                    }

                    //For date range ("next week" is too vauge to pinpoint to a single date)
                    resolution.TryGetValue("start", out string startValue);
                    if (startValue != null)
                    {
                        DateTime.TryParse(startValue, out DateTime dt);
                        return dt;
                    }
                }
            }
        }

        return null;
    }

    public record DateTimeRange(DateTime start, DateTime end);

    public static List<DateTimeRange> StringToDateTimeRanges(string dateTimeString)
    {
        var ranges = new List<DateTimeRange>();

        List<ModelResult> results =
            DateTimeRecognizer.RecognizeDateTime(dateTimeString, CULTURE);

        foreach (var result in results)
        {
            if (result.Resolution == null ||
                !result.Resolution.TryGetValue("values", out object valuesObj))
                continue;

            if (valuesObj is not List<Dictionary<string, string>> valuesList)
                continue;

            foreach (var resolution in valuesList)
            {
                if (resolution.TryGetValue("value", out string value))
                {
                    if (DateTime.TryParse(value, out DateTime dt))
                        ranges.Add(new DateTimeRange(dt, dt));
                }
                else if (
                    resolution.TryGetValue("start", out string start) &&
                    resolution.TryGetValue("end", out string end))
                {
                    if (DateTime.TryParse(start, out DateTime dtStart) &&
                        DateTime.TryParse(end, out DateTime dtEnd))
                    {
                        ranges.Add(new DateTimeRange(dtStart, dtEnd));
                    }
                }
            }
        }

        return ranges;
    }
}