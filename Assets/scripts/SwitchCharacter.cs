using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    [HideInInspector] public bool isFlyUnlocked;
    [HideInInspector] public bool isGolemUnlocked;
    [HideInInspector] public bool isHumanUnlocked;
    public DialogueTrigger unlockFly;
    public DialogueTrigger unlockGolem;
    public DialogueTrigger unlockHuman;

    public static SwitchCharacter instance;
    public List<GameObject> charList;
    [HideInInspector] public GameObject activeCharacter;
    public CameraFollow cameraFollow;
    public GameObject selectionWeel;

    private GameObject previusCharacter;

    private void Awake()
    {
        activeCharacter = charList[0];
        if(instance == null)
            instance = this;
    }


    public void ChangeCharacter(int id)
    {
        //Time.timeScale = 1;
        GameManager.instance.isInputEnable = false;
        selectionWeel.SetActive(false);
        previusCharacter = activeCharacter;
        previusCharacter.SetActive(false);
        activeCharacter = charList[id];
        activeCharacter.transform.position = previusCharacter.transform.position;
        activeCharacter.SetActive(true);
        activeCharacter.GetComponent<Animator>().SetTrigger("SwitchOut");
        GameManager.instance.isInputEnable = true;
    }


}
