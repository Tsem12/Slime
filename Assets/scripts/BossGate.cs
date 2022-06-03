using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGate : MonoBehaviour
{
    public bool isBossDefeated;

    private bool isOpen;
    [SerializeField] private GameObject elevatorObject;
    [SerializeField] private BoxCollider2D gateToOpen;


    void Update()
    {
        if (isBossDefeated)
        {
            if (elevatorObject != null)
                elevatorObject.GetComponent<Elevator>().isON = true;

        gateToOpen.enabled = false;
        gateToOpen.gameObject.GetComponent<Animator>().SetBool("Open", true);
        }
    }


}

