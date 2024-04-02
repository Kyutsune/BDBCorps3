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
    public List<Unite> tab_unite_en_regiment= new List<Unite>();
    private bool regiment_tous_rejoins;
    private bool regiment_en_train_de_se_former;
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
        unite_capitaine.EnRegiment=true;
        a_rejoint_le_regiment.Add(true);
        this.nb_unite_actuelle_dans_regiment=1;
        this.regiment_tous_rejoins=false;
    }


    ///Fonction dans laquelle on va chercher a ranger les unite dans un regiment 
    //tab_uni correspond au tableau d'unité de la meme equipe que l'unite qui est la "capitaine"
    public void cherche_unite_dans_regiment(List<Unite> tab_uni,int nb_unite)
    {
        //AfficheList(tab_uni);
        while(this.nb_unite_actuelle_dans_regiment < this.nb_unite_max_dans_regiment)
        {   
            Unite recrue=tab_unite_en_regiment[0].DetectionUnite_regiment(tab_uni,nb_unite,tab_unite_en_regiment);
            tab_unite_en_regiment.Add(recrue);
            a_rejoint_le_regiment.Add(false);
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


    public bool verify_if_all_rejoins()
    {
        for(int i=0;i < a_rejoint_le_regiment.Count;i++)
        {   
            if(a_rejoint_le_regiment[i]==false)
                return false;
        }
        regiment_tous_rejoins=true;
        return true;
    }

    public void Regiment_se_rejoint()
    {
        if(this.regiment_tous_rejoins == false)
        {
            //On commence à 1 car l'unité 0 du tableau est celle vers laquelle on veut se déplacer
            for(int i=1;i < this.nb_unite_actuelle_dans_regiment;i++)
            {
                if(Outil.distanceUnite(tab_unite_en_regiment[i],tab_unite_en_regiment[0]) > 2)
                {
                    int RunOrWalk = tab_unite_en_regiment[i].Deplacement(tab_unite_en_regiment[0]);
                    if(RunOrWalk == 1){
                        tab_unite_en_regiment[i].Run = true;
                    } if(RunOrWalk == 2){
                        tab_unite_en_regiment[i].Walk = true;
                    }
                }
                
                if(Outil.distanceUnite(tab_unite_en_regiment[i],tab_unite_en_regiment[0])<=2)
                {
                    tab_unite_en_regiment[i].Run = false;
                    tab_unite_en_regiment[i].Walk = false;
                    a_rejoint_le_regiment[i]=true;                        
                }
            }
        } 
        bool test;
        test=verify_if_all_rejoins();
    }


    public bool verification_toute_unite_en_regiment()
    {
        for(int i=0;i<nb_unite_actuelle_dans_regiment;i++){
            if(tab_unite_en_regiment[i].EnRegiment==false)
            {
                Debug.Log("toutes les unités n'ont pas leur booléen en_regiment à true,autrement dit au moins une l'a à false");
                return false;
            }
        }
        return true;
    }

    public bool VerifierUnitesIndependantes()
    {
        // Créer un HashSet pour stocker les références des unités déjà rencontrées
        HashSet<Unite> unitesRencontrees = new HashSet<Unite>();

        foreach (Unite unite in tab_unite_en_regiment)
        {
            // Vérifier si l'unité a déjà été rencontrée
            if (unitesRencontrees.Contains(unite))
            {
                // Si l'unité est déjà présente dans le HashSet, cela signifie qu'elle n'est pas unique
                return false;
            }
            else
            {
                unitesRencontrees.Add(unite);
            }
        }
        return true;
    }


    public bool verif_unite_autre_regiment(Regiment autreRegiment)
    {
        foreach (Unite unite in autreRegiment.tab_unite_en_regiment)
        {
            if (tab_unite_en_regiment.Contains(unite))
            {
                return true;
            }
        }
        return false;
    }

    public bool Tentative_ranger_retardataire(Unite unite_retardataire)
    {
        if(nb_unite_max_dans_regiment<nb_unite_actuelle_dans_regiment)
        {
            tab_unite_en_regiment.Add(unite_retardataire);
            unite_retardataire.EnRegiment=true;
            nb_unite_actuelle_dans_regiment++;
            return true;
        }
        return false;
    }



    public bool cherche_si_unite_dans_regiment(Unite unite_cherchee)
    {
        return tab_unite_en_regiment.Contains(unite_cherchee);
    }
}
