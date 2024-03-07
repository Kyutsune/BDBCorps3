using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer 
{
    public float secondes;
    public float minutes;


    public float Secondes
    {
        get { return secondes; }
        set { secondes = value; }
    }

    public void Initialisation_Timer(int temps_secondes=0,int temps_minutes=0)
    {
        secondes=temps_secondes;
        minutes=temps_minutes;
    }


    public void increments_temps()
    {
        secondes += Time.deltaTime;
        if(secondes>=60)
        {
            secondes=0;
            minutes++;
        }
    }


    public void Affichage_temps_console()
    {
        Debug.Log("minutes = "+minutes+" secondes = "+secondes);
    }


    public void Deroulement_timer_console(bool affichage_temps)
    {
        increments_temps();
        if(affichage_temps)
            Affichage_temps_console();
    }
}
