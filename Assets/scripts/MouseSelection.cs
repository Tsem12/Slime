using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSelection : MonoBehaviour
{
    private void OnMouseOver()
    {
        this.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
        Debug.Log("over");
    }

    private void OnMouseExit()
    {
        this.GetComponent<Image>().color = new Color32(255, 255, 225, 206);
        Debug.Log("exit");
    }
}
