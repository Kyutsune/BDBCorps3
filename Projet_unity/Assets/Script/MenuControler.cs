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

    public void GetInputTextEnnemis()
    {
        nombre_unites_globales_ennemies_menu = int.Parse(inputFieldEnnemis.GetComponent<Text>().text); //On récupère l'entrée du joueur et on la convertis en un entier
        textDisplayEnnemis.GetComponent<Text>().text = "Vous avez entré : " + nombre_unites_globales_ennemies_menu; //Permet d'afficher ce que le joueur a entré directement sur le jeu
        PlayerPrefs.SetInt("nombre_unites_globales_ennemies_menu", nombre_unites_globales_ennemies_menu);
        PlayerPrefs.Save();
    }

    public void GetInputTextAllies()
    {
        nombre_unites_globales_allies_menu = int.Parse(inputFieldAlliés.GetComponent<Text>().text); //On récupère l'entrée du joueur et on la convertis en un entier
        textDisplayAlliés.GetComponent<Text>().text = "Vous avez entré : " + nombre_unites_globales_allies_menu; //Permet d'afficher ce que le joueur a entré directement sur le jeu
        PlayerPrefs.SetInt("nombre_unites_globales_allies_menu", nombre_unites_globales_allies_menu);
        PlayerPrefs.Save();
    }
    


}
 