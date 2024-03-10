using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorController : MonoBehaviour
{
    Animator animator;
    Vector3 savedPosition;
    float vitesse;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void seTourner(Unite Courante, Unite Visee){
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

    public void setRunning(bool run){
        animator.SetBool("IsWalking",false);
        animator.SetBool("IsRunning",true);
        animator.SetBool("IsFighting",false);
    }

    public void setFighting(bool fight)
    {
        animator.SetBool("IsFighting",fight);
        animator.SetBool("IsRunning",false);
        animator.SetBool("IsWalking",false);
    }

    public void setWalking(bool walk){
        animator.SetBool("IsWalking",true);
        animator.SetBool("IsRunning",false);
        animator.SetBool("IsFighting",false);
    }

    public void Mort(){
        animator.SetBool("IsRunning",false);
        animator.SetBool("IsFighting",false);
        animator.SetBool("IsWalking",false);
        animator.SetBool("Dead",true);
    }

    public void Victoire(){
        animator.SetBool("IsRunning",false);
        animator.SetBool("IsFighting",false);
        animator.SetBool("IsWalking",false);
        animator.SetBool("Won",true);
    }
}
