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
    private List<Unite> ennemie = new List<Unite>();
    private Unite unite1;
    private Unite unite2;
    private int nb_alliee;
    private int nb_ennemie;
    public GameObject prefab;
    Animator animator;


    private Canvas canva_pour_texte_pv;
    private List<AffichageDesPVs> liste_texte_pv= new List<AffichageDesPVs>();
    


    void Start()
    {
        GameObject canvasObj = new GameObject("Canvas");
        canva_pour_texte_pv = canvasObj.AddComponent<Canvas>();
        canva_pour_texte_pv.renderMode = RenderMode.ScreenSpaceOverlay;
        

        //unite1 = new Unite(0, 0, 0, 1000, 2, 1,Team.Equipe1, Type_unitee.Melee, canva_pour_texte_pv);
        nb_alliee=1;
        nb_ennemie=1;

        

        animator = GetComponent<Animator>();


        for(int i=0;i<nb_alliee+nb_ennemie;i++) // Parcours du nombre d'ennemis + alliées
        {
            for(int j=0;j<nb_alliee;j++) //Parcours du nombre d'alliées
            {
                alliee.Add(new Unite(0, 0, 0, 1000, 2, 1, Team.Equipe1, Type_unitee.Melee, canva_pour_texte_pv));

                float positionX_texte_pv = alliee[j].PositionX; // Exemple de position X (vous pouvez ajuster selon vos besoins)
                float positionY_texte_pv = alliee[j].PositionY+40; // Exemple de position Y (vous pouvez ajuster selon vos besoins)
                float positionZ_texte_pv = alliee[j].PositionZ; // Exemple de position Z (vous pouvez ajuster selon vos besoins)
            
                // Créer le texte PV et l'ajouter à la liste
                AffichageDesPVs textePVUnite=GetComponent<AffichageDesPVs>();
                textePVUnite.CreerTextePV(canva_pour_texte_pv, positionX_texte_pv, positionY_texte_pv, positionZ_texte_pv);
                liste_texte_pv.Add(textePVUnite);

                CreerUnite(alliee[j].PositionX, alliee[j].PositionY, alliee[j].PositionZ);
                
            } 
            for(int t=0;t<nb_alliee;t++) //parcours du nombre d'ennemies
            {
                ennemie.Add(new Unite(10, 0, 0, 1000, 1, 2,Team.Equipe2, Type_unitee.Melee, canva_pour_texte_pv));
                CreerUnite(ennemie[t].PositionX, ennemie[t].PositionY, ennemie[t].PositionZ);
            }     
            
        }
        

        
    }

    void Update()
    {
        
        for(int i=0;i<nb_alliee+nb_ennemie;i++) // Parcours du nombre d'ennemis + alliées
        {

            for(int j=0;j<nb_alliee;j++) //Parcours du nombre d'alliées
            {
                alliee[j].DeplacerVersUniteDifferente(ennemie[0], canva_pour_texte_pv);
                float positionX_texte = alliee[j].PositionX ; // Exemple de position X (vous pouvez ajuster selon vos besoins)
                float positionY_texte = alliee[j].PositionY+40; // Exemple de position Y (vous pouvez ajuster selon vos besoins)
                // Debug.Log(positionX_texte);

                //liste_texte_pv[i].MettreAJourTextePV(unite2.Pv,positionX_texte,positionY_texte,0);

                // Mettre à jour la position des unités
                if(unites[0] != null && unites[1] != null)
                {
                    unites[0].transform.position = new Vector3(alliee[j].PositionX, alliee[j].PositionY, alliee[j].PositionZ);
                    unites[1].transform.position = new Vector3(ennemie[0].PositionX, ennemie[0].PositionY, ennemie[0].PositionZ);

                    if(ennemie[0].Pv <= 0)
                    {
                        Debug.Log("L'unite 2 est morte");

                        Destroy(unites[1]);
                        unites.RemoveAt(1);
                        animator.SetBool("Won",true);
                    }
                    else
                    {
                        alliee[j].Attaquer(ennemie[0]);
                        //Debug.Log("PV de unite2 après attaque : " + unite2.Pv);
                    }
                }
            }               
            for(int t=0;t<nb_ennemie;t++)//Parcours du nombre d'alliées
            {
                ennemie[t].DeplacerVersUniteDifferente(alliee[0], canva_pour_texte_pv);
            
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
