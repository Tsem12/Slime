using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    [SerializeField] private DialogueTrigger humanDialogue;
    [SerializeField] private DialogueTrigger monsterDialogue;
    public GameObject[] children;
    private bool canTalk;
    private bool isInShop;
    private bool leaveShop = true;

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
            }
            else
            {
                monsterDialogue.TrigerDialogue();
            }
        }

        if (SwitchCharacter.instance.activeCharacter.transform.position.x - transform.position.x < -1.5 || SwitchCharacter.instance.activeCharacter.transform.position.x - transform.position.x > 6 || SwitchCharacter.instance.activeCharacter.transform.position.y - transform.position.y < -1 || SwitchCharacter.instance.activeCharacter.transform.position.y - transform.position.y > 1)
        {
            isInShop = false;
        }
        else
        {
            isInShop = true;
            leaveShop = true;
        }
        if (!isInShop && leaveShop)
        {
            DialogueManager.instance.EndDialogue();
            leaveShop = false;
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
