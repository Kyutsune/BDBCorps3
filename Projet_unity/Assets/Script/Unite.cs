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
    private float positionZ = 0;


    private double pv;

    //Composantes nécessaire à l'attaque
    private float portee;
    private double vitesseAttaque;
    private float momentDerniereAttaque;
    private bool parmiNous;

    //Type des unités
    public Team team;
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

     public float PositionZ
    {
        get { return positionZ; }
        set { positionZ = value; }
    }

    public double Pv
    {
        get { return pv; }
        set { pv = value; }
    }

    public bool ParmiNous
    {
        get { return parmiNous; }
        set { parmiNous = value; }
    }


    //Constructeur par défaut,à utiliser pour les tests de début 
    public Unite()
    {
        positionX = 0;
        positionY = 0;
        positionZ = 0;
        pv = 100;
        portee = 100;
        vitesseAttaque = 1;
        momentDerniereAttaque = 0;
        team = 0;
        type_unitee = Type_unitee.Melee;

    }

    public Unite(float newX, float newY, float newZ, double newPv, float newPortee, double newVitesseAttaque,Team newteam, Type_unitee newTypeUnitee, Canvas NewCanva,bool newparmiNous )
    {
        positionX = newX;
        positionY = newY;
        positionZ = newZ;
        pv = newPv;
        portee = newPortee;
        vitesseAttaque=newVitesseAttaque;
        team = newteam;
        type_unitee = newTypeUnitee;
        parmiNous = newparmiNous; 

    }

    public float distanceUnite(Unite autreUnite){
        return Vector3.Distance(new Vector3(this.positionX, this.positionY, this.positionZ), new Vector3(autreUnite.PositionX, autreUnite.PositionY, autreUnite.PositionZ));
    }

    public Unite DetectionUnite (List<Unite> tab_uni,int nb_unite) {
        int indice_min = 0;
		float distance_min = 0;
		for(int j = 0; j < nb_unite; j++){
			float distance = this.distanceUnite(tab_uni[j]);
			if(distance_min > distance || j == 0){
                indice_min = j;
                distance_min = distance;
            }
		}
		return tab_uni[indice_min];
	}

    public void Attaquer(Unite autreUnite)
    {
        long tempsActuel = DateTimeOffset.Now.ToUnixTimeSeconds();
        double tempsDepuisDerniereAttaque = (tempsActuel - momentDerniereAttaque);
        if(tempsDepuisDerniereAttaque >= vitesseAttaque)
        {
            autreUnite.Pv=autreUnite.Pv - 1;
            tempsDepuisDerniereAttaque = tempsActuel;
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
            float distance = this.distanceUnite(autreUnite);

            // Vérifier si la distance est supérieure à la distance minimale
            if (distance > distanceMinimale)
            {
                
                // Déplacer vers l'autre unité en utilisant une approche de lissage linéaire
                float lissage = 0.0007f;  // Ajustez la valeur de lissage selon la vitesse de déplacement souhaitée

                positionX = Mathf.Lerp(positionX, autreUnite.PositionX, lissage);
                positionY = Mathf.Lerp(positionY, autreUnite.PositionY, lissage);
                positionZ = Mathf.Lerp(positionZ, autreUnite.PositionY, lissage);
            }
        }
    }

    public void GestionEvenement(List<Unite> tab,int nb_unite){
        Unite plus_proche = this.DetectionUnite(tab,nb_unite);
        this.DeplacerVersUniteDifferente(plus_proche);

        double distance = Math.Sqrt(Math.Pow(plus_proche.PositionX-this.positionX, 2) + Math.Pow(plus_proche.PositionY-this.positionY, 2) + Math.Pow(plus_proche.PositionZ-this.positionZ, 2));
            if(distance <= this.portee)
            {
                this.Attaquer(plus_proche);
            }
    }
}