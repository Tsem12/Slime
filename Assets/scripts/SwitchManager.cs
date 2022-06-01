using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    public SwitchCharacter switchCharacter;


    public void DisableInputOn()
    {
        GameManager.instance.isInputEnable = false;
    }

    public void DisableInputOff()
    {
        GameManager.instance.isInputEnable = true;
    }


    
}
