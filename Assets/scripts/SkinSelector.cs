using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelector : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Button button;
    private Color activeColor;
    private bool isPlaying;
    [SerializeField] private bool isBaseSlime;
    void Start()
    {
        activeColor = image.color;
        if(isBaseSlime)
            button.interactable = true;
        else
            image.color = Color.white;
    }
    /// <summary>
    /// Unlock Skin when buy or collect
    /// </summary>
    /// <param name="isPims"></param>
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
    /// <summary>
    /// Change Skin function
    /// </summary>
    public void SelectSkin()
    {
        AbilitieManager.instance.slimeAblitie.color = activeColor;
        SwitchCharacter.instance.ChangeCharacter(0);
        InventoryManager.instance.closeInventory = true;
        SwitchCharacter.instance.activeCharacter.GetComponent<SpriteRenderer>().color = activeColor;
    }

}
