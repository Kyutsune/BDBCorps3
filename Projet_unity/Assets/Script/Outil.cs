using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Outil
{
    public static int Aleatoire(int min, int max)
    {
        // Génération d'un nombre aléatoire entre min (inclus) et max (exclus)
        return UnityEngine.Random.Range(min, max);
    }

    public static float distanceUnite(Unite courante, Unite autreUnite){
        return Vector3.Distance(new Vector3(courante.PositionX, courante.PositionY, courante.PositionZ), new Vector3(autreUnite.PositionX, autreUnite.PositionY, autreUnite.PositionZ));
    }

    public static float distanceFleche(Fleche fleche, Unite autreUnite){
        return Vector3.Distance(new Vector3(fleche.PositionX, fleche.PositionY, fleche.PositionZ), new Vector3(autreUnite.PositionX, autreUnite.PositionY, autreUnite.PositionZ));
    }
}
