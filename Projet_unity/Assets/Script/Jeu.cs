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
    private int nb_unite;


    public GameObject prefab;
    Animator animator;


    private Canvas canva_pour_texte_pv;
    public List<AffichageDesPVs> liste_texte_pv= new List<AffichageDesPVs>();
    


    void Start()
    {
        GameObject canvasObj = new GameObject("Canvas");
        canva_pour_texte_pv = canvasObj.AddComponent<Canvas>();
        canva_pour_texte_pv.renderMode = RenderMode.ScreenSpaceOverlay;
        

        unite1 = new Unite(0, 0, 0, 1000, 2, 1,Team.Equipe1, Type_unitee.Melee, canva_pour_texte_pv);
        unite2 = new Unite(10, 0, 0, 1000, 1, 2,Team.Equipe2, Type_unitee.Melee, canva_pour_texte_pv);
        nb_unite=2;

    
        animator = GetComponent<Animator>();


        //Ici on va créer le texte des pvs pour chaque unité
        for (int i = 0; i < nb_unite; i++)
        {
            // Créer un nouveau textePVUnite à chaque itération
            AffichageDesPVs textePVUnite = GetComponent<AffichageDesPVs>();

            // Créer le texte PV et l'ajouter à la liste
            textePVUnite.CreerTextePV(canva_pour_texte_pv, unite1.PositionX, unite1.PositionY + 40, unite1.PositionZ);
            liste_texte_pv.Add(textePVUnite);
        }
    
        CreerUnite(unite1.PositionX, unite1.PositionY, unite1.PositionZ);
        CreerUnite(unite2.PositionX, unite2.PositionY, unite2.PositionZ);
    }

    void Update()
    {
        unite1.DeplacerVersUniteDifferente(unite2);
        unite2.DeplacerVersUniteDifferente(unite1);
        for(int i=0;i<nb_unite;i++)
        {
            liste_texte_pv[i].MettreAJourTextePV(unite2.Pv,unite1.PositionX,unite1.PositionY+40,unite1.PositionZ);
        }

        
        // Mettre à jour la position des unités
        if(unites[0] != null && unites[1] != null)
        {
            unites[0].transform.position = new Vector3(unite1.PositionX, unite1.PositionY, unite1.PositionZ);
            unites[1].transform.position = new Vector3(unite2.PositionX, unite2.PositionY, unite1.PositionZ);

            if(unite2.Pv <= 0)
            {
                Debug.Log("L'unite 2 est morte");

                Destroy(unites[1]);
                unites.RemoveAt(1);
                animator.SetBool("Won",true);
            }
            else
            {
                unite1.Attaquer(unite2);
                //Debug.Log("PV de unite2 après attaque : " + unite2.Pv);
            }
        }
    }

    void CreerUnite(float positionX, float positionY, float positionZ)
    {
        GameObject newUnite = Instantiate(prefab);
        newUnite.name = "Unite" + unites.Count.ToString();

        // Placer le cube à la position spécifiée
        newUnite.transform.position = new Vector3(positionX, positionY, positionZ);

        unites.Add(newUnite);
    }
}
