using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool isInputEnable = true;
    public GameObject selectionWeel;
    public PlayerAttack playerAttack;
    public SwitchCharacter switchCharacter;



    private void Awake()
    {
        if (instance != null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            selectionWeel.SetActive(true);
            switchCharacter.activeCharacter.GetComponent<PlayerAttack>().canAttack = false;
            Time.timeScale = 0.1f;
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            selectionWeel.SetActive(false);
            switchCharacter.activeCharacter.GetComponent<PlayerAttack>().canAttack = true;
            Time.timeScale = 1f;
        }

    }




}
