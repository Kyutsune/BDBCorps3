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
    EquipeBleue,
    EquipeRouge
}

public enum Type_unitee
{
    Melee,
    Distance
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
    private bool parmiNous;
    private float degat;
    private float dernierTempsAttaque = 0f;

    //Type des unités
    public Team team;
    public Type_unitee type_unitee;

    // vitesse de déplacement de l'unité
    public float RunSpeed = 5f;
    public float WalkSpeed = 2f;


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
        team = 0;
        type_unitee = Type_unitee.Melee;
        degat = 1;
    }

    public Unite(double newPv, float newPortee, double newVitesseAttaque,Team newteam, Type_unitee newTypeUnitee, Canvas NewCanva,bool newparmiNous,float newDegat )
    {
        if(newteam == Team.EquipeBleue){
            positionX = Aleatoire(20,50);
            positionY = 0;
            positionZ = Aleatoire(-10,0);
        }
        else{
            positionX = Aleatoire(20,50);
            positionY = 0;
            positionZ = Aleatoire(30,40);
        }
        
        pv = newPv;
        portee = newPortee;
        vitesseAttaque=newVitesseAttaque;
        team = newteam;
        type_unitee = newTypeUnitee;
        parmiNous = newparmiNous; 
        degat = newDegat;
    }

    public float distanceUnite(Unite autreUnite){
        return Vector3.Distance(new Vector3(this.positionX, this.positionY, this.positionZ), new Vector3(autreUnite.PositionX, autreUnite.PositionY, autreUnite.PositionZ));
    }

    public Unite DetectionUnite (List<Unite> tab_uni,int nb_unite) {
        if(nb_unite != 0){
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
        return null;
	}

    public void Attaquer(Unite autreUnite)
    {
        if(autreUnite != null){
            autreUnite.pv=autreUnite.pv - this.degat;
        }
    }

    public bool GestionEvenement(List<Unite> tab,int nb_unite,animatorController animEvenement){
        if(nb_unite != 0) {
            Unite plus_proche = this.DetectionUnite(tab,nb_unite);

            // Définir une distance minimale pour éviter les collisions
            float distanceMinimale = 1.5f;

            // Calculer la distance entre les deux unités
            float distance = this.distanceUnite(plus_proche);

            // Vérifier si la distance est supérieure à la distance minimale
            if (distance > distanceMinimale && distance > this.portee)
            {
                animEvenement.seTourner(this,plus_proche);
                int RunOrWalk = this.Deplacement(plus_proche);
                if(RunOrWalk == 1){
                    animEvenement.setRunning(true);
                }
                if(RunOrWalk == 2) {
                    animEvenement.setWalking(true);
                }
            }

            if(distance <= this.portee)
            {
                animEvenement.seTourner(this,plus_proche);
                animEvenement.setFighting(true);
                if(Time.time - dernierTempsAttaque > this.vitesseAttaque)
                {
                    this.Attaquer(plus_proche);
                    dernierTempsAttaque = Time.time;
                }
            }

            if(this.pv <= 0){
                animEvenement.Mort();
                return true;
            }
        }
        else {
            animEvenement.Victoire();
        }

        return false;
    }

    int Aleatoire(int min, int max)
    {
        // Génération d'un nombre aléatoire entre min (inclus) et max (exclus)
        return UnityEngine.Random.Range(min, max);
    }

    public int Deplacement(Unite targetUnit){
        if(targetUnit != null){
            if (targetUnit.team != this.team)
            {
                // Récupérer la position de la cible
                Vector3 targetPosition = new Vector3(targetUnit.PositionX, targetUnit.PositionY, targetUnit.PositionZ);

                // Calculer le vecteur direction de la cible
                Vector3 direction = (targetPosition - new Vector3(this.PositionX, this.PositionY, this.PositionZ)).normalized;

                float distance = this.distanceUnite(targetUnit);
                // Calculer le déplacement en fonction de la vitesse constante
                if(distance > 5f){
                    Vector3 movement = direction * RunSpeed * Time.deltaTime;

                    // Mettre à jour la position de l'unité
                    this.PositionX += movement.x;
                    this.PositionY += movement.y;
                    this.PositionZ += movement.z;

                    return 1;
                }
                
                if(distance <= 5f){
                    Vector3 movement = direction * WalkSpeed * Time.deltaTime;

                    // Mettre à jour la position de l'unité
                    this.PositionX += movement.x;
                    this.PositionY += movement.y;
                    this.PositionZ += movement.z;

                    return 2;
                }
            }
        }
        return 0;
    }
}