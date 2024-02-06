using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Classe jeu qui va être celle qui va contenir notre boucle de jeu,essayons de conserver un maximum un systeme de MVC s'il vous plait

*/
public class Jeu : MonoBehaviour
{
    private List<GameObject> unites = new List<GameObject>();
    private Unite unite1;
    private Unite unite2;
    public GameObject prefabToInstantiate;
    private Animator animator;

    void Start()
    {
        unite1 = new Unite(0, 0, 100, Unite_alliee_ennemie.Allie, Type_unitee.Melee);
        unite2 = new Unite(10, 0, 100, Unite_alliee_ennemie.Ennemie, Type_unitee.Melee);

        animator = GetComponent<Animator>();

        // Appeler la fonction CreerCube en passant les positions des unités
        CreerUnite(unite1.PositionX, unite1.PositionY);
        CreerUnite(unite2.PositionX, unite2.PositionY);
    }

    void Update()
    {
        if (unite1 != null && unite2 != null && unites != null && unites.Count >= 2)
        {
            bool EstEnMouvement = false;
            unite1.DeplacerVersUniteDifferente(unite2,out EstEnMouvement);
            animator.SetBool("EstEnMouvement", EstEnMouvement);
            unite2.DeplacerVersUniteDifferente(unite1,out EstEnMouvement);
            animator.SetBool("EstEnMouvement", EstEnMouvement);

            // Mettre à jour la position des cubes
            unites[0].transform.position = new Vector3(unite1.PositionX, unite1.PositionY, 0);
            unites[1].transform.position = new Vector3(unite2.PositionX, unite2.PositionY, 0);
        }

    }

    void CreerUnite(float positionX, float positionY)
    {
        GameObject newUnite = GameObject.Instantiate(prefabToInstantiate);
        newUnite.name = "Unite" + unites.Count.ToString();

        // Placer le cube à la position spécifiée
        newUnite.transform.position = new Vector3(positionX, positionY, 0);

        unites.Add(newUnite);
    }
}