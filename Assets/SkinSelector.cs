using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelector : MonoBehaviour
{
    private Image image;
    private Color activeColor;
    private Button button;
    void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        activeColor = image.color;
        image.color = Color.white;
    }

    public void GetSkin(bool isPims)
    {
        if (!isPims)
        {
            button.interactable = true;
            image.color = activeColor;
        }
        else
        {
            image.enabled = true;
            button.interactable = true;
        }
    }
}
