using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyItems : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI costStr;
    [SerializeField] private bool isPims;
    [SerializeField] private DialogueTriger humanDialogue;
    [SerializeField] private DialogueTriger monsterDialogue;
    private int cost;
    private bool canTalk;
    private bool isActive;
    void Start()
    {
        if(int.TryParse(costStr.text, out cost))
            cost = int.Parse(costStr.text);
        
    }

    void Update()
    {
        if (!isPims)
        {
            if(GameManager.instance.moneyAmount < cost)
                costStr.color = Color.red;
            else
                costStr.color = Color.white;
        }
        else
        {
            if (GameManager.instance.pimsAmount < cost)
                costStr.color = Color.red;
            else
                costStr.color = Color.white;
        }  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player_Human")
        {
            humanDialogue.TrigerDialogue();
        }
        else if (collision.CompareTag("Player"))
        {
            monsterDialogue.TrigerDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //DialogueManager.instance.EndDialogue();
 
        }
    }
}
