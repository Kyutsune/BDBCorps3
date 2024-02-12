using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class AffichageDesPVs : MonoBehaviour
{
    public GameObject nouveauTextePV;
    public TMP_FontAsset maPolice;

    public void MettreAJourTextePV(double pv)
    {
        // Si aucun texte n'a été créé, sortir
        if (nouveauTextePV == null)
            return;

        // Mettre à jour le texte avec les PVs fournis
        nouveauTextePV.GetComponent<TextMeshProUGUI>().text = "pv du héros : " + pv.ToString();
    }


    public GameObject  CreerTextePV(Canvas canvas, float positionX, float positionY, float positionZ)
    {
        // Créer un GameObject de type TextMeshPro
        nouveauTextePV = new GameObject("Nouveau_Texte_pv");
        TextMeshProUGUI texteComponent = nouveauTextePV.AddComponent<TextMeshProUGUI>();

        // Charger la police TextMeshPro
        TMP_FontAsset maPolice = Resources.Load<TMP_FontAsset>("Fonts & Materials/Arial SDF");



        // Ajouter le composant TextMeshPro au GameObject
        texteComponent.text = "Nouveau texte";
        texteComponent.name = "test_texte";

        // Définir la police et la taille du texte si nécessaire
        if (maPolice != null)
            texteComponent.font = maPolice;

        // Positionner le GameObject à la position spécifiée
        nouveauTextePV.transform.position = new Vector3(positionX, positionY, positionZ);

        // Assurez-vous que le texte créé est enfant du Canvas spécifié
        nouveauTextePV.transform.SetParent(canvas.transform, false);

        return nouveauTextePV;
    }

// Fonction pour supprimer les canavs et les textes affichés
// Destruction dans un premier temps du texte puis du canva
    public void DetruireTexteEtCanvas(GameObject texteEtCanvas)
    {
        if (texteEtCanvas != null)
        {
            // Supprimer le composant TextMeshProUGUI du GameObject texte
            TextMeshProUGUI texteComponent = texteEtCanvas.GetComponent<TextMeshProUGUI>();
            if (texteComponent != null)
            {
                Destroy(texteComponent);
            }

            // Supprimer le GameObject (qui contient le texte et le Canvas)
            Destroy(texteEtCanvas);
        }
    }


}
