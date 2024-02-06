using UnityEngine;

/*
Classe servant à faire apparaître un message lorsque l'on appuie sur un gameobject sur lequel il est appliqué 
*/



public class InteractionClic : MonoBehaviour
{
    void Update()
    {
        // Vérifie si le bouton de la souris est cliqué (pour les PC) ou si l'écran est touché (pour les appareils mobiles)
        if (Input.GetMouseButtonDown(0))
        {
            // Convertit les coordonnées de l'écran en un rayon dans la scène
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Déclare une variable RaycastHit pour stocker les informations sur la collision
            RaycastHit hit;

            // Effectue le rayon et vérifie s'il y a une collision
            if (Physics.Raycast(ray, out hit))
            {
                // Vérifie si l'objet cliqué est le carré
                if (hit.collider.gameObject == gameObject)
                {
                    // Appelle la fonction fct()
                    Debug.Log("tu as cliqué sur le carré !");
                    fct();
                }
            }
        }
    }

    // La fonction à appeler lors du clic sur le carré
    void fct()
    {
        return;
        // Ajoutez ici le code que vous souhaitez exécuter lors du clic sur le carré
    }
}
