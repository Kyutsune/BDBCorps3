using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

/*
Classe jeu qui va être celle qui va contenir notre boucle de jeu,essayons de conserver un maximum un systeme de MVC s'il vous plait

*/
public class Jeu : MonoBehaviour
{
    private List<GameObject> tab_gameobject_unite = new List<GameObject>();
    private List<Unite> unites_alliees = new List<Unite>();
    private List<Unite> unites_ennemies = new List<Unite>();

    private int nb_alliee_melee;
    private int nb_ennemis_melee;
    private int nb_alliee_distant;
    private int nb_ennemis_distant;

    private int nb_ennemis_total;
    private int nb_alliee_total;

    public GameObject prefabAllie;
    public GameObject prefabEnnemi;
    public GameObject prefabArcherAllie;
    public GameObject prefabArcherEnnemi;

    public Animator animatorMelee;
    public Animator animatorDistant;

    private Canvas canva_pour_texte_pv;
    private List<AffichageDesPVs> liste_texte_pv= new List<AffichageDesPVs>();

    Timer temps_passé_en_jeu=new Timer();
    bool affichage_temps=false;

    
    public AdministrationRegiments administrationRegiments = new AdministrationRegiments();

    public int modeJeu;
    

   

    void Start()
    {
        GameObject canvasObj = new GameObject("Canvas");
        canva_pour_texte_pv = canvasObj.AddComponent<Canvas>();
        canva_pour_texte_pv.renderMode = RenderMode.ScreenSpaceOverlay;

        //Partie ici qui montre comment garder entre les scènes des variables globales

        
        nb_alliee_melee=PlayerPrefs.GetInt("nombre_unites_globales_allies_menu", 1);
        nb_ennemis_melee=PlayerPrefs.GetInt("nombre_unites_globales_ennemies_menu", 1);

        nb_alliee_distant=PlayerPrefs.GetInt("nombre_unites_globales_alliesDistant_menu", 1);
        nb_ennemis_distant=PlayerPrefs.GetInt("nombre_unites_globales_ennemiesDistant_menu", 1);

        nb_alliee_total=nb_alliee_melee+nb_alliee_distant;
        nb_ennemis_total=nb_ennemis_melee+nb_ennemis_distant;

        animatorMelee = GetComponent<Animator>();
        animatorDistant = GetComponent<Animator>();

        modeJeu = PlayerPrefs.GetInt("mode_jeu_menu", 1);

        
        for(int i=0;i<nb_alliee_total;i++) 
        {
            // Créer le texte PV et l'ajouter à la liste
            // AffichageDesPVs textePVUnite=GetComponent<AffichageDesPVs>();
            // textePVUnite.CreerTextePV(canva_pour_texte_pv, unites_alliees[i].PositionX, unites_alliees[i].PositionY+40, unites_alliees[i].PositionZ);
            // liste_texte_pv.Add(textePVUnite);
            if(i<nb_alliee_melee) ////////////////////Alliee Melee////////////////////
            {
                unites_alliees.Add(new Paladin(Team.EquipeBleue));
                CreerUnite(unites_alliees[i].PositionX, unites_alliees[i].PositionY, unites_alliees[i].PositionZ,1,1);
            }
            else /////////////////Alliée Distant/////////////////////
            {
                unites_alliees.Add(new Archer(Team.EquipeBleue));
                CreerUnite(unites_alliees[i].PositionX, unites_alliees[i].PositionY, unites_alliees[i].PositionZ,1,2);
            }

           
        }
        
        for(int i=0;i<nb_ennemis_total;i++) 
        {
            if(i<nb_ennemis_melee) ////////////////////Ennemis Melee////////////////////
            {
                unites_ennemies.Add(new Paladin(Team.EquipeRouge));
                CreerUnite(unites_ennemies[i].PositionX, unites_ennemies[i].PositionY, unites_ennemies[i].PositionZ,2,1);
            }
            else /////////////////Ennemis Distant////////////////////
            {
                unites_ennemies.Add(new Archer(Team.EquipeRouge));
                CreerUnite(unites_ennemies[i].PositionX, unites_ennemies[i].PositionY, unites_ennemies[i].PositionZ,2,2);

            }

            
        }

        
        temps_passé_en_jeu.Initialisation_Timer();

        

        administrationRegiments.InitialisationNombreRegiments(nb_alliee_total, nb_ennemis_total);
        administrationRegiments.GestionRegiments(nb_alliee_total, nb_ennemis_total, unites_alliees,  unites_ennemies);

        // Debug.Log(tab_regiments_alliees[0].verif_unite_autre_regiment(tab_regiments_alliees[1]));

        // test.Creation_Regiment(unites_alliees[0]);
        // test.AfficheList();
        // test.cherche_unite_dans_regiment(unites_alliees,nb_alliee_total);
        // test.AfficheList();
        

        // Debug.Log(nb_alliee_total);

    }

    void Update()
    { 
        if(modeJeu == 0)
        {
            GestionJeuIndividuel();
        }
        else{
            administrationRegiments.RegroupementRegiment(nb_alliee_total,nb_ennemis_total,tab_gameobject_unite,unites_alliees,unites_ennemies);
            administrationRegiments.GestionAttaqueRegiment();
        }

        animationRegiment();
        
        
        // Projectiles.deplacementObjetProjectile();
        // for(int j=0;j<nb_alliee_melee;j++)
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

    void animationRegiment(){
        for(int i=0;i<nb_alliee_total;i++)
        {
            animatorController allieeUniteController = tab_gameobject_unite[i].GetComponent<animatorController>();
            if(unites_alliees[i].Run==true){
                allieeUniteController.setRunning(true);
            }
            if(unites_alliees[i].Walk==true){
                allieeUniteController.setWalking(true);
            }
            if(unites_alliees[i].Run==false && unites_alliees[i].Walk==false){
                allieeUniteController.setRunning(false);
                allieeUniteController.setWalking(false);
            }
        }

        for(int i=0;i<nb_ennemis_total;i++)
        {
            animatorController ennemiUniteController = tab_gameobject_unite[nb_alliee_total+i].GetComponent<animatorController>();
            if(unites_ennemies[i].Run==true){
                ennemiUniteController.setRunning(true);
            }
            if(unites_ennemies[i].Walk==true){
                ennemiUniteController.setWalking(true);
            }
            if(unites_ennemies[i].Run==false && unites_ennemies[i].Walk==false){
                ennemiUniteController.setRunning(false);
                ennemiUniteController.setWalking(false);
            }
        }
    }

    void CreerUnite(float positionX, float positionY, float positionZ,int equipe, int type)
    {

        if(type==1)
        {
            if(equipe==1)
                CreerPrefab("UniteAlliee","Melee",positionX,positionY,positionZ);
            if(equipe==2)
                CreerPrefab("UniteEnnemis","Melee",positionX,positionY,positionZ);
        }
        if(type==2)
        {
            if(equipe==1)
                CreerPrefab("UniteAlliee","Distant",positionX,positionY,positionZ);
            if(equipe==2)
                CreerPrefab("UniteEnnemis","Distant",positionX,positionY,positionZ);
        }
    }

    void GestionJeuIndividuel(){
        for(int i=0;i<nb_alliee_total;i++) 
        {
            if(i<nb_alliee_melee)
            {
                ParcoursEvenement(i,"alliee");
                
            }
            else
            {
                ParcoursEvenement(i,"alliee");
            }           
        }
        
        for(int i=0;i<nb_ennemis_total;i++){
            if(i<nb_ennemis_melee)
            {
                ParcoursEvenement(i,"ennemi");
            }
            else
            {
                ParcoursEvenement(i,"ennemi");
            }
            
        }
    }

    void CreerPrefab(string ChoixUnite,string TypeUnite, float positionX, float positionY, float positionZ)
    {
        GameObject newUnite;
        if(ChoixUnite == "UniteAlliee")
            if(TypeUnite == "Melee"){
                newUnite = Instantiate(prefabAllie);
            }
            else if(TypeUnite == "Distant"){
                newUnite = Instantiate(prefabArcherAllie);
            }
            else 
                newUnite = null;
        else if(ChoixUnite == "UniteEnnemis")
        {
            if(TypeUnite == "Melee"){
                newUnite = Instantiate(prefabEnnemi); 
            }
            else if(TypeUnite == "Distant"){
                newUnite = Instantiate(prefabArcherEnnemi); 
            }
            else 
                newUnite = null;
        }
        else 
            newUnite = null;
             
            
        newUnite.name = ChoixUnite + TypeUnite + tab_gameobject_unite.Count.ToString();

        // Placer l'unité à la position spécifiée
        newUnite.transform.position = new Vector3(positionX, positionY, positionZ);

        tab_gameobject_unite.Add(newUnite);
    }

    void ParcoursEvenement(int posTab,string equipe)
    {
        if(equipe == "alliee")
        {
            animatorController allieUniteController = tab_gameobject_unite[posTab].GetComponent<animatorController>();
            bool etat = unites_alliees[posTab].GestionEvenement(unites_ennemies,nb_ennemis_total,allieUniteController);
            tab_gameobject_unite[posTab].transform.position = new Vector3(unites_alliees[posTab].PositionX, unites_alliees[posTab].PositionY, unites_alliees[posTab].PositionZ);
            if(etat == true){
                tab_gameobject_unite.RemoveAt(posTab);
                unites_alliees.RemoveAt(posTab);
                nb_alliee_melee--;
                nb_alliee_total--;
            }
        }
        else
        {
            animatorController ennemiUniteController = tab_gameobject_unite[posTab+nb_alliee_total].GetComponent<animatorController>();
                bool etat = unites_ennemies[posTab].GestionEvenement(unites_alliees,nb_alliee_total,ennemiUniteController);
                tab_gameobject_unite[posTab+nb_alliee_total].transform.position = new Vector3(unites_ennemies[posTab].PositionX, unites_ennemies[posTab].PositionY, unites_ennemies[posTab].PositionZ);
                if(etat == true){
                    tab_gameobject_unite.RemoveAt(posTab+nb_alliee_total);
                    unites_ennemies.RemoveAt(posTab);
                    nb_ennemis_melee--;
                    nb_ennemis_total--;
                }
        }
    }




}
