using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladin :  Unite
{
    public Paladin() : base() {}

    public Paladin(Team newteam)
    {
        if(newteam == Team.EquipeBleue){
            PositionX = Outil.Aleatoire(20,50);
            PositionY = 0;
            PositionZ = Outil.Aleatoire(-10,0);
        }
        else{
            PositionX = Outil.Aleatoire(20,50);
            PositionY = 0;
            PositionZ = Outil.Aleatoire(30,40);
        }
        
        Pv = 200;
        Portee = 2;
        VitesseAttaque= 2f;
        team = newteam;
        type_unitee = Type_unitee.Melee; 
        Degat = 20f;
        en_regiment=false;
    }

    public override void Attaquer(Unite autreUnite)
    {
        if(autreUnite != null && this.Pv>0){
            autreUnite.Pv=autreUnite.Pv - this.Degat;
        }
    }

    public override bool GestionEvenement (List<Unite> tab,int nb_unite){
        if(nb_unite != 0) {
            plus_proche = this.DetectionUnite(tab,nb_unite);

            // Définir une distance minimale pour éviter les collisions
            float distanceMinimale = 1.5f;

            // Calculer la distance entre les deux unités
            float distance = Outil.distanceUnite(this,plus_proche);

            // Vérifier si la distance est supérieure à la distance minimale
            if (distance > distanceMinimale && distance > this.Portee)
            {
                int RunOrWalk = this.Deplacement(plus_proche);
                if(RunOrWalk == 1){
                    this.Run = true;
                }
                if(RunOrWalk == 2) {
                    this.Walk = true;
                }
            }

            if(distance <= this.Portee)
            {
                this.Attack = true;
                if(Time.time - DernierTempsAttaque > this.VitesseAttaque)
                {
                    this.Attaquer(plus_proche);
                    DernierTempsAttaque = Time.time;
                }
            }
            if(this.Pv <= 0){
                this.Mort = true;
                return true;
            }
        }
        else {
            this.Win = true;
        }

        return false;
    }
}
