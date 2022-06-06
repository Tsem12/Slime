using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMagic : MonoBehaviour
{
    private Animator animator;
    public bool canMagic;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && GameManager.instance.isActiveAndEnabled && canMagic)
            animator.SetTrigger("Magic");
    }

    public void ActiveElement()
    {
        RaycastHit2D[] hits;
        hits = Physics2D.CircleCastAll(transform.position, 1, Vector2.right, 1);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.tag == "Activable")
            {

                Elevator elevator = hit.collider.GetComponent<Elevator>();
                SpriteRenderer spriteRenderer = hit.collider.GetComponent<SpriteRenderer>();
                if (!elevator.isActive)
                {
                    spriteRenderer.color = Color.white;
                    elevator.isON = true;
                    if(elevator.elevatorToDesactive != null)
                    {
                        elevator.elevatorToDesactive.GetComponent<SpriteRenderer>().color = Color.gray;
                        elevator.elevatorToDesactive.GetComponent<Elevator>().isON = false;
                    }
                }

            }
        }
    }
}
