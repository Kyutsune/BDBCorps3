using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Class servant à bouger les objets avec la souris 
*/

public class Souris : MonoBehaviour
{
    public float lerpSpeed = 500f;

    // Update is called once per frame
    void Update()
    {
        // Obtenez la position du clic de la souris
        Vector3 posClick = Input.mousePosition;

        // Convertissez les coordonnées de l'écran en coordonnées du monde 3D
        Vector3 posClickWorld = Camera.main.ScreenToWorldPoint(new Vector3(posClick.x, posClick.y, 10f));
        // Bouger l'objet en fonction de la caméra
        Vector3 directionToCamera = (Camera.main.transform.position - transform.position);
        Vector3 velocity = Vector3.zero;

        if (Input.GetMouseButton(0))
        {  
             // Convertit les coordonnées de l'écran en un rayon dans la scène
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Déclare une variable RaycastHit pour stocker les informations sur la collision
            RaycastHit hit;
            
             if (Physics.Raycast(ray, out hit))
            {
                // Vérifie si l'objet cliqué est le carré
                if (hit.collider.gameObject == gameObject)
                {
                    // Interpolez la position actuelle vers la position du clic
                    transform.position = Vector3.Lerp(transform.position, posClickWorld, lerpSpeed);
                    if (Input.GetKey(KeyCode.P))
                    {
                        // Eloigne l'objet
                        velocity += directionToCamera * Time.deltaTime * 10;
                    }
                    else if (Input.GetKey(KeyCode.O))
                    {
                        // Rapproche l'objet
                        transform.Translate(-directionToCamera * Time.deltaTime * 10);
                    }
                 }
                transform.Translate(velocity);
            }
        }
    
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clic gauche pressé");
            Debug.Log("Coordonnées du clic : X=" + posClickWorld.x + ", Y=" + posClickWorld.y + ", Z=" + posClickWorld.z);
        }
    }
}
