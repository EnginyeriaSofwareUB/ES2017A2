using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TimerGame : MonoBehaviour
{
    private Timer aTimer;
    private bool timeOver;
    private int limit;
    [SerializeField] private double currentTime;
    private bool running;

    public bool TimeOver {
        get {
            return this.timeOver;
        }

        set {
            this.timeOver = value;
        }
    }

    /**
     * Metodo que inicializa el timer
     */
    public void init(int seconds)
    {
        this.limit = seconds;
        this.currentTime = 0;
        this.TimeOver = false;
        this.running = true;
        //Debug.Log("2 constructor limit = " + this.limit + " curr = " + this.currentTime);
    }

    private void FixedUpdate()
    {
        if (running)
        {
            currentTime += Time.deltaTime;
            Debug.Log("currentTime: " + (int) currentTime + ",  limit = " + this.limit);
            this.TimeOver = this.currentTime >= this.limit;
        }
    }

    /**
     * Metodo que para el timer
     */
    public void stop() {
        this.running = false;
    }

    /**
     * Metodo que resetea el timer con los s iniciales
     */
    public void reset() {
        init(this.limit);
    }

    /**
     * Metodo que resetea el timer con los s dados por parametros
     */
    public void reset(int seconds) {
        TimeOver = false;
        init(seconds);
    }

	public double getTimeLeft(){
		return this.currentTime;
	}
}
