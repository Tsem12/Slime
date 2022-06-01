using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBlink : StateMachineBehaviour
{
    private int blinkId;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        blinkId = Random.Range(0, 5);
        animator.SetInteger("BlinkID", blinkId);
    }

}
