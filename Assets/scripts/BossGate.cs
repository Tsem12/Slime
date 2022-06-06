using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGate : MonoBehaviour
{
    public bool isBossDefeated;

    private bool isOpen;
    private Animator gateAnimator;
    [SerializeField] private GameObject elevatorObject;
    [SerializeField] private BoxCollider2D gateToOpen;
    private bool canOpen = true;

    private void Start()
    {
        gateAnimator = gateToOpen.GetComponent<Animator>();
    }

    void Update()
    {
        if (isBossDefeated && canOpen)
        {
            if (elevatorObject != null)
                elevatorObject.GetComponent<Elevator>().isON = true;

            gateToOpen.isTrigger = !gateToOpen.isTrigger;
            gateAnimator.SetBool("Open", !gateAnimator.GetBool("Open"));
            canOpen = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gateToOpen.isTrigger = !gateToOpen.isTrigger;
            gateAnimator.SetBool("Open", !gateAnimator.GetBool("Open"));
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void ResetDoor()
    {
        gateToOpen.isTrigger = true;
        gateAnimator.SetBool("Open", false);
        GetComponent<BoxCollider2D>().enabled = true;
        canOpen = true;
    }


}

