using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdministrationRegiments 
{
    private Regiment test=new Regiment();
    private List<Regiment> tab_regiments_alliees = new List<Regiment>();
    private List<Regiment> tab_regiments_enemies = new List<Regiment>();

    private int nb_regiments_allie;
    private int nb_allie_sans_regiment;

    private int nb_regiments_ennemis;
    private int nb_ennemis_sans_regiment;
    
    private int Nb_Unite_Max_Dans_Regiment;
    private int taille_dernier_regiment_allie;
    private int taille_dernier_regiment_ennemis;




    public Unite Capitaine_regiment1_allie;
    public Unite capitaine_regiment2_allie;

    public Unite Capitaine_regiment1_ennemi;
    public Unite capitaine_regiment2_ennemi;
    


    ///Tu as bien créer une list de regiment allié,faut en faire une pour les régiments ennemis
///Pour vétifier si ils sont bien crées,cf l'exemple que j'ai mis pour init 1 régiment 

    public void InitialisationNombreRegiments(int nb_alliee_total, int nb_ennemis_total)
    {
        Nb_Unite_Max_Dans_Regiment = 10;
        nb_regiments_allie = nb_alliee_total / Nb_Unite_Max_Dans_Regiment; 

        if(nb_alliee_total % Nb_Unite_Max_Dans_Regiment!=0)
        {
            taille_dernier_regiment_allie=nb_alliee_total % Nb_Unite_Max_Dans_Regiment;
            nb_regiments_allie++;
        }
        else
            taille_dernier_regiment_allie=Nb_Unite_Max_Dans_Regiment;

        nb_regiments_ennemis = nb_ennemis_total / Nb_Unite_Max_Dans_Regiment; 

        if(nb_ennemis_total % Nb_Unite_Max_Dans_Regiment!=0)
        {
            taille_dernier_regiment_ennemis=nb_ennemis_total % Nb_Unite_Max_Dans_Regiment;
            nb_regiments_ennemis++;
        }
        else
            taille_dernier_regiment_ennemis=Nb_Unite_Max_Dans_Regiment;
        
    } 

    public void GestionRegiments(int nb_alliee_total, int nb_ennemis_total, List<Unite> tab_uni_alliee, List<Unite> tab_uni_ennemis)
    {
        for(int j = 0; j < nb_regiments_allie; j++)
        {
            int variable_alliee=0;
            while(tab_uni_alliee[variable_alliee].EnRegiment == true)
            {
                variable_alliee++;
            }

            if(j != (nb_regiments_allie-1))
            {              
                Regiment regiment_generique = new Regiment();
                regiment_generique.Creation_Regiment(tab_uni_alliee[variable_alliee]);
                Capitaine_regiment1_allie=tab_uni_alliee[variable_alliee];
                tab_regiments_alliees.Add(regiment_generique);  

                
            
                tab_regiments_alliees[j].cherche_unite_dans_regiment(tab_uni_alliee , nb_alliee_total); 
                // Debug.Log(tab_regiments_alliees[0].VerifierUnitesIndependantes());
            } 
            else
            {
                Regiment regiment_generique = new Regiment();
                regiment_generique.Creation_Regiment(tab_uni_alliee[variable_alliee]);
                capitaine_regiment2_allie=tab_uni_alliee[variable_alliee];
                tab_regiments_alliees.Add(regiment_generique);
                tab_regiments_alliees[j].dernier_regiment_possible(taille_dernier_regiment_allie);           
                tab_regiments_alliees[j].cherche_unite_dans_regiment(tab_uni_alliee , nb_alliee_total);

                // Debug.Log(tab_regiments_alliees[1].VerifierUnitesIndependantes());
            }
            
          
        }

        for(int j = 0; j < nb_regiments_ennemis; j++)
        {
            int variable_ennemis=0;
            while(tab_uni_ennemis[variable_ennemis].EnRegiment == true)
            {
                variable_ennemis++;
            }
            if(j != (nb_regiments_ennemis - 1))
            {              
                Regiment regiment_generique = new Regiment();
                regiment_generique.Creation_Regiment(tab_uni_ennemis[variable_ennemis]);
                Capitaine_regiment1_ennemi = tab_uni_ennemis[variable_ennemis];
                tab_regiments_enemies.Add(regiment_generique);
            
                tab_regiments_enemies[j].cherche_unite_dans_regiment(tab_uni_ennemis , nb_ennemis_total);
            } 
            else
            {
                Regiment regiment_generique = new Regiment();
                regiment_generique.Creation_Regiment(tab_uni_ennemis[variable_ennemis]);
                capitaine_regiment2_ennemi = tab_uni_ennemis[variable_ennemis];
                tab_regiments_enemies.Add(regiment_generique);

                tab_regiments_enemies[j].dernier_regiment_possible(taille_dernier_regiment_ennemis);                
                tab_regiments_enemies[j].cherche_unite_dans_regiment(tab_uni_ennemis , nb_ennemis_total);    
                           
            } 
            // Debug.Log("on affiche regiment"+tab_regiments_enemies[j]);   
            // tab_regiments_enemies[j].AfficheList();               
        } 


    }

    public void RegroupementRegiment(int nb_alliee_total, int nb_ennemis_total , List<GameObject> tab_gameobject_unite, List<Unite> unites_alliees, List<Unite> unites_ennemies)
    {
         // tab_gameobject_unite
        for(int i = 0; i < nb_regiments_allie; i++)
        {
            tab_regiments_alliees[i].Regiment_se_rejoint();
        }

        for(int j=0;j<nb_alliee_total;j++)
        {
            tab_gameobject_unite[j].transform.position=new Vector3(unites_alliees[j].PositionX, unites_alliees[j].PositionY, unites_alliees[j].PositionZ);

            if(unites_alliees[j].EnRegiment==false)
            {
                Debug.Log("Probleme avec l'unite d'indice donné, qui n'a donc pas de régiments "+ j);
            }
        }

        // tab_gameobject_unite
        for(int i = 0; i < nb_regiments_ennemis; i++)
        {
            tab_regiments_enemies[i].Regiment_se_rejoint();
        }

        for(int j=0;j<nb_ennemis_total;j++)
        {
            tab_gameobject_unite[nb_alliee_total+j].transform.position=new Vector3(unites_ennemies[j].PositionX, unites_ennemies[j].PositionY, unites_ennemies[j].PositionZ);

            if(unites_ennemies[j].EnRegiment==false)
            {
                Debug.Log("Probleme avec l'unite d'indice donné, qui n'a donc pas de régiments "+ j);
            }
        }
    }
}
