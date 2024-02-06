using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Classe jeu qui va être celle qui va contenir notre boucle de jeu,essayons de conserver un maximum un systeme de MVC s'il vous plait

*/






public class Jeu : MonoBehaviour
{
    private List<GameObject> cubes = new List<GameObject>();
    private Unite unite1;
    private Unite unite2;

    void Start()
    {
        unite1 = new Unite(0, 0, 100, Unite_alliee_ennemie.Allie, Type_unitee.Melee);
        unite2 = new Unite(10, 0, 100, Unite_alliee_ennemie.Ennemie, Type_unitee.Melee);



        // Appeler la fonction CreerCube en passant les positions des unités
        CreerCube(unite1.PositionX, unite1.PositionY);
        CreerCube(unite2.PositionX, unite2.PositionY);
    }

    void Update()
    {
        if (unite1 != null && unite2 != null && cubes != null && cubes.Count >= 2)
        {
            unite1.DeplacerVersUniteDifferente(unite2);
            unite2.DeplacerVersUniteDifferente(unite1);

            // Mettre à jour la position des cubes
            cubes[0].transform.position = new Vector3(unite1.PositionX, unite1.PositionY, 0);
            cubes[1].transform.position = new Vector3(unite2.PositionX, unite2.PositionY, 0);
        }

    }

    void CreerCube(float positionX, float positionY)
    {
        GameObject nouveauCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        nouveauCube.name = "MonCubeamoi,pas a toi " + cubes.Count.ToString();
        cubes.Add(nouveauCube);

        // Placer le cube à la position spécifiée
        nouveauCube.transform.position = new Vector3(positionX, positionY, 0);
    }
}
