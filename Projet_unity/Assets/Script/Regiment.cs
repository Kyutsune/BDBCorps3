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
    public int nb_unite_actuelle_dans_regiment;



    //Ici on va avoir le tableau qui sert à conserver les unites qui sont dans le régiment
    //La première unité qui est dans le tableau est l'unité "capitaine" 
    public List<Unite> tab_unite_en_regiment= new List<Unite>();
    private bool regiment_tous_rejoins;
    // private bool regiment_en_train_de_se_former;
    private List<bool> a_rejoint_le_regiment = new List<bool>();

    private float puissance_regiment;
    Regiment regiment_a_attaquer_param_class;


    public int Nb_Unite_Max_Dans_Regiment
    {
        get { return this.nb_unite_max_dans_regiment;}
        set { this.nb_unite_max_dans_regiment = value;}
    }
    
    public float Puissance_Regiment
    {
        get { return this.puissance_regiment;}
        set { this.puissance_regiment = value;}
    }

    public Unite renvoi_capitaine()
    {
        return tab_unite_en_regiment[0];
    }



    public void Creation_Regiment(Unite unite_capitaine)
    {
        this.nb_unite_max_dans_regiment=10;
        // this.regiment_en_train_de_se_former=true;
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
        puissance_regiment=Calcul_puissance_regiment(this);
        // this.regiment_en_train_de_se_former=false;   
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

    public bool cherche_si_unite_dans_regiment(Unite unite_cherchee)
    {
        return tab_unite_en_regiment.Contains(unite_cherchee);
    }


    ///Fonction qui va calculer le régiment qu'on va aller attaquer
    ///On choisi d'aller attaquer le regiment le plus faible,et si ils sont tous de même puissance,le plus proche
    public Regiment cherche_regiment_a_attaquer(List <Regiment> ensemble_autre_regiment)
    {
        int indice_min=0;
        float regiment_plus_fort_depart = ensemble_autre_regiment[0].Puissance_Regiment;
        float distance=Outil.distanceUnite(ensemble_autre_regiment[0].renvoi_capitaine(),this.renvoi_capitaine());


        for(int i=1;i<ensemble_autre_regiment.Count;i++)
        {
            float regiment_plus_fort = ensemble_autre_regiment[i].Puissance_Regiment;
            float distance_test=Outil.distanceUnite(ensemble_autre_regiment[i].renvoi_capitaine(),this.renvoi_capitaine());
            if(regiment_plus_fort < regiment_plus_fort_depart)
            {
                    regiment_plus_fort_depart = regiment_plus_fort;
                    distance=distance_test;
                    indice_min=i;
            }
            
            if(regiment_plus_fort == regiment_plus_fort_depart && distance>distance_test)
            {
                    distance=distance_test;
                    indice_min=i;
            }
        }
        // Debug.Log("On attaque le régiment "+indice_min+ " qui a une puissance de " +regiment_plus_fort_depart);
        return ensemble_autre_regiment[indice_min];
    }



    public bool Attaque_autre_regiment(List <Regiment> ensemble_autre_regiment)
    {
        if(this.regiment_tous_rejoins && ensemble_autre_regiment.Count>0)
        {
            
            Regiment regiment_a_attaquer=cherche_regiment_a_attaquer(ensemble_autre_regiment);


            if(regiment_a_attaquer.Puissance_Regiment>this.Puissance_Regiment)
            {
                //Il faudrait passer en paramètre de cette fonction la liste de régiment de nos potes
                //Appel_a_aide();
            }
            
            


            foreach (Unite unite in this.tab_unite_en_regiment)
            {
                unite.GestionEvenement(regiment_a_attaquer.tab_unite_en_regiment,regiment_a_attaquer.tab_unite_en_regiment.Count);
            }
            // Debug.Log("Nombre d'unité dans le régiment à attaquer = "+regiment_a_attaquer.tab_unite_en_regiment.Count);
            for(int i=0;i<tab_unite_en_regiment.Count;i++)
            {
                if(tab_unite_en_regiment[i].Pv<=0)
                {
                    this.puissance_regiment-=tab_unite_en_regiment[i].Degat;
                    this.tab_unite_en_regiment.RemoveAt(i);
                    // Debug.Log("Nouveau test,count du tab de celui qu'on enleve = "+ tab_unite_en_regiment.Count);
                }
            }
            if(this.tab_unite_en_regiment.Count <= 0)
            {
                return true;
            }
            else
                return false;
        }
        return false;
    } 


    public float Calcul_puissance_regiment(Regiment regiment)
    {
        for(int i=0; i < regiment.tab_unite_en_regiment.Count ; i++)
            puissance_regiment += regiment.tab_unite_en_regiment[i].Degat;
        return puissance_regiment;
    }

    public void Appel_a_aide(List <Regiment> regiment_notre_camp)
    {

    }
}

