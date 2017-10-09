using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TimerGame : MonoBehaviour
{

    private Timer aTimer;
    [SerializeField] private int miliseconds;
    [SerializeField] private bool timeOver;

    public TimerGame(int milis)
    {
        miliseconds = milis;
        timeOver = false;
        init();

        //Console.WriteLine("\n_______________ Press the Enter key to exit the application...\n");
        //Console.WriteLine("_______________ The application started at {0:HH:mm:ss.fff}", DateTime.Now);
        //while (Console.ReadLine() == null) ;
    }

    private void init()
    {
        // Create a timer with a two second interval.
        aTimer = new Timer(miliseconds);
        // Hook up the Elapsed event for the timer. 
        aTimer.Elapsed += OnTimedEvent;
        aTimer.AutoReset = true;
        aTimer.Enabled = true;
    }


    public void start() {
        aTimer.Start();
    }

    public void stop() {
        aTimer.Stop();
        //aTimer.Dispose();
    }

    public void reset() {
        timeOver = false;
        init();
    }

    public void reset(int milis) {
        miliseconds = milis;
        timeOver = false;
        init();
    }

    public bool isTimeOver() {
        return timeOver;
    }


    private void OnTimedEvent(System.Object source, ElapsedEventArgs e)
    {
        //Console.WriteLine("_______________ The Elapsed event was raised at {0:HH:mm:ss.fff}",
        //                e.SignalTime);
        timeOver = true;
    }
}
