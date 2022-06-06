using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBossIdle : StateMachineBehaviour
{
    public float range = 3f;

    private Rigidbody2D rb;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponentInParent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform player = SwitchCharacter.instance.activeCharacter.transform;
        animator.GetComponent<BossFlip>().LookAtPlayer();

       
        if (!animator.GetBool("AttackSpé"))
        {
            if (Vector2.Distance(player.position, rb.position) <= range)
            {
                animator.SetTrigger("Attack");
            }
            else if (Vector2.Distance(player.position, rb.position) > range)
            {
                animator.SetTrigger("Run");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Run");
    }

}
