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
public abstract class Unite
{
    //Position
    private float positionX = 0;
    private float positionY = 0;
    private float positionZ = 0;


    private double pv;

    //Composantes nécessaire à l'attaque
    private float portee;
    private double vitesseAttaque;
    private float degat;
    private float dernierTempsAttaque = 0f;

    //Type des unités
    public Team team;
    public Type_unitee type_unitee;

    // vitesse de déplacement de l'unité
    public float RunSpeed = 5f;
    public float WalkSpeed = 2f;



    //Ici on va gérer les composantes servant pour les régiments d'untité
    //booléen qui nous dis si l'unité est en régiment ou non  
    public bool en_regiment;
    public bool Run = false;
    public bool Walk = false;
    public bool Attack = false;
    public bool Mort = false;
    public bool Win = false;

    public Unite plus_proche;

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
    
    public bool EnRegiment
    {
        get { return en_regiment; }
        set { en_regiment = value; }
    }

    public float Portee
    {
        get { return portee; }
        set { portee = value; }
    }

    public double VitesseAttaque
    {
        get { return vitesseAttaque; }
        set { vitesseAttaque = value; }
    }

    public float DernierTempsAttaque 
    {
        get { return dernierTempsAttaque; }
        set { dernierTempsAttaque = value; }
    }

    public float Degat
    {
        get { return degat; }
        set { degat = value; }
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

    //Fonction qui va chercher l'unité la plus proche de l'unité courant dans le tableau tab_uni
    //nb_unite correspond au nombre unite dans le tableau tab_uni
    public Unite DetectionUnite (List<Unite> tab_uni,int nb_unite) {
        if(nb_unite != 0){
            int indice_min = 0;
            float distance_min = 0;
            for(int j = 0; j < nb_unite; j++){
                
                if(tab_uni[j].Pv <= 0)
                    continue;
                float distance = Outil.distanceUnite(this,tab_uni[j]);
                if((distance_min > distance || j == 0)){
                    indice_min = j;
                    distance_min = distance;
                }
            }
            return tab_uni[indice_min];
        }
        return null;
	}

    public Unite DetectionUnite_regiment(List<Unite> tab_uni, int nb_unite, List<Unite> tab_regiment_deja_forme)
    {
        if (nb_unite != 0)
        {
            int indice_min = 0;
            float distance_min = Outil.distanceUnite(tab_uni[indice_min], this);
            while(tab_regiment_deja_forme.Contains(tab_uni[indice_min]) || tab_uni[indice_min].EnRegiment==true)
            {
               indice_min++;
               distance_min = Outil.distanceUnite(tab_uni[indice_min], this);
            }
            for (int j = indice_min; j < nb_unite; j++)
            {
                if(tab_regiment_deja_forme.Contains(tab_uni[j]))
                {
                    continue;
                }
                float distance = Outil.distanceUnite(tab_uni[j], this);
                if ((distance_min > distance) && (tab_uni[j].EnRegiment == false))
                {
                    indice_min = j;
                    distance_min = distance;
                }
            }
            tab_uni[indice_min].EnRegiment = true;
            return tab_uni[indice_min];
        }
        else
            return null;
    }



    public abstract void Attaquer(Unite autreUnite);

    public abstract bool GestionEvenement(List<Unite> tab,int nb_unite);

    public int Deplacement(Unite targetUnit){
        if(targetUnit != null && this.Pv>0){
            // Récupérer la position de la cible
            Vector3 targetPosition = new Vector3(targetUnit.PositionX, targetUnit.PositionY, targetUnit.PositionZ);

            // Calculer le vecteur direction de la cible
            Vector3 direction = (targetPosition - new Vector3(this.PositionX, this.PositionY, this.PositionZ)).normalized;

                float distance = Outil.distanceUnite(this,targetUnit);
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
        return 0;
    }
}

