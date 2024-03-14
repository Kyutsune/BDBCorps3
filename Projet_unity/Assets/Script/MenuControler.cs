using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
Classe MenuControler qui va permettre la gestion de tout les changements de scenes, ainsi que les 
entrées du joueur...
*/


public class MenuControler : MonoBehaviour
{
    public static int nbEnnemisentree;

    //Permet de changer de scene
    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //Quitter le jeu
    public void Quit()
    {
        Application.Quit();
    }

    public GameObject inputFieldEnnemis;
    public GameObject textDisplayEnnemis;

    public GameObject inputFieldAlliés;
    public GameObject textDisplayAlliés;

    public int nombre_unites_globales_ennemies_menu;
    public int nombre_unites_globales_allies_menu;

    public GameObject inputFieldEnnemisDistant;
    public GameObject textDisplayEnnemisDistant;

    public GameObject inputFieldAlliésDistant;
    public GameObject textDisplayAlliésDistant;

    public int nombre_unites_globales_ennemiesDistant_menu;
    public int nombre_unites_globales_alliesDistant_menu;

    
 

        
    
    

    public void GetInputTextEnnemis()
    {
        nombre_unites_globales_ennemies_menu = int.Parse(inputFieldEnnemis.GetComponent<Text>().text); //On récupère l'entrée du joueur et on la convertis en un entier
        textDisplayEnnemis.GetComponent<Text>().text = "Vous avez entré : " + nombre_unites_globales_ennemies_menu + " ennemis de type mélée "; //Permet d'afficher ce que le joueur a entré directement sur le jeu
        PlayerPrefs.SetInt("nombre_unites_globales_ennemies_menu", nombre_unites_globales_ennemies_menu);
        PlayerPrefs.Save();
        
    }

    public void GetInputTextAllies()
    {
        nombre_unites_globales_allies_menu = int.Parse(inputFieldAlliés.GetComponent<Text>().text); //On récupère l'entrée du joueur et on la convertis en un entier
        textDisplayAlliés.GetComponent<Text>().text = "Vous avez entré : " + nombre_unites_globales_allies_menu + " alliés de type mélée "; //Permet d'afficher ce que le joueur a entré directement sur le jeu
        PlayerPrefs.SetInt("nombre_unites_globales_allies_menu", nombre_unites_globales_allies_menu);
        PlayerPrefs.Save();
    }

    public void GetInputTextEnnemisDistant()
    {
        nombre_unites_globales_ennemiesDistant_menu = int.Parse(inputFieldEnnemisDistant.GetComponent<Text>().text); //On récupère l'entrée du joueur et on la convertis en un entier
        textDisplayEnnemisDistant.GetComponent<Text>().text = "Vous avez entré : " + nombre_unites_globales_ennemiesDistant_menu + " ennemis de type distant "; //Permet d'afficher ce que le joueur a entré directement sur le jeu
        PlayerPrefs.SetInt("nombre_unites_globales_ennemiesDistant_menu", nombre_unites_globales_ennemiesDistant_menu);
        PlayerPrefs.Save();
    }

    public void GetInputTextAlliesDistant()
    {
        nombre_unites_globales_alliesDistant_menu = int.Parse(inputFieldAlliésDistant.GetComponent<Text>().text); //On récupère l'entrée du joueur et on la convertis en un entier      
        textDisplayAlliésDistant.GetComponent<Text>().text = "Vous avez entré : " + nombre_unites_globales_alliesDistant_menu + " alliés de type distant "; //Permet d'afficher ce que le joueur a entré directement sur le jeu
        PlayerPrefs.SetInt("nombre_unites_globales_alliesDistant_menu", nombre_unites_globales_alliesDistant_menu);
        PlayerPrefs.Save();
    }
    
}
 