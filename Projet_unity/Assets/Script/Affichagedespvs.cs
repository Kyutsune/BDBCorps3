using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class AffichageDesPVs : MonoBehaviour
{
    public GameObject nouveauTextePV;
    public TMP_FontAsset maPolice;
    int nombre_affichage_actuel=0;

    public void MettreAJourTextePV(double pv,float positionX, float positionY, float positionZ)
    {   
        // Mettre à jour le texte avec les PVs fournis
        nouveauTextePV.GetComponent<TextMeshProUGUI>().text = "pv du héros : " + pv.ToString();
        nouveauTextePV.transform.position = new Vector3(positionX, positionY, positionZ);      
    }


    public GameObject  CreerTextePV(Canvas canvas, float positionX, float positionY, float positionZ)
    {
        // Créer un GameObject de type TextMeshPro
        nouveauTextePV = new GameObject("Nouveau_Texte_pv");
        TextMeshProUGUI texteComponent = nouveauTextePV.AddComponent<TextMeshProUGUI>();

        // Charger la police TextMeshPro
        TMP_FontAsset maPolice = Resources.Load<TMP_FontAsset>("Fonts & Materials/Arial SDF");



        // Ajouter le composant TextMeshPro au GameObject
        texteComponent.text = "Vie de l'unité:";
        texteComponent.name = "affichage_vie unite "+nombre_affichage_actuel;
        texteComponent.fontSize=12;

        // Définir la police et la taille du texte si nécessaire
        if (maPolice != null)
            texteComponent.font = maPolice;

        // Positionner le GameObject à la position spécifiée
        nouveauTextePV.transform.position = new Vector3(positionX, positionY, positionZ);

        // Assurez-vous que le texte créé est enfant du Canvas spécifié
        nouveauTextePV.transform.SetParent(canvas.transform, false);
        nombre_affichage_actuel++;

        return nouveauTextePV;
    }

}
