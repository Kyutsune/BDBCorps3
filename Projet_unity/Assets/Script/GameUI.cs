using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Text timerText; // Référence au composant Text pour afficher le temps

    void Start()
    {

    }

    void Update()
    {
        int secondes = PlayerPrefs.GetFloat("secondes_ecoulees");
        int minutes=PlayerPrefs.GetFloat("minutes_ecoulees");
           // Mettre à jour le texte avec le temps écoulé
            timerText.text = string.Format("{0:00}:{1:00}",minutes,secondes);
        
    }
}
