using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTriger : MonoBehaviour
{
    public Dialogue dialogue;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        dialogue.image = spriteRenderer.sprite;
    }



    public void TrigerDialogue()
    {
        DialogueManager.instance.image.color = spriteRenderer.color;
        DialogueManager.instance.StartDialogue(dialogue, this);
        if (dialogue.isDialogueFix)
        {
            DialogueManager.instance.isDialogueFix = true;
        }
        else
            DialogueManager.instance.isDialogueFix = false;

        if (dialogue.canBuy)
        {
            DialogueManager.instance.buyButton.SetActive(true);
        }
        else
            DialogueManager.instance.buyButton.SetActive(false);

    }

    public void EndDialogue()
    {
        DialogueManager.instance.EndDialogue();
        DialogueManager.instance.isDialogueFix = false;
    }

    public void ChooseAction(int actionId)
    {
        switch (actionId)
        {
            case 0:
                break;

            case 1:
                ActiveShop();
                break;

            case 2:
                DesactiveShop();
                break;
            case 3:
                BossFlee();
                break;
        }
    }

    private void ActiveShop()
    {
        Animator anim = GetComponent<Animator>();
        GameObject[] childrenList = GetComponent<ShopManager>().children;

        if (!DialogueManager.instance.isItemDisplay)
        {
            anim.SetTrigger("Reveal");
            StartCoroutine(Active(childrenList, true));
            DialogueManager.instance.isItemDisplay = true;
        }
    }

    private void DesactiveShop()
    {
        Animator anim = GetComponent<Animator>();
        GameObject[] childrenList = GetComponent<ShopManager>().children;

        if (DialogueManager.instance.isItemDisplay)
        {
            anim.SetTrigger("Reveal");
            StartCoroutine(Active(childrenList, false));
            DialogueManager.instance.isItemDisplay = false;
        }
    }

    private void BossFlee()
    {
        GetComponent<Animator>().SetTrigger("Flee");
    }

    private IEnumerator Active(GameObject[] childrenList, bool active)
    {
        foreach (GameObject item in childrenList)
        {
            item.SetActive(active);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
