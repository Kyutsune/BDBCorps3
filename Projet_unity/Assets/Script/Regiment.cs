using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Ici on va coder le fait que nos unites se regroupent en régiment,donc en groupe et attaquent un autre groupe 
Pour se faire on va donc choisir une unité qui va être en quelque sorte être le capitaine du régiment
Puis n unites vont se regrouper autour
Puis le régiment va se déplacer vers un autre régiment 
*/


public class Regiment
{   
    private int nb_unite_max_dans_regiment;
    private int nb_unite_actuelle_dans_regiment;



    //Ici on va avoir le tableau qui sert à conserver les unites qui sont dans le régiment
    //La première unité qui est dans le tableau est l'unité "capitaine" 
    private List<Unite> tab_unite_en_regiment= new List<Unite>();
    private bool regiment_en_train_de_se_former;

    public int Nb_Unite_Max_Dans_Regiment
    {
        get { return this.nb_unite_max_dans_regiment;}
        set { this.nb_unite_max_dans_regiment = value;}
    }


    public void Creation_Regiment(Unite unite_capitaine)
    {
        this.nb_unite_max_dans_regiment=10;
        this.regiment_en_train_de_se_former=true;
        tab_unite_en_regiment.Add(unite_capitaine);
        this.nb_unite_actuelle_dans_regiment=1;
    }


    ///Fonction dans laquelle on va chercher a ranger les unite dans un regiment 
    //tab_uni correspond au tableau d'unité de la meme equipe que l'unite qui est la "capitaine"
    public void cherche_unite_dans_regiment(List<Unite> tab_uni,int nb_unite)
    {
        //AfficheList(tab_uni);
        while(this.nb_unite_actuelle_dans_regiment < this.nb_unite_max_dans_regiment)
        {   
            Unite recrue=tab_unite_en_regiment[0].DetectionUnite_regiment(tab_uni,nb_unite);
            tab_unite_en_regiment.Add(recrue);
            this.nb_unite_actuelle_dans_regiment++;
        }
        this.regiment_en_train_de_se_former=false;   
    }

    
    public void AfficheList()
    {
        Debug.Log(this.tab_unite_en_regiment.Count);
    }

    public void dernier_regiment_possible(int nouvelle_taille)
    {
        nb_unite_max_dans_regiment=nouvelle_taille;
    }

}
