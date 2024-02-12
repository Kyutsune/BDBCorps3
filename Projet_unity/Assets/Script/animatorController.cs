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
        if(transform.position!=savedPosition){
            transform.forward = transform.position-savedPosition;
        }
        vitesse = ((transform.position-savedPosition).magnitude)/Time.deltaTime;

        bool isWalking = animator.GetBool("IsWalking");
        bool isRunning = animator.GetBool("IsRunning");
        bool isFighting = animator.GetBool("IsFighting");

        if(vitesse > 0 && vitesse < 1.0f && !isWalking){
            animator.SetBool("IsWalking",true);
            animator.SetBool("IsRunning",false);
        }
        else if(vitesse > 1.0f && !isRunning){
            animator.SetBool("IsWalking",false);
            animator.SetBool("IsRunning",true);
        }
        else if(vitesse == 0.0f && !isFighting){
            animator.SetBool("IsFighting",true);
            animator.SetBool("IsWalking",false);
            animator.SetBool("IsRunning",false);
        }
        
        savedPosition = transform.position;
    }
}
