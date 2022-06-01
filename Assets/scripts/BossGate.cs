using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGate : MonoBehaviour
{
    public bool isBossDefeated;

    private bool isOpen;
    [SerializeField] private GameObject elevatorObject;
    private BoxCollider2D boxColider;

    private void Start()
    {
        boxColider = GetComponentInParent<BoxCollider2D>();
    }

    void Update()
    {
        if (isBossDefeated)
        {
            if (elevatorObject != null)
                elevatorObject.GetComponent<Elevator>().isON = true;

        }
            GetComponentInParent<Animator>().SetBool("Open", true);
        boxColider.enabled = false;
    }


}

