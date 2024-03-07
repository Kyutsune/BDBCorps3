using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Classe Timer dans laquelle on met à jour un compteur qui nous permet de traquer le temps passé 
*/



//Ce qu'il reste a faire dans cette classe c'est d'afficher graphiquement le timer
public class Timer
{
    public float secondes;
    public float minutes;
    private bool displayTime;



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
        PlayerPrefs.SetFloat("secondes_ecoulees", secondes);
        PlayerPrefs.SetFloat("minutes_ecoulees", minutes);
        PlayerPrefs.Save();
    }


    public void Affichage_temps_console()
    {
        Debug.Log("minutes = "+minutes+" secondes = "+secondes);
    }

    void OnGUI()
    {
        if (displayTime)
        {
            GUI.Label(new Rect(10, 10, 100, 20), string.Format("{0:00}:{1:00}", minutes, secondes));
            displayTime = false; // Reset the flag after rendering
        }
    }





    public void Deroulement_timer_console(bool affichage_temps)
    {
        increments_temps();
        if(affichage_temps)
            Affichage_temps_console();
        displayTime = true;
    }
}
