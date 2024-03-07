using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TMP_Text  timerText; // Référence au composant Text pour afficher le temps

    void Start()
    {
        
    }

    void Update()
    {
        float secondes = PlayerPrefs.GetFloat("secondes_ecoulees");
        float minutes=PlayerPrefs.GetFloat("minutes_ecoulees");
        // Mettre à jour le texte avec le temps écoulé
        timerText.text = string.Format("{0:00}:{1:00}",minutes,secondes);


        // Récupérer la position de la caméra
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            // Définir la position Z de l'objet que vous souhaitez placer au centre de l'écran
            float distanceFromCamera = 10f; // par exemple

            // Obtenir la position centrale de l'écran en coordonnées de la vue (Viewport)
            Vector3 centerViewport = new Vector3(1f, 0.8f, distanceFromCamera);

            // Convertir la position de la vue en coordonnées mondiales
            Vector3 centerWorld = mainCamera.ViewportToWorldPoint(centerViewport);

            // Placer l'objet au centre de l'écran
            timerText.transform.position = centerWorld;
        }

        
    }
}
