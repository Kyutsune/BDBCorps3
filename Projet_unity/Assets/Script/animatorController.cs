using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorController : MonoBehaviour
{
    Animator animator;
    Vector3 savedPosition;
    float vitesse;
    

    public void seTourner(Unite Courante, Unite Visee, Animator animator){
        if(animator.GetBool("IsWalking")==true || animator.GetBool("IsRunning")==true){
            if(transform.position!=savedPosition){
                transform.forward = transform.position-savedPosition;
            }
            vitesse = ((transform.position-savedPosition).magnitude)/Time.deltaTime;
            
            savedPosition = transform.position;
        }
        if(animator.GetBool("IsFighting")){
                // Récupérer la position de la cible
                Vector3 targetPosition = new Vector3(Visee.PositionX, Visee.PositionY, Visee.PositionZ);

                // Calculer le vecteur direction de la cible
                Vector3 direction = (targetPosition - new Vector3(Courante.PositionX, Courante.PositionY, Courante.PositionZ)).normalized;

                transform.forward = direction;
        }
    }

    public void setRunning(bool run, Animator animator)
    {
        animator.SetBool("IsWalking",false);
        animator.SetBool("IsRunning",run);
        animator.SetBool("IsFighting",false);
    }

    public void setFighting(bool fight, Animator animator)
    {
        animator.SetBool("IsFighting",fight);
        animator.SetBool("IsRunning",false);
        animator.SetBool("IsWalking",false);
    }

    public void setWalking(bool walk, Animator animator){
        animator.SetBool("IsWalking",walk);
        animator.SetBool("IsRunning",false);
        animator.SetBool("IsFighting",false);
    }

    public void Mort(bool mort, Animator animator){
        animator.SetBool("IsRunning",false);
        animator.SetBool("IsFighting",false);
        animator.SetBool("IsWalking",false);
        animator.SetBool("Dead",mort);
    }

    public void Victoire(bool victoire, Animator animator){
        animator.SetBool("IsRunning",false);
        animator.SetBool("IsFighting",false);
        animator.SetBool("IsWalking",false);
        animator.SetBool("Won",victoire);
    }
}
