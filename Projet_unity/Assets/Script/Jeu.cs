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

    public GameObject prefabAllie;
    public GameObject prefabEnnemi;
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

        //Partie ici qui montre comment garder entre les scènes des variables globales

        
        nb_alliee=PlayerPrefs.GetInt("nombre_unites_globales_allies_menu", 1);
        nb_ennemis=PlayerPrefs.GetInt("nombre_unites_globales_ennemies_menu", 1);
    
        animator = GetComponent<Animator>();

        for(int i=0;i<nb_alliee;i++) // Parcours du nombre d'unites_ennemies + alliées
        {
            // Créer le texte PV et l'ajouter à la liste
            // AffichageDesPVs textePVUnite=GetComponent<AffichageDesPVs>();
            // textePVUnite.CreerTextePV(canva_pour_texte_pv, unites_alliees[i].PositionX, unites_alliees[i].PositionY+40, unites_alliees[i].PositionZ);
            // liste_texte_pv.Add(textePVUnite);

            unites_alliees.Add(new Unite(100, 2, 2f, Team.Equipe1, Type_unitee.Melee, canva_pour_texte_pv, true,50f));
            CreerUnite(unites_alliees[i].PositionX, unites_alliees[i].PositionY, unites_alliees[i].PositionZ,1);
        }
        for(int i=0;i<nb_ennemis;i++) // Parcours du nombre d'unites_ennemies + alliées
        {
            unites_ennemies.Add(new Unite(100, 2, 2f,Team.Equipe2, Type_unitee.Melee, canva_pour_texte_pv, true,2f));
            CreerUnite(unites_ennemies[i].PositionX, unites_ennemies[i].PositionY, unites_ennemies[i].PositionZ,2);
        }

        // Debug.Log(PlayerPrefs.GetInt("nombre_unites_globales_ennemies_menu"));

        temps_passé_en_jeu.Initialisation_Timer();

    }

    void Update()
    { 
        GestionJeu();

        // for(int j=0;j<nb_alliee;j++)
        // {
        //     liste_texte_pv[j].MettreAJourTextePV(unites_ennemies[0].Pv,unites_alliees[0].PositionX,unites_alliees[0].PositionY,unites_alliees[0].PositionZ);
        // }
        // float positionX_texte = unites_alliees[i].PositionX ; // Exemple de position X (vous pouvez ajuster selon vos besoins)
        // float positionY_texte = unites_alliees[i].PositionY+40; // Exemple de position Y (vous pouvez ajuster selon vos besoins)
        // Debug.Log(positionX_texte);

        //liste_texte_pv[i].MettreAJourTextePV(unite2.Pv,positionX_texte,positionY_texte,0);


        // Ici on implémente le fait 1-de mettre a jour le compteur 2-d'afficher le temps dans la console si on appuie sur t
        if (Input.GetKeyDown(KeyCode.T))
        {
            affichage_temps=true;
        }
        temps_passé_en_jeu.Deroulement_timer_console(affichage_temps);

        

        affichage_temps=false;
    }

    void CreerUnite(float positionX, float positionY, float positionZ,int equipe)
    {
        if(equipe==1){
            GameObject newUnite = Instantiate(prefabAllie);
            newUnite.name = "UniteAlliee" + tab_gameobject_unite.Count.ToString();

            // Placer l'unité à la position spécifiée
            newUnite.transform.position = new Vector3(positionX, positionY, positionZ);

            tab_gameobject_unite.Add(newUnite);
        }
        if(equipe==2){
            GameObject newUnite = Instantiate(prefabEnnemi);
            newUnite.name = "UniteEnnemie" + tab_gameobject_unite.Count.ToString();

            // Placer l'unité à la position spécifiée
            newUnite.transform.position = new Vector3(positionX, positionY, positionZ);

            tab_gameobject_unite.Add(newUnite);
        }
    }

    void GestionJeu(){
        for(int i=0;i<nb_alliee;i++) 
        {
            animatorController allieUniteController = tab_gameobject_unite[i].GetComponent<animatorController>();
            bool etat = unites_alliees[i].GestionEvenement(unites_ennemies,nb_ennemis,allieUniteController);
            tab_gameobject_unite[i].transform.position = new Vector3(unites_alliees[i].PositionX, unites_alliees[i].PositionY, unites_alliees[i].PositionZ);
            if(etat == true){
                tab_gameobject_unite.RemoveAt(i);
                unites_alliees.RemoveAt(i);
                nb_alliee--;
            }
        }
        for(int i=0;i<nb_ennemis;i++){
            animatorController ennemiUniteController = tab_gameobject_unite[i+nb_alliee].GetComponent<animatorController>();
            bool etat = unites_ennemies[i].GestionEvenement(unites_alliees,nb_alliee,ennemiUniteController);
            tab_gameobject_unite[i+nb_alliee].transform.position = new Vector3(unites_ennemies[i].PositionX, unites_ennemies[i].PositionY, unites_ennemies[i].PositionZ);
            if(etat == true){
                tab_gameobject_unite.RemoveAt(i+nb_alliee);
                unites_ennemies.RemoveAt(i);
                nb_ennemis--;
            }
        }
    }

}
