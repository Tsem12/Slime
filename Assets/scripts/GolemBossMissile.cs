using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBossMissile : StateMachineBehaviour
{
    private bool cantakeDamage;
    private GameObject newMissile;
    private GameObject missileStartPoint;
    private Vector3 player;
    private Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cantakeDamage = true;
        player = animator.GetComponent<BossWeapons>().target;
        newMissile = animator.GetComponent<BossWeapons>().missile;
        missileStartPoint = animator.transform.Find("Weapons").Find("MissileFolder").Find("StartPoint").gameObject;

        newMissile.transform.position = missileStartPoint.transform.position;
        newMissile.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        newMissile.SetActive(false);

        rb = newMissile.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if(newMissile.GetComponent<BossAtkHitBox>().isInRange && cantakeDamage)
        {
            animator.GetComponent<BossWeapons>().DoDamage(30);
            cantakeDamage = false;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Distance", false);
    }


}
