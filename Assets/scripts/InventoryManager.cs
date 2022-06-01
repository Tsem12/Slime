using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public GameObject inventory;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
            inventory.SetActive(!inventory.activeSelf);
    }
}
