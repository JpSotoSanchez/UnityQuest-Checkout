using System;

[System.Serializable]
public class KitData
{
    public int kitNumber;
    public int available; // 0 available, 1 charging, 2 unavailable, 3 incomplete
    public bool complete;

    public int year;
    public int month;
    public int day;
    public int hour;
    public int minute;
    public int second;

    // Converts the stored fields to a DateTime (MinValue if the date is invalid)
    public DateTime GetDate()
    {
        if (year < 1 || year > 9999 || month < 1 || month > 12)
        {
            return DateTime.MinValue;
        }
        if (day < 1 || day > DateTime.DaysInMonth(year, month))
        {
            return DateTime.MinValue;
        }
        return new DateTime(year, month, day, hour, minute, second);
    }

    // Stores a DateTime in the fields
    public void SetDate(DateTime date)
    {
        year = date.Year;
        month = date.Month;
        day = date.Day;
        hour = date.Hour;
        minute = date.Minute;
        second = date.Second;
    }
}