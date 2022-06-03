using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitieManager : MonoBehaviour
{
    public static AbilitieManager instance;
    [HideInInspector] public int minMoney = 2;
    [HideInInspector] public int maxMoney = 6;
    [SerializeField] private GameObject slime;
    private bool canHeal;

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

    public void Reset()
    {
        slime.GetComponent<PlayerMovement>().moveSpeed -= 50;
        slime.GetComponent<PlayerAttack>().playerDamage -= 10;
        StopAllCoroutines();
        canHeal = false;
        
    }

    public void SlimeOfSwiftness()
    {
        slime.GetComponent<PlayerMovement>().moveSpeed += 50;
    }

    public void SlimeOfStregth()
    {
        slime.GetComponent<PlayerAttack>().playerDamage += 10;
    }

    public void SlimeOfHeal()
    {
        canHeal = true;
    }

    private IEnumerator Heal()
    {
        canHeal = false;
        yield return new WaitForSeconds(5);
        slime.GetComponentInParent<PlayerHealth>().Heal(10);
        canHeal = true;
    }
}
