using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{


    public void DisableInputOn()
    {
        GameManager.isInputEnable = false;
    }

    public void DisableInputOff()
    {
        GameManager.isInputEnable = true;
    }

    public void Transition()
    {
        this.GetComponent<Animator>().SetTrigger("SwitchOut");
    }

    
}
