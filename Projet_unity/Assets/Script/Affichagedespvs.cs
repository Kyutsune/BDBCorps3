using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Affichagedespvs:MonoBehaviour
{
    public GameObject nouveauTextePV;

    public void MettreAJourTextePV(double pv)
    {
        // Mettez à jour le texte avec les PVs fournis
        nouveauTextePV.GetComponent<TextMeshProUGUI>().text = "pv du héros : " + pv;
    }

    public void creerTextePv(Transform parent, float positionX, float positionY)
    {
        // Charger la police de caractères depuis les ressources de votre projet
        TMP_FontAsset maPolice = Resources.Load<TMP_FontAsset>("LiberationSans SDF");

        // Créer un nouveau GameObject vide
        nouveauTextePV = new GameObject("Nouveau_Texte_pv");

        // Ajouter le composant TextMeshProUGUI au GameObject
        TextMeshProUGUI texteComponent = nouveauTextePV.AddComponent<TextMeshProUGUI>();

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
        nouveauTextePV.transform.position = new Vector3(positionX, positionY, 0f);

        // Assurez-vous que le texte créé est enfant du GameObject portant ce script
        nouveauTextePV.transform.SetParent(parent, false);
    }
}
