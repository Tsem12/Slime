using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    public SwitchCharacter switchCharacter;


    public void DisableInputOn()
    {
        GameManager.instance.isInputEnable = false;
        SwitchCharacter.instance.activeCharacter.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, SwitchCharacter.instance.activeCharacter.GetComponent<Rigidbody2D>().velocity.y);
    }

    public void DisableInputOff()
    {
        GameManager.instance.isInputEnable = true;
    }


    
}
