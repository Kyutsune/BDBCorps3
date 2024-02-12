using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AffichageDesPVs : MonoBehaviour
{
    public GameObject nouveauTextePV;
    public Font maPolice;

    void Start()
    {
        // Vous pouvez charger la police de caractères ici si vous le souhaitez
        // maPolice = Resources.Load<Font>("NomDeVotrePolice");
    }

    public void MettreAJourTextePV(double pv)
    {
        // Si aucun texte n'a été créé, sortir
        if (nouveauTextePV == null)
            return;

        // Mettre à jour le texte avec les PVs fournis
        nouveauTextePV.GetComponent<Text>().text = "pv du héros : " + pv;
    }

    public void CreerTextePV(Transform parent, float positionX, float positionY, float positionZ)
    {
        // Détruire l'ancien texte s'il existe
        if (nouveauTextePV != null)
            Destroy(nouveauTextePV);

        // Créer un nouveau GameObject vide
        nouveauTextePV = new GameObject("Nouveau_Texte_pv");

        // Ajouter le composant Text au GameObject
        Text texteComponent = nouveauTextePV.AddComponent<Text>();

        // Définir le texte initial
        texteComponent.text = "Nouveau texte";

        texteComponent.name = "test_texte";

        // Définir la police et la taille du texte si nécessaire
        if (maPolice != null)
        {
            // Appliquer la police de caractères au composant texte
            texteComponent.font = maPolice;
        }

        // Positionner le GameObject à la position spécifiée
        nouveauTextePV.transform.position = new Vector3(positionX, positionY, positionZ);

        // Assurez-vous que le texte créé est enfant du GameObject portant ce script
        nouveauTextePV.transform.SetParent(parent, false);

        MettreAJourTextePV(0); 
    }
}
