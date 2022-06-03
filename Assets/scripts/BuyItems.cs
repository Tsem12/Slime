using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyItems : MonoBehaviour
{
    public int itemID;

    [SerializeField] private TextMeshProUGUI costStr;
    [SerializeField] private bool isPims;
    [SerializeField] private DialogueTriger humanDialogue;
    [SerializeField] private DialogueTriger monsterDialogue;
    [SerializeField] private DialogueTriger alreadyBuy;
    [SerializeField] private DialogueTriger notEnoughMoney;
    [SerializeField] private SkinSelector skin;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject sold;
    private int cost;
    private bool canTalk;
    private bool isActive;
    private bool isBuy;
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

    public void BuyItem()
    {
        if (!isPims)
        {
            if(cost <= GameManager.instance.moneyAmount && !isBuy)
            {
                canvas.gameObject.SetActive(false);
                sold.SetActive(true);
                isBuy = true;
                GameManager.instance.moneyAmount -= cost;
                skin.GetSkin(false);
            }
            else if(isBuy)
                alreadyBuy.TrigerDialogue();
            else
                notEnoughMoney.TrigerDialogue();
        }
        else
        {
            if (cost <= GameManager.instance.pimsAmount && !isBuy)
            {
                canvas.gameObject.SetActive(false);
                sold.SetActive(true);
                isBuy = true;
                GameManager.instance.pimsAmount -= cost;
                skin.GetSkin(true);
            }
            else if (isBuy)
                alreadyBuy.TrigerDialogue();
            else
                notEnoughMoney.TrigerDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.name == "Player_Human")
        {
            humanDialogue.TrigerDialogue();
        }
        else if (collision.name == "Player_Human" && isBuy)
        {
            alreadyBuy.TrigerDialogue();
        }
        else if (collision.CompareTag("Player"))
        {
            monsterDialogue.TrigerDialogue();
        }
    }

}
