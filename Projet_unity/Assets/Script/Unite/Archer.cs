using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Unite
{
    public Archer() : base(){}

    public Archer(Team newteam)
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
        
        Pv = 500;
        Portee = 10;
        VitesseAttaque= 5f;
        team = newteam;
        type_unitee = Type_unitee.Distance;
        Degat = 50f;
        en_regiment=false;
    }

    public override void Attaquer(Unite autreUnite)
    {
        if(autreUnite != null){
            if(this.team == Team.EquipeBleue){
                Projectiles.envoyerFleche(this.team,this.PositionX,1.3f,this.PositionZ+0.5f,autreUnite);
            }
            else {
                Projectiles.envoyerFleche(this.team,this.PositionX,1.3f,this.PositionZ-0.5f,autreUnite);
            }
            autreUnite.Pv=autreUnite.Pv - this.Degat;
        }
    }

    public override bool GestionEvenement(List<Unite> tab,int nb_unite,animatorController animEvenement){
        if(nb_unite != 0) {
            Unite plus_proche = this.DetectionUnite(tab,nb_unite);

            // Définir une distance minimale pour éviter les collisions
            float distanceMinimale = 1.5f;

            // Calculer la distance entre les deux unités
            float distance = Outil.distanceUnite(this,plus_proche);

            // Vérifier si la distance est supérieure à la distance minimale
            if (distance > distanceMinimale && distance > this.Portee)
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

            if(distance <= this.Portee)
            {
                animEvenement.seTourner(this,plus_proche);
                animEvenement.setFighting(true);
                if(Time.time - DernierTempsAttaque > this.VitesseAttaque)
                {
                    this.Attaquer(plus_proche);
                    DernierTempsAttaque = Time.time;
                }
            }

            if(this.Pv <= 0){
                animEvenement.Mort(true);
                return true;
            }
        }
        else {
            animEvenement.Victoire(true);
        }

        return false;
    }
}
