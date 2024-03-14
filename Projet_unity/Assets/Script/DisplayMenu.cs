using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
Classe DisplayMenu qui va permettre la gestion de l'affichage de toutes les données entrées par le joueur
*/
public class DisplayMenu : MonoBehaviour
{
    int sauv_textDisplayNbEnnemisMelee;
    int sauv_textDisplayNbEnnemisDistant;
    int sauv_textDisplayNbAlliesMelee;
    int sauv_textDisplayNbAlliesDistant;

    public GameObject textDisplayNbEnnemisMelee;
    public GameObject textDisplayNbEnnemisDistant;
    public GameObject textDisplayNbAlliesMelee;
    public GameObject textDisplayNbAlliesDistant;
    
    void Start()
    {
        sauv_textDisplayNbEnnemisMelee = PlayerPrefs.GetInt("nombre_unites_globales_ennemies_menu", 1);
        sauv_textDisplayNbEnnemisDistant = PlayerPrefs.GetInt("nombre_unites_globales_ennemiesDistant_menu", 1);
        sauv_textDisplayNbAlliesMelee = PlayerPrefs.GetInt("nombre_unites_globales_allies_menu", 1);
        sauv_textDisplayNbAlliesDistant = PlayerPrefs.GetInt("nombre_unites_globales_alliesDistant_menu", 1); 
    }

    
    void Update()
    {       
        DisplayInputTextEnnemis();
        DisplayInputTextEnnemisDistant();
        DisplplayInputTextAllies();
        DisplplayInputTextAlliesDistant();
    }

    public void DisplayInputTextEnnemis()
    {
        sauv_textDisplayNbEnnemisMelee = PlayerPrefs.GetInt("nombre_unites_globales_ennemies_menu", 1);
        textDisplayNbEnnemisMelee.GetComponent<Text>().text = "Actuellement: " + sauv_textDisplayNbEnnemisMelee;
    }

    public void DisplayInputTextEnnemisDistant()
    {
        sauv_textDisplayNbEnnemisDistant = PlayerPrefs.GetInt("nombre_unites_globales_ennemiesDistant_menu", 1);
        textDisplayNbEnnemisDistant.GetComponent<Text>().text = "Actuellement: " + sauv_textDisplayNbEnnemisDistant;
    }

    public void DisplplayInputTextAllies()
    {
        sauv_textDisplayNbAlliesMelee = PlayerPrefs.GetInt("nombre_unites_globales_allies_menu", 1);
        textDisplayNbAlliesMelee.GetComponent<Text>().text = "Actuellement: " + sauv_textDisplayNbAlliesMelee;
    }

    public void DisplplayInputTextAlliesDistant()
    {
        sauv_textDisplayNbAlliesDistant = PlayerPrefs.GetInt("nombre_unites_globales_alliesDistant_menu", 1);
        textDisplayNbAlliesDistant.GetComponent<Text>().text = "Actuellement: " + sauv_textDisplayNbAlliesDistant;
    }
    
}
