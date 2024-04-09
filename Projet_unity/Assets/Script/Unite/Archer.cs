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
        
        Pv = 100;
        Portee = 10;
        VitesseAttaque= 5f;
        team = newteam;
        type_unitee = Type_unitee.Distance;
        Degat = 50f;
        en_regiment=false;
    }

    public override void Attaquer(Unite autreUnite)
    {
        if(autreUnite != null && this.Pv>0){
            // if(this.team == Team.EquipeBleue){
            //     Projectiles.envoyerFleche(this.team,this.PositionX,1.3f,this.PositionZ+0.5f,autreUnite);
            // }
            // else {
            //     Projectiles.envoyerFleche(this.team,this.PositionX,1.3f,this.PositionZ-0.5f,autreUnite);
            // }
            autreUnite.Pv=autreUnite.Pv - this.Degat;
        }
    }

    public override void GestionEvenement(List<Unite> tab,int nb_unite){
        this.Run= false;
        this.Walk=false;
        this.Attack=false;
        bool res=true;
        for(int i=0;i<nb_unite;i++)
        {
            if(tab[i].Mort==false)
            {
                res=false;
                break;
            }
        }
        if(res==true) {
            this.Win = true;
        }
        else{
            plus_proche = this.DetectionUnite(tab,nb_unite);
            if(plus_proche==null){
                return ;
            }

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
            }
        }
    }
}
