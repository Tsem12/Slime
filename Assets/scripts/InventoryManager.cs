using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [HideInInspector] public bool closeInventory;
    public static InventoryManager instance;
    public GameObject inventory;
    private bool isInventoryOpen;
    

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    private void Start()
    {
        inventory.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && GameManager.instance.isInputEnable || closeInventory || isInventoryOpen && Input.GetKeyDown(KeyCode.I))
        {
            inventory.SetActive(!inventory.activeSelf);

            if(closeInventory)
                inventory.SetActive(false);

            if (inventory.activeSelf)
            {
                GameManager.instance.isInputEnable = false;
                isInventoryOpen = true;
                GameManager.instance.SetMiniMap(false);
            }
            else if (!inventory.activeSelf && isInventoryOpen)
            {
                GameManager.instance.isInputEnable = true;
                isInventoryOpen = false;
                GameManager.instance.SetMiniMap(true);
            }

            if (isInventoryOpen)
                SwitchCharacter.instance.activeCharacter.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            closeInventory = false;
        }
    }
}
