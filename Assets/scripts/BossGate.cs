using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGate : MonoBehaviour
{
    public bool isBossDefeated;

    private bool isOpen;
    [SerializeField] private GameObject elevatorObject;
    [SerializeField] private BoxCollider2D bc;
    

    // Update is called once per frame
    void Update()
    {
        if (isBossDefeated)
        {
            if (elevatorObject != null)
                elevatorObject.GetComponent<Elevator>().isON = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            if (isOpen == true)
            {
                bc.isTrigger = true;
                GetComponentInParent<Animator>().SetBool("Open", true);
            }
            else
            {
                bc.isTrigger = false;
                GetComponentInParent<Animator>().SetBool("Open", true);
            }

        

        }

       
    }

}

