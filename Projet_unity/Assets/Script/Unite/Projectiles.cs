using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Projectiles
{
    private static List<Fleche> tab_projectiles= new List<Fleche>();
    public static List<GameObject> tab_gameobject_projectile = new List<GameObject>();

    private static int nb_fleche = 0;
    private static int nb_objectProjectiles = 0;

    public static void CreerPrefabFleche(Team newTeam,float positionX, float positionY, float positionZ){
        if(newTeam == Team.EquipeBleue){
            GameObject flecheObj = Resources.Load<GameObject>("Prefabs/Projectile/ArrowBleu");
            GameObject.Instantiate(flecheObj);
            flecheObj.transform.position = new Vector3(positionX, positionY, positionZ);
            tab_gameobject_projectile.Add(flecheObj);
            nb_objectProjectiles++;
        }
        else {
            GameObject flecheObj = Resources.Load<GameObject>("Prefabs/Projectile/ArrowRouge");
            GameObject.Instantiate(flecheObj);
            flecheObj.transform.position = new Vector3(positionX, positionY, positionZ);
            tab_gameobject_projectile.Add(flecheObj);
            nb_objectProjectiles++;
        }
    }

    public static void envoyerFleche(Team equipe, float posX,float posY,float posZ,Unite autreUnite){
        CreerPrefabFleche(equipe,posX,posY,posZ);
        tab_projectiles.Add(new Fleche(posX,posY,posZ,autreUnite));
        nb_fleche++;
    }

    public static void deplacementObjetProjectile(){
        for(int i = 0;i < nb_objectProjectiles ;i++){
            deplacementFleche(tab_projectiles[i],tab_projectiles[i].autreUnite);
            tab_gameobject_projectile[i].transform.position = new Vector3(tab_projectiles[i].PositionX, tab_projectiles[i].PositionY, tab_projectiles[i].PositionZ);
        }
    }

    public static void deplacementFleche(Fleche fleche,Unite targetUnit){
        // Récupérer la position de la cible
        Vector3 targetPosition = new Vector3(targetUnit.PositionX, targetUnit.PositionY, targetUnit.PositionZ);

        // Calculer le vecteur direction de la cible
        Vector3 direction = (targetPosition - new Vector3(fleche.PositionX, fleche.PositionY, fleche.PositionZ)).normalized;

        // Calculer le déplacement en fonction de la vitesse constante
        Vector3 movement = direction * 5f * Time.deltaTime;

        // Mettre à jour la position de l'unité
        fleche.PositionX += movement.x;
        fleche.PositionY += movement.y;
        fleche.PositionZ += movement.z;
    }
}
