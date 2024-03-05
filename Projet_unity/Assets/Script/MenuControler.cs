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

    public GameObject inputField;
    public GameObject textDisplay;

    //Récupérer l'entrée du joueur
    public void GetInputText()
    {
        nbEnnemisentree = int.Parse(inputField.GetComponent<Text>().text);
        textDisplay.GetComponent<Text>().text = "Vous avez entré : " + nbEnnemisentree; //Permet d'afficher ce que le joueur a entré directement sur le jeu
    }

    public void nbAlliés(int param)
    {
        param=nbEnnemisentree;
        
    }

    /*
    public void AfficherVie(Unite unite)
    {
        textDisplay.GetComponent<Text>().text = "Nombre de pv" + unite + unite.Pv ;
    }

    */

}
 