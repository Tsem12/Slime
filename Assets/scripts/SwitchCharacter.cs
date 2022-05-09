using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    public List<GameObject> charList;
    public GameObject activeCharacter;
    private GameObject previusCharacter;
    public CameraFollow cameraFollow;
    public GameObject selectionWeel;

    private void Start()
    {
        activeCharacter = charList[0];
    }


    public void ChangeCharacter(int id)
    {
        GameManager.isInputEnable = false;
        selectionWeel.SetActive(false);
        previusCharacter = activeCharacter;
        previusCharacter.SetActive(false);
        activeCharacter = charList[id];
        activeCharacter.SetActive(true);
        activeCharacter.transform.position = previusCharacter.transform.position;
        activeCharacter.GetComponent<Animator>().SetTrigger("SwitchOut");
        cameraFollow.playerCamera = activeCharacter;
        GameManager.isInputEnable = true;
    }


}
