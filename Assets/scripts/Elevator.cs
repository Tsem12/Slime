using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private GameObject player;

    public float upForce;



    private void OnTriggerStay2D(Collider2D collision)
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (collision.CompareTag("Player"))
        {
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, upForce));
        }
    }


}
