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
    public GameObject prefab;
    private Affichagedespvs pvs_perso;

    void Start()
    {
        unite1 = new Unite(0, 0, 1000, 1, 1,Team.Equipe1, Type_unitee.Melee);
        unite2 = new Unite(10, 0, 1000, 1, 2,Team.Equipe2, Type_unitee.Melee);
        nouveauTextePV = GetComponent<Affichagedespvs>();
        nouveauTextePV.creerTextePv(transform,10,10);

        // Appeler la fonction CreerCube en passant les positions des unités
        CreerUnite(unite1.PositionX, unite1.PositionY);
        CreerUnite(unite2.PositionX, unite2.PositionY);
    }

    void Update()
    {
        nouveauTextePV.MettreAJourTextePV(80);
        if (unite1 != null && unite2 != null && unites != null && unites.Count >= 2)
        {
            unite1.DeplacerVersUniteDifferente(unite2);
            unite2.DeplacerVersUniteDifferente(unite1);
            
            // Mettre à jour la position des unités
            if(unites[0] != null && unites[1] != null)
            {
                unites[0].transform.position = new Vector3(unite1.PositionX, unite1.PositionY, 0);
                unites[1].transform.position = new Vector3(unite2.PositionX, unite2.PositionY, 0);

                if(unite2.Pv <= 0)
                {
                    Debug.Log("L'unite 2 est morte");

                    Destroy(unites[1]);
                    unites.RemoveAt(1);

                }
                else
                {
                    unite1.Attaquer(unite2);
                    pvs_perso.MettreAJourTextePV(unite2.Pv);
                }
            }
        }

    }

    void CreerUnite(float positionX, float positionY)
    {
        GameObject newUnite = Instantiate(prefab);
        newUnite.name = "Unite" + unites.Count.ToString();

        // Placer le cube à la position spécifiée
        newUnite.transform.position = new Vector3(positionX, positionY, 0);

        unites.Add(newUnite);
    }
}