using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    private SwitchCharacter switchCharacter;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    private float fallDistance;
    private float fallStart;
    private bool isFalling;
    private bool resetFall = true;

    private void Start()
    {
        switchCharacter = FindObjectOfType<SwitchCharacter>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        playerMovement = switchCharacter.activeCharacter.GetComponent<PlayerMovement>();
        if(playerMovement.isGrounded == false && resetFall)
        {
            fallStart = switchCharacter.activeCharacter.transform.position.y;
            resetFall = false;
        }

        if(isFalling && playerMovement.isPlanning)
            fallStart = switchCharacter.activeCharacter.transform.position.y;


        if (fallStart > switchCharacter.activeCharacter.transform.position.y)
        {
            fallDistance = fallStart - switchCharacter.activeCharacter.transform.position.y;
            isFalling = true;
        }

        if(playerMovement.isGrounded == true && !resetFall)
        {
            int fallDamage = 0;

            if(fallDistance < 5 && fallDistance > 3)
                fallDamage = 15;
            else if(fallDistance < 7 && fallDistance > 3)
                fallDamage = 30;
            else if (fallDistance < 11 && fallDistance > 3)
                fallDamage = 50;
            else if (fallDistance > 15)
                fallDamage = 100;

            if(fallDamage > 0)
                playerHealth.TakeDamage(fallDamage);

            fallDistance = 0;
            fallStart = switchCharacter.activeCharacter.transform.position.y;
            isFalling = false;
            resetFall = true;
        }
    }
}
