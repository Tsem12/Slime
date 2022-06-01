using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBossLaser : StateMachineBehaviour
{
    [SerializeField] private GameObject laser;
    private int maxDamage = 40;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Animator[] anim = animator.GetComponentsInChildren<Animator>();

        anim[1].SetTrigger("Laser");

        laser = animator.transform.Find("Weapons").Find("Laser").Find("StartPoint").gameObject;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (laser.GetComponent<BossAtkHitBox>().isInRange && maxDamage > 0)
        {
            animator.GetComponent<BossWeapons>().LaserAttack(laser.GetComponent<BossAtkHitBox>().player);
            maxDamage -= animator.GetComponent<BossWeapons>().laserDamage;
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Laser", false);
        maxDamage = 40;
    }


}
