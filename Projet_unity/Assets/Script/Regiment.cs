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

    private int nb_regiments_allie;
    private int nb_allie_sans_regiment;

    private int nb_regiments_ennemis;
    private int nb_ennemis_sans_regiment;
    

    //Ici on va avoir le tableau qui sert à conserver les unites qui sont dans le régiment
    //La première unité qui est dans le tableau est l'unité "capitaine" 
    private List<Unite> tab_unite_en_regiment= new List<Unite>();
    private bool regiment_en_train_de_se_former;

    public int Nb_Unite_Max_Dans_Regiment
    {
        get { return this.nb_unite_max_dans_regiment;}
        set { this.nb_unite_max_dans_regiment = value;}
    }


    private void Creation_Regiment(Unite unite_capitaine)
    {
        this.nb_unite_max_dans_regiment=10;
        this.regiment_en_train_de_se_former=true;
        tab_unite_en_regiment.Add(unite_capitaine);
        this.nb_unite_actuelle_dans_regiment=1;
    }


    ///Fonction dans laquelle on va chercher a ranger les unite dans un regiment 
    //tab_uni correspond au tableau d'unité de la meme equipe que l'unite qui est la "capitaine"
    private void cherche_unite_dans_regiment(List<Unite> tab_uni,int nb_unite)
    {
        AfficheList(tab_uni);
        while(this.nb_unite_actuelle_dans_regiment<=this.nb_unite_max_dans_regiment)
        {   
            Unite recrue=tab_unite_en_regiment[0].DetectionUnite_regiment(tab_uni,nb_unite);
            tab_unite_en_regiment.Add(recrue);
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
    
    public void AfficheList(List<Unite> tab_uni)
    {
        Debug.Log(tab_uni.Count);
    }

    public void InitialisationNombreRegiments(int nb_alliee_total, int nb_ennemis_total)
    {
        Nb_Unite_Max_Dans_Regiment = 10;

        nb_regiments_allie = nb_alliee_total / Nb_Unite_Max_Dans_Regiment; 
        
        //Si le nombre d'alliés n'est pas divisible par le nombre de régiments, alors il manque un dernier régiment (non complet)
        if(nb_alliee_total != nb_regiments_allie * Nb_Unite_Max_Dans_Regiment) 
        {
            nb_regiments_allie++;
        } 

        nb_allie_sans_regiment = nb_alliee_total - 1;

        nb_regiments_ennemis = nb_ennemis_total / Nb_Unite_Max_Dans_Regiment; 

        //Si le nombre d'ennemis n'est pas divisible par le nombre de régiments, alors il manque un dernier régiment (non complet)
        if(nb_ennemis_total != nb_regiments_ennemis * Nb_Unite_Max_Dans_Regiment) 
        {
            nb_regiments_ennemis++;
        } 

        nb_ennemis_sans_regiment = nb_ennemis_total - 1;
    }

    public void GestionRegiments(int nb_alliee_total, int nb_ennemis_total, List<Unite> tab_uni_alliee, List<Unite> tab_uni_ennemis)
    {
        for(int j = 0; j < nb_regiments_allie; j++)
        {
            Formation_regiment(tab_uni_alliee[nb_allie_sans_regiment], tab_uni_alliee , nb_alliee_total);  
            //AfficheList(tab_uni_ennemis);  
            nb_allie_sans_regiment = nb_allie_sans_regiment - Nb_Unite_Max_Dans_Regiment;                
        }

        for(int j = 0; j < nb_regiments_ennemis; j++)
        {
            Formation_regiment(tab_uni_ennemis[nb_ennemis_sans_regiment], tab_uni_ennemis , nb_ennemis_total); 
            nb_ennemis_sans_regiment = nb_ennemis_sans_regiment - Nb_Unite_Max_Dans_Regiment;                   
        } 
    }
}
