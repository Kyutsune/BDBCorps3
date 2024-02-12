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

//Enum qui va nous servir à savoir dans quelle équipe est l'unité
public enum Team
{
    Equipe1,
    Equipe2
}

public enum Type_unitee
{
    Melee
}

//Classe unité qui va définir les personnages bougeant sur le terrain
public class Unite 
{
    //Position
    private float positionX = 0;
    private float positionY = 0;


    private double pv;

    //Composantes nécessaire à l'attaque
    private float portee;
    private double vitesseAttaque;
    private float momentDerniereAttaque;

    //Type des unités
    public Team team;
    public Type_unitee type_unitee;

    //Création pvs
    Canvas canva;


    AffichageDesPVs pvs_graphiques;

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
        team = 0;
        type_unitee = Type_unitee.Melee;
    }

    public Unite(float newX, float newY, double newPv, float newPortee, double newVitesseAttaque,Team newteam, Type_unitee newTypeUnitee, Canvas NewCanva)
    {
        positionX = newX;
        positionY = newY;
        pv = newPv;
        portee = newPortee;
        vitesseAttaque=newVitesseAttaque;
        team = newteam;
        type_unitee = newTypeUnitee;
        
        pvs_graphiques = new AffichageDesPVs();
        pvs_graphiques.CreerTextePV(NewCanva,positionX , positionY, 0);
    }

    public void Attaquer(Unite autreUnite)
    {
        long tempsActuel = DateTimeOffset.Now.ToUnixTimeSeconds();
        double tempsDepuisDerniereAttaque = (tempsActuel - momentDerniereAttaque);
        if(tempsDepuisDerniereAttaque >= vitesseAttaque)
        {
            double distance = Math.Sqrt(Math.Pow(autreUnite.PositionX-positionX, 2) + Math.Pow(autreUnite.PositionY-positionY, 2));
            if(distance <= this.portee)
            {
                autreUnite.Pv=autreUnite.Pv - 1;
                pvs_graphiques.MettreAJourTextePV(autreUnite.Pv);
                tempsDepuisDerniereAttaque = tempsActuel;

            }
        }
    }

    public void DeplacerVersUniteDifferente(Unite autreUnite)
    {
        // Vérifier si l'autre unité a une Unite_alliee_ennemie différente
        if (autreUnite.team != this.team)
        {
            // Définir une distance minimale pour éviter les collisions
            float distanceMinimale = 1.5f;  // Ajustez cette valeur selon votre préférence

            // Calculer la distance entre les deux unités
            float distance = Vector2.Distance(new Vector2(positionX, positionY), new Vector2(autreUnite.PositionX, autreUnite.PositionY));

            // Vérifier si la distance est supérieure à la distance minimale
            if (distance > distanceMinimale)
            {
                // Déplacer vers l'autre unité en utilisant une approche de lissage linéaire
                float lissage = 0.0007f;  // Ajustez la valeur de lissage selon la vitesse de déplacement souhaitée

                positionX = Mathf.Lerp(positionX, autreUnite.PositionX, lissage);
                positionY = Mathf.Lerp(positionY, autreUnite.PositionY, lissage);
            }
        }
    }

    public void DeplacerVersUniteDifferente2(Unite autreUnite)
    {
        // Vérifier si l'autre unité a une Unite_alliee_ennemie différente
        if (autreUnite.team != this.team)
        {
           // Calculer la direction vers l'autre unité
            Vector2 direction = new Vector2(autreUnite.PositionX - positionX, autreUnite.PositionY - positionY).normalized;

            // Définir une vitesse de déplacement
            float vitesseDeplacement = 0.005f;  // Ajustez cette valeur selon la vitesse de déplacement souhaitée

            float distanceMinimale = 1.5f;  // Ajustez cette valeur selon votre préférence
            positionX += direction.x * vitesseDeplacement;
            positionY += direction.y * vitesseDeplacement;

            // Déplacer l'unité vers l'autre unité
            if(distanceMinimale <= positionX && distanceMinimale <= positionY)
            {
                positionX += direction.x * vitesseDeplacement;
                positionY += direction.y * vitesseDeplacement;
            }
        }
    }


}
