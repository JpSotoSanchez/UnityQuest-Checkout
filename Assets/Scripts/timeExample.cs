using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeExample : MonoBehaviour
{
    public double totalGameSeconds;
    public Text time;

    public double seconds;
    public double minutes;
    public double hours;
    public double days;
    public double months;
    public double years;

    private double secondsPerSecond;

 

    void Start()
    {

        secondsPerSecond = 3600;
        totalGameSeconds += secondsPerSecond * Time.deltaTime;


    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            secondsPerSecond = 3600;


        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            secondsPerSecond = 86400;
        }
        totalGameSeconds += secondsPerSecond * Time.deltaTime;

        seconds = totalGameSeconds;
        minutes = totalGameSeconds / 60;
        hours = minutes / 60;
        days = hours / 24;
        months = days / (365 / 12);
        years = months / 12;

        time.text = (days + months + years).ToString();
       

    }

  
}