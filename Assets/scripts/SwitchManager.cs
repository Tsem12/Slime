using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    public SwitchCharacter switchCharacter;


    public void DisableInputOn()
    {
        GameManager.isInputEnable = false;
    }

    public void DisableInputOff()
    {
        GameManager.isInputEnable = true;
    }


    
}
