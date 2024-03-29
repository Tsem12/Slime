using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float force;
    public bool isON;
    public int objectsToDestroy;
    public GameObject elevatorToDesactive;
    public bool isActive;

    private GameObject player;
    [SerializeField] private string direction;
    [SerializeField] private bool needToDestroy;
    [SerializeField] private bool needToActivate;

    private void Update()
    {
        if(isON)
        {
            GetComponent<Animator>().SetBool("IsActive", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("IsActive", false);
        }

        if (needToDestroy && objectsToDestroy == 0)
        {
            isON = true;
            needToDestroy = false;
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        player = SwitchCharacter.instance.activeCharacter;

        if (collision.CompareTag("Player") && isON == true)
        {
            switch (direction)
            {
                case "up":
                    player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, force);
                    break;
                case "down":
                    player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -force);
                    break;
                case "left":
                    player.GetComponent<Rigidbody2D>().velocity = new Vector2(-force, -1f);
                    break;
                case "right":
                    player.GetComponent<Rigidbody2D>().velocity = new Vector2(force, -1f);
                    break;

            }

        }

    }

}
