using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Classe jeu qui va être celle qui va contenir notre boucle de jeu,essayons de conserver un maximum un systeme de MVC s'il vous plait

*/
public class Jeu : MonoBehaviour
{
    private List<GameObject> unites = new List<GameObject>();
    private List<Unite> alliee = new List<Unite>();
    private List<Unite> ennemis = new List<Unite>();
    private Unite unite1;
    private Unite unite2;
    private int nb_alliee;
    private int nb_ennemis;
    public GameObject prefab;
    Animator animator;


    private Canvas canva_pour_texte_pv;
    private List<AffichageDesPVs> liste_texte_pv= new List<AffichageDesPVs>();
    


    void Start()
    {
        GameObject canvasObj = new GameObject("Canvas");
        canva_pour_texte_pv = canvasObj.AddComponent<Canvas>();
        canva_pour_texte_pv.renderMode = RenderMode.ScreenSpaceOverlay;
        

        //alliee[0] = new Unite(0, 0, 0, 1000, 2, 1,Team.Equipe1, Type_unitee.Melee, canva_pour_texte_pv);
        nb_alliee=1;
        nb_ennemis=1;

    
        animator = GetComponent<Animator>();


        for(int i=0;i<nb_alliee;i++) // Parcours du nombre d'ennemis + alliées
        {
            
            alliee.Add(new Unite(0, 0, 0, 1000, 2, 1, Team.Equipe1, Type_unitee.Melee, canva_pour_texte_pv, true));
        
            // Créer le texte PV et l'ajouter à la liste
            // AffichageDesPVs textePVUnite=GetComponent<AffichageDesPVs>();
            // textePVUnite.CreerTextePV(canva_pour_texte_pv, alliee[i].PositionX, alliee[i].PositionY+40, alliee[i].PositionZ);
            // liste_texte_pv.Add(textePVUnite);

            CreerUnite(alliee[i].PositionX, alliee[i].PositionY, alliee[i].PositionZ);
        }
        for(int i=0;i<nb_ennemis;i++) // Parcours du nombre d'ennemis + alliées
        {
            ennemis.Add(new Unite(10, 0, 0, 1000, 1, 2,Team.Equipe2, Type_unitee.Melee, canva_pour_texte_pv, true));
            CreerUnite(ennemis[i].PositionX, ennemis[i].PositionY, ennemis[i].PositionZ);
        }
    }

    void Update()
    {
        
        for(int i=0;i<nb_alliee;i++) // Parcours du nombre d'ennemis + alliées
        {
            // for(int j=0;j<nb_alliee;j++)
            // {
            //     liste_texte_pv[j].MettreAJourTextePV(ennemis[0].Pv,alliee[0].PositionX,alliee[0].PositionY,alliee[0].PositionZ);
            // }

            alliee[i].DeplacerVersUniteDifferente(ennemis[0]);
            // float positionX_texte = alliee[i].PositionX ; // Exemple de position X (vous pouvez ajuster selon vos besoins)
            // float positionY_texte = alliee[i].PositionY+40; // Exemple de position Y (vous pouvez ajuster selon vos besoins)
            // Debug.Log(positionX_texte);

            //liste_texte_pv[i].MettreAJourTextePV(unite2.Pv,positionX_texte,positionY_texte,0);

            // Mettre à jour la position des unités
            if(unites[i+nb_ennemis] != null && ennemis[0].ParmiNous)
            {
                Debug.Log("on ");
                unites[i].transform.position = new Vector3(alliee[i].PositionX, alliee[i].PositionY, alliee[i].PositionZ);

                    if(ennemis[0].Pv <= 0)
                    {
                        Debug.Log("L'unite 2 est morte");

                        Destroy(unites[1]);
                        unites.RemoveAt(1);
                        animator.SetBool("Won",true);
                        ennemis[0].ParmiNous=false;
                        nb_ennemis--;
                    }
                    else
                    {
                        alliee[i].Attaquer(ennemis[0]);
                        //Debug.Log("PV de unite2 après attaque : " + unite2.Pv);   
                    } 
            }         
            
        }

        for(int i=0;i<nb_ennemis;i++) // Parcours du nombre d'ennemis + alliées
        {
            ennemis[i].DeplacerVersUniteDifferente(alliee[0]);
            // for(int j=0;j<nb_alliee;j++)
            // {
            //     liste_texte_pv[j].MettreAJourTextePV(ennemis[0].Pv,alliee[0].PositionX,alliee[0].PositionY,alliee[0].PositionZ);
            // }
            if(unites[i+nb_alliee] != null)
            {   
                unites[i+nb_alliee].transform.position = new Vector3(ennemis[i].PositionX, ennemis[i].PositionY, ennemis[i].PositionZ);
            }   
            
        }
            

        
        // Mettre à jour la position des unités
        // if(unites[0] != null && unites[1] != null)
        // {
        //     unites[0].transform.position = new Vector3(alliee[0].PositionX, alliee[0].PositionY, alliee[0].PositionZ);
        //     unites[1].transform.position = new Vector3(ennemis[0].PositionX, ennemis[0].PositionY, ennemis[0].PositionZ);

        //     if(ennemis[0].Pv <= 0)
        //     {
        //         Debug.Log("L'unite 2 est morte");

        //         Destroy(unites[1]);
        //         unites.RemoveAt(1);
        //         animator.SetBool("Won",true);
        //     }
        //     else
        //     {
        //         alliee[0].Attaquer(ennemis[0]);
        //         //Debug.Log("PV de unite2 après attaque : " + unite2.Pv);
        //     }
        // }
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
