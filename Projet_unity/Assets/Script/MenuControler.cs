using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
Classe MenuControler qui va permettre la gestion de tout les changements de scenes, ainsi que les 
entrées du joueur...
*/

public class Drag : MonoBehaviour
{
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

    public GameObject inputField;
    public GameObject textDisplay;
    public int nombre_unites_globales_ennemies_menu;

    //Récupérer l'entrée du joueur
    public void GetInputText()
    {
        nombre_unites_globales_ennemies_menu = int.Parse(inputField.GetComponent<Text>().text); //On récupère l'entrée du joueur et on la convertis en un entier
        textDisplay.GetComponent<Text>().text = "Vous avez entré : " + nombre_unites_globales_ennemies_menu; //Permet d'afficher ce que le joueur a entré directement sur le jeu
        PlayerPrefs.SetInt("nombre_unites_globales_ennemies_menu", nombre_unites_globales_ennemies_menu);
        PlayerPrefs.Save();


    }

    /*
    public void AfficherVie(Unite unite)
    {
        textDisplay.GetComponent<Text>().text = "Nombre de pv" + unite + unite.Pv ;
    }

    */

}
 