using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> charList;
    public GameObject ActiveCharacter;
    public static GameManager instance;

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeCharacter();
        }
           
    }

    void ChangeCharacter()
    {
        charList.Add(ActiveCharacter);
        ActiveCharacter = charList[0];
        ActiveCharacter.SetActive(true);
        charList.Remove(ActiveCharacter);
        ActiveCharacter.transform.position = charList[charList.Count - 1].transform.position;


        foreach (GameObject character in charList)
        {
            character.SetActive(false);

        }


    }


}
