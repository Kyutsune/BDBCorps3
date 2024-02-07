using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
Classe unité qui va être celle qui va gérer les personnages sur le terrain
Contient leur position via positionX,positionY
les points de vie via pv
le type d'unité qu'ils sont via alliee_ou_ennemie
le type de l'untité via type_unitee
*/





//Enum qui va nous servir à savoir si l'unité est alliée ou Ennemie
public enum Unite_alliee_ennemie
{
    Allie,
    Ennemie
}

public enum Type_unitee
{
    Melee
}


//Classe unité qui va définir les personnages bougeant sur le terrain
public class Unite
{
    private float positionX = 0;
    private float positionY = 0;
    private double pv;
    private float portee;
    private double vitesseAttaque;
    private float momentDerniereAttaque;
    public Unite_alliee_ennemie alliee_ou_ennemie;
    public Type_unitee type_unitee;

    // Propriétés pour accéder aux données
    public float PositionX
    {
        get { return positionX; }
        set { positionX = value; }
    }

    public float PositionY
    {
        get { return positionY; }
        set { positionY = value; }
    }

    public double Pv
    {
        get { return pv; }
        set { pv = value; }
    }




    //Constructeur par défaut,à utiliser pour les tests de début 
    public Unite()
    {
        positionX = 0;
        positionY = 0;
        pv = 100;
        portee = 100;
        vitesseAttaque = 1;
        momentDerniereAttaque = 0;
        alliee_ou_ennemie = Unite_alliee_ennemie.Allie;
        type_unitee = Type_unitee.Melee;
    }

    public Unite(float newX, float newY, double newPv, float newPortee, double newVitesseAttaque,Unite_alliee_ennemie newAllieeOuEnnemie, Type_unitee newTypeUnitee)
    {
        positionX = newX;
        positionY = newY;
        pv = newPv;
        portee = newPortee;
        vitesseAttaque=newVitesseAttaque;
        alliee_ou_ennemie = newAllieeOuEnnemie;
        type_unitee = newTypeUnitee;
    }

    public void Attaquer(Unite autreUnite)
    {
        long tempsActuel = DateTimeOffset.Now.ToUnixTimeSeconds();
        double tempsDepuisDerniereAttaque = (tempsActuel - momentDerniereAttaque);
        if(tempsDepuisDerniereAttaque >= vitesseAttaque)
        {
            double distance = Math.Sqrt(Math.Pow(autreUnite.PositionX-positionX, 2) + Math.Pow(autreUnite.PositionY-positionY, 2));
            if(distance <= portee)
            {
                autreUnite.Pv=autreUnite.Pv - 1;
                tempsDepuisDerniereAttaque = tempsActuel;
            }
        }
    }


    public void DeplacerVersUniteDifferente(Unite autreUnite,out bool EstEnMouvement)
    {
        // Vérifier si l'autre unité a une Unite_alliee_ennemie différente
        if (autreUnite.alliee_ou_ennemie != this.alliee_ou_ennemie)
        {
            // Définir une distance minimale pour éviter les collisions
            float distanceMinimale = 1.0f;  // Ajustez cette valeur selon votre préférence

            // Calculer la distance entre les deux unités
            float distance = Vector2.Distance(new Vector2(positionX, positionY), new Vector2(autreUnite.PositionX, autreUnite.PositionY));

            // Vérifier si la distance est supérieure à la distance minimale
            if (distance > distanceMinimale)
            {
                // Déplacer vers l'autre unité en utilisant une approche de lissage linéaire
                float lissage = 0.001f;  // Ajustez la valeur de lissage selon la vitesse de déplacement souhaitée

                positionX = Mathf.Lerp(positionX, autreUnite.PositionX, lissage);
                positionY = Mathf.Lerp(positionY, autreUnite.PositionY, lissage);
                EstEnMouvement = true;
            }
            else EstEnMouvement = false;
        }
        else EstEnMouvement = false;
    }


}
