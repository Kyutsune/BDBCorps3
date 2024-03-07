using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Classe jeu qui va être celle qui va contenir notre boucle de jeu,essayons de conserver un maximum un systeme de MVC s'il vous plait

*/
public class Jeu : MonoBehaviour
{
    private List<GameObject> tab_gameobject_unite = new List<GameObject>();
    private List<Unite> unites_alliees = new List<Unite>();
    private List<Unite> unites_ennemies = new List<Unite>();
    private int nb_alliee;
    private int nb_ennemis;




    public GameObject prefab;
    Animator animator;



    private Canvas canva_pour_texte_pv;
    private List<AffichageDesPVs> liste_texte_pv= new List<AffichageDesPVs>();



    Timer temps_passé_en_jeu=new Timer();
    bool affichage_temps=false;
    


    void Start()
    {
        GameObject canvasObj = new GameObject("Canvas");
        canva_pour_texte_pv = canvasObj.AddComponent<Canvas>();
        canva_pour_texte_pv.renderMode = RenderMode.ScreenSpaceOverlay;
        
        nb_alliee=1;
        nb_ennemis=1;

    
        animator = GetComponent<Animator>();


        for(int i=0;i<nb_alliee;i++) // Parcours du nombre d'unites_ennemies + alliées
        {
            // Créer le texte PV et l'ajouter à la liste
            // AffichageDesPVs textePVUnite=GetComponent<AffichageDesPVs>();
            // textePVUnite.CreerTextePV(canva_pour_texte_pv, unites_alliees[i].PositionX, unites_alliees[i].PositionY+40, unites_alliees[i].PositionZ);
            // liste_texte_pv.Add(textePVUnite);

            unites_alliees.Add(new Unite(0, 0, 0, 1000, 2, 1, Team.Equipe1, Type_unitee.Melee, canva_pour_texte_pv, true));
            CreerUnite(unites_alliees[i].PositionX, unites_alliees[i].PositionY, unites_alliees[i].PositionZ);
        }
        for(int i=0;i<nb_ennemis;i++) // Parcours du nombre d'unites_ennemies + alliées
        {
            unites_ennemies.Add(new Unite(-10, 0, 0, 1000, 1, 2,Team.Equipe2, Type_unitee.Melee, canva_pour_texte_pv, true));
            CreerUnite(unites_ennemies[i].PositionX, unites_ennemies[i].PositionY, unites_ennemies[i].PositionZ);
        }


        temps_passé_en_jeu.Initialisation_Timer();


        //Partie ici qui montre comment garder entre les scènes des variables globales
        int maValeur = PlayerPrefs.GetInt("nombre_unites_globales_ennemies_menu");
        Debug.Log(maValeur);
    }

    void Update()
    { 
            GestionJeu();
            // animatorController premierUniteController = unites[i].GetComponent<animatorController>();
            


            // for(int j=0;j<nb_alliee;j++)
            // {
            //     liste_texte_pv[j].MettreAJourTextePV(unites_ennemies[0].Pv,unites_alliees[0].PositionX,unites_alliees[0].PositionY,unites_alliees[0].PositionZ);
            // }
            // float positionX_texte = unites_alliees[i].PositionX ; // Exemple de position X (vous pouvez ajuster selon vos besoins)
            // float positionY_texte = unites_alliees[i].PositionY+40; // Exemple de position Y (vous pouvez ajuster selon vos besoins)
            // Debug.Log(positionX_texte);

            //liste_texte_pv[i].MettreAJourTextePV(unite2.Pv,positionX_texte,positionY_texte,0);

            // if(tab_gameobject_unite[i+nb_ennemis] != null && unites_ennemies[0].ParmiNous)
            // {

            //         if(unites_ennemies[0].Pv <= 0)
            //         {
            //             Debug.Log("L'unite 2 est morte");

            //             Destroy(tab_gameobject_unite[i+nb_ennemis]);
            //             tab_gameobject_unite.RemoveAt(i+nb_ennemis);
            //             //animator.SetBool("IsFighting",false);
            //             animator.SetBool("Won",true);
            //             unites_ennemies[0].ParmiNous=false;
            //             nb_ennemis--;

            //             premierUniteController.change_is_fighting(false);
            //             Debug.Log(premierUniteController.affiche_isfighting());
                        
                    
            //         }
            //         else
            //         {

                        
            //             unites_alliees[i].Attaquer(unites_ennemies[0]);
            //             //Debug.Log("PV de unite2 après attaque : " + unite2.Pv);
                        
            //         } 
            // }     




        // Ici on implémente le fait 1-de mettre a jour le compteur 2-d'afficher le temps dans la console si on appuie sur t
        if (Input.GetKeyDown(KeyCode.T))
        {
            affichage_temps=true;
        }
        temps_passé_en_jeu.Deroulement_timer_console(affichage_temps);

        

        affichage_temps=false;
    }

    void CreerUnite(float positionX, float positionY, float positionZ)
    {
        GameObject newUnite = Instantiate(prefab);
        newUnite.name = "Unite" + tab_gameobject_unite.Count.ToString();

        // Placer le cube à la position spécifiée
        newUnite.transform.position = new Vector3(positionX, positionY, positionZ);

        tab_gameobject_unite.Add(newUnite);
    }

    void GestionJeu(){
        for(int i=0;i<nb_alliee;i++) 
        {
            unites_alliees[i].GestionEvenement(unites_ennemies,nb_ennemis);
            if(tab_gameobject_unite[i] != null)
            {
                tab_gameobject_unite[i].transform.position = new Vector3(unites_alliees[i].PositionX, unites_alliees[i].PositionY, unites_alliees[i].PositionZ);
            }
        }
        for(int i=0;i<nb_ennemis;i++){
            unites_ennemies[i].GestionEvenement(unites_alliees,nb_alliee);
            if(tab_gameobject_unite[i+nb_alliee] != null)
            {
                tab_gameobject_unite[i+nb_alliee].transform.position = new Vector3(unites_ennemies[i].PositionX, unites_ennemies[i].PositionY, unites_ennemies[i].PositionZ);
            }
        }
    }
}
