using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> charList;
    public GameObject ActiveCharacter;
    public static GameManager instance;
    public static bool isInputEnable = true;
    public GameObject selectionWeel;



    private void Awake()
    {
        if (instance != null)
            instance = this;
    }
    void Start()
    {
        foreach (GameObject character in charList)
        {
            character.SetActive(false);

        }
        ActiveCharacter = charList[0];
        ActiveCharacter.SetActive(true);
        charList.Remove(ActiveCharacter);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInputEnable == true && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().isGrounded == true)
        {
            ChangeCharacter();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            selectionWeel.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            selectionWeel.SetActive(false);
        }

    }

    void ChangeCharacter()
    {
        StartCoroutine(waiter());
        foreach (GameObject character in charList)
        {
            character.SetActive(false);

        }
    }

    IEnumerator waiter()
    {
        GameManager.isInputEnable = false;
        charList.Add(ActiveCharacter);
        ActiveCharacter = charList[0];
        ActiveCharacter.SetActive(true);
        charList.Remove(ActiveCharacter);
        ActiveCharacter.transform.position = charList[charList.Count - 1].transform.position;
        ActiveCharacter.GetComponent<Animator>().SetTrigger("SwitchOut");
        yield return new WaitForSeconds(0.4f);
        GameManager.isInputEnable = true;

    }




}
