using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Affichagedespvs 
{
    
    public GameObject Texte_pv;


    public void MettreAJourTextePV(double pv)
    {

        // Mettez à jour le texte avec les PVs fournis
        Texte_pv.GetComponent<Text>().text = "pv du héro : " + pv; //Permet d'afficher ce que le joueur a entré directement sur le jeu
        

    }

}
