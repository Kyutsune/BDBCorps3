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
    private bool regiment_tous_rejoins;
    private List<bool> a_rejoint_le_regiment = new List<bool>();

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
        a_rejoint_le_regiment.Add(true);
        this.nb_unite_actuelle_dans_regiment=1;
        this.regiment_est_rejoint=false;
    }


    ///Fonction dans laquelle on va chercher a ranger les unite dans un regiment 
    //tab_uni correspond au tableau d'unité de la meme equipe que l'unite qui est la "capitaine"
    public void cherche_unite_dans_regiment(List<Unite> tab_uni,int nb_unite)
    {
        //AfficheList(tab_uni);
        while(this.nb_unite_actuelle_dans_regiment<=this.nb_unite_max_dans_regiment)
        {   
            Unite recrue=tab_unite_en_regiment[0].DetectionUnite_regiment(tab_uni,nb_unite);
            tab_unite_en_regiment.Add(recrue);
            a_rejoint_le_regiment.Add(false);
            this.nb_unite_actuelle_dans_regiment++;
        }
        this.regiment_en_train_de_se_former=false;   

    }


    //Ici on a la fonction qui va nous servir à initialiser le régiment et le remplir d'unité 
    public void Formation_regiment(Unite unite_capitaine,List<Unite> tab_uni,int nb_unite)
    {
        Creation_Regiment(unite_capitaine);
        cherche_unite_dans_regiment(tab_uni,nb_unite);
    }
    
    public void AfficheList()
    {
        Debug.Log(this.tab_unite_en_regiment.Count);
    }


    public bool verify_if_all_rejoins()
    {
        for(int i=0;i<a_rejoint_le_regiment.Count;i++)
        {   
            if(a_rejoint_le_regiment[i]==false)
                return false
        }
        regiment_tous_rejoins=true;
        return true;
    }

    public void Regiment_se_rejoint()
    {
        if(this.regiment_tous_rejoins==false)
        {
            //On commence à 1 car l'unité 0 du tableau est celle vers laquelle on veut se déplacer
            for(int i=1;i<this.nb_unite_actuelle_dans_regiment;i++)
            {
                tab_unite_en_regiment[i].Deplacement(tab_unite_en_regiment[0]);
                if(tab_unite_en_regiment[i].distanceUnite(tab_unite_en_regiment[0])<=tab_unite_en_regiment[i].Portee)
                {
                    a_rejoint_le_regiment[i]=true;                        
                }
            }
            verify_if_all_rejoins();
        } 
    }
}
