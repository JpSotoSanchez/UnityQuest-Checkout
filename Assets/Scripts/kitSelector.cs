using System;
using System.IO;
using UnityEngine;

public class kitSelector
{
    private int kitSelected;
    public int available; 
    public bool complete;

    public int year;
    public int month;
    public int day;
    public int hour;
    public int minute;
    public int second;

    static void KitSelected(int n)
    {
        this.kit = n;
    }
    void AvailableSelection()
    {
        
    }
    void CompleteCheckbox()
    {
        
    }

    static void YearSelected(int n)
    {
        this.year = n;
    }
    void MonthSelected(int n)
    {
        
    }
    void DaySelected(int n)
    {
        
    }
    void HourSelected(int n)
    {
        
    }
    void MinuteSelected(int n)
    {
        
    }
    void SecondSelected(int n)
    {
        
    }

}