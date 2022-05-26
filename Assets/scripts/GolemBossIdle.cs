using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBossIdle : StateMachineBehaviour
{
    [SerializeField] private Animator laser;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<BossFlip>().LookAtPlayer();
        //animator.GetComponent<BossHealth>.

        if (animator.GetComponentInChildren<BossAtkHitBox>().isInRange == true && !animator.GetComponent<BossHealth>().isPhase2)
            animator.SetBool("Melee", true);
        else if(animator.GetComponentInChildren<BossAtkHitBox>().isInRange == true && animator.GetComponent<BossHealth>().isPhase2)
            animator.SetBool("MeleeP2", true);

        else
        {
            if(!animator.GetComponent<BossHealth>().isPhase2)
                animator.SetBool("Distance", true);
            else
                animator.SetBool("Laser", true);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
