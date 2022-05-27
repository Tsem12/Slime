using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlip : MonoBehaviour
{
    public SwitchCharacter switchCharacter;
    [HideInInspector] public bool isLeft;

    public void LookAtPlayer()
    {
        if(transform.position.x - switchCharacter.activeCharacter.transform.position.x < 0)
        {
            transform.eulerAngles = new Vector3(0,0,0);
            isLeft = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0,180,0);
            isLeft = true;
        }

    }




}

