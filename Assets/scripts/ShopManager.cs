using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    [SerializeField] private DialogueTriger humanDialogue;
    [SerializeField] private DialogueTriger monsterDialogue;
    public GameObject[] children;
    private bool canTalk;
    private bool isActive;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (canTalk && Input.GetKeyDown(KeyCode.E))
        {
            if (SwitchCharacter.instance.activeCharacter.name == "Player_Human")
            {
                humanDialogue.TrigerDialogue();
                isActive = true;
            }
            else
            {
                monsterDialogue.TrigerDialogue();
                isActive = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player_Human")
        {
            canTalk = true;
        }
        else if (collision.CompareTag("Player"))
        {
            canTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTalk = false;
        }
    }
}