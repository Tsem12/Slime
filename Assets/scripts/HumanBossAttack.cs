using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBossAttack : StateMachineBehaviour
{
    public float range = 3f;
    public int hitsCounter = 1;
    public int damage = 10;
    public int kbForce = 1000;

    private Rigidbody2D rb;
    private HumanAttacks humanAttacks;
    private GameObject hitBox;
    private Transform target;
    [SerializeField] private bool endCombo;
    [SerializeField] private bool isSpe;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponentInParent<Rigidbody2D>();
        humanAttacks = animator.GetComponent<HumanAttacks>();
        hitBox = humanAttacks.atkHitBox;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform target = SwitchCharacter.instance.activeCharacter.transform;

        if (humanAttacks.player != null)
        {
            if (humanAttacks.player.tag == "Player" && humanAttacks.canDamage && hitsCounter > 0)
            {
                humanAttacks.canDamage = false;
                humanAttacks.player.GetComponentInParent<PlayerHealth>().TakeDamage(damage);
                humanAttacks.LaunchCoroutine(target.GetComponent<Rigidbody2D>(), target, hitBox, kbForce);
                hitsCounter --;
            }
        }

        if (Vector2.Distance(target.position, rb.position) <= range)
        {
            animator.SetTrigger("Attack");
            animator.ResetTrigger("Idle");
        }
        else
        {
            animator.SetTrigger("Idle");
            animator.ResetTrigger("Attack");

        }

        if (endCombo)
        {
            animator.SetBool("AttackSpé", true);
            animator.ResetTrigger("Attack");
            animator.ResetTrigger("Idle");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Idle");
        hitsCounter = 1;

        if(isSpe)
            animator.SetBool("AttackSpé", false);
    }

    
}
