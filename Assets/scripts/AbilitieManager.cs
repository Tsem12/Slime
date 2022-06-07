using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitieManager : MonoBehaviour
{
    public static AbilitieManager instance;
    [HideInInspector] public int minMoney = 2;
    [HideInInspector] public int maxMoney = 6;
    public bool resistKb;
    [SerializeField] private GameObject slime;
    private bool canHeal;
    private int functionToReset;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Update()
    {
        if (canHeal)
            StartCoroutine(Heal());
    }

    public void Reset(int functionToReset)
    {
        switch (functionToReset)
        {
            case 0:
                break;

            case 1:
                slime.GetComponent<PlayerMovement>().moveSpeed -= 50;
                break;

            case 2:
                slime.GetComponent<PlayerAttack>().playerDamage -= 10;
                break;

            case 3:
                canHeal = false;
                StopAllCoroutines();
                break;

            case 4:
                resistKb = false;
                break;

        }
        
    }

    public void BaseSlime()
    {
        Reset(functionToReset);
        functionToReset = 0;
    }

    public void SlimeOfSwiftness()
    {
        Reset(functionToReset);
        slime.GetComponent<PlayerMovement>().moveSpeed += 50;
        functionToReset = 1;
    }

    public void SlimeOfStregth()
    {
        Reset(functionToReset);
        slime.GetComponent<PlayerAttack>().playerDamage += 10;
        functionToReset = 2;
    }

    public void SlimeOfHeal()
    {
        Reset(functionToReset);
        canHeal = true;
        functionToReset = 3;
    }

    public void SlimeOfHardness()
    {
        Reset(functionToReset);
        resistKb = true;
        functionToReset = 4;
    }

    private IEnumerator Heal()
    {
        canHeal = false;
        yield return new WaitForSeconds(5);

        if(SwitchCharacter.instance.activeCharacter.name == "Player")
            slime.GetComponentInParent<PlayerHealth>().Heal(10);

        canHeal = true;
    }
}
