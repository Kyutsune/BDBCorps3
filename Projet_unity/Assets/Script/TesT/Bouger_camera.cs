using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Module Servant à mouvoir et utiliser la caméra,
touches: 
z:avancer 
q:aller à gauche 
s:reculer 
d:aller à droite
Espace:monter
Shift:Descendre
Clic droit souris+bouger:Bouger la caméra 
*/




public class ScriptCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public float movementSpeed = 5f;
    public float rotationSpeed = 3f;
    public float verticalLookLimit = 80f;
    public float groundHeight = 0.5f; // Ajoutez cette variable pour définir la hauteur du sol

    private float rotationX = 0f;

    // Update is called once per frame
    void Update()
    {
        // Déplacement de la caméra basé sur les entrées du joueur
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * movementSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Limite la position verticale pour empêcher la caméra d'aller en dessous du sol
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, groundHeight, float.MaxValue), transform.position.z);


        // Rotation de la caméra avec le clic droit de la souris
        if (Input.GetMouseButton(1)) // 1 représente le bouton droit de la souris
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            // Rotation horizontale (gauche/droite)
            transform.Rotate(Vector3.up * mouseX);

            // Rotation verticale (haut/bas), avec limite verticale
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -verticalLookLimit, verticalLookLimit);

            transform.localRotation = Quaternion.Euler(rotationX, transform.localRotation.eulerAngles.y, 0f);
        }

        // Montée de la caméra lorsque la touche espace est enfoncée
        if (Input.GetKey(KeyCode.Space))
        {
            float verticalMovement = movementSpeed * Time.deltaTime;
            transform.Translate(Vector3.up * verticalMovement);
        }

        // Descente de la caméra lorsque la touche Shift est enfoncée
        if (Input.GetKey(KeyCode.LeftShift))
        {
            float verticalMovement = -movementSpeed * Time.deltaTime;
            transform.Translate(Vector3.up * verticalMovement);
        }
    }
}