using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitieManager : MonoBehaviour
{
    public static AbilitieManager instance;
    [HideInInspector] public int minMoney = 2;
    [HideInInspector] public int maxMoney = 6;
    public bool resistKb;
    [SerializeField] private GameObject slime;
    private bool canHeal;
    private int functionToReset;

    [Header("Abilities")]
    public Image slimeAblitie;
    public Image HumanMagic;
    public Image HumanBlade;
    public Image GolemDash;
    public Image GolemMissile;

    [HideInInspector] public bool canMagic = true;
    [HideInInspector] public bool canBlade = true;
    [HideInInspector] public bool canDash = true;
    [HideInInspector] public bool canMissile = true;
     
    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Update()
    {
        if (canHeal)
            StartCoroutine(Heal());
        Debug.Log(canMagic);
    }

    public void StartCoolDownCoroutine(Image coolDownUI, float coolDownValue, int coolDownBool)
    {
        StartCoroutine(CoolDown(coolDownUI, coolDownValue, coolDownBool));
    }

    public IEnumerator CoolDown(Image coolDownUI, float coolDownValue, int coolDownBool)
    {
        float coolDown = coolDownValue;
        coolDownUI.fillAmount = 0;
        BoolToFalse(coolDownBool);

        while(coolDown > 0)
        {
            coolDown -= Time.deltaTime;
            coolDownUI.fillAmount =  1 - (coolDown / coolDownValue);
            yield return new WaitForEndOfFrame();
        }

        BoolToTrue(coolDownBool);
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

    public void UpdateUI()
    {
        if(SwitchCharacter.instance.activeCharacter.name == "Player" || SwitchCharacter.instance.activeCharacter.name == "Player_Fly")
        {
            HumanBlade.transform.parent.gameObject.SetActive(false);
            HumanMagic.transform.parent.gameObject.SetActive(false);
            GolemDash.transform.parent.gameObject.SetActive(false);
            GolemMissile.transform.parent.gameObject.SetActive(false);
        }
        else if (SwitchCharacter.instance.activeCharacter.name == "Player_Renf")
        {
            HumanBlade.transform.parent.gameObject.SetActive(false);
            HumanMagic.transform.parent.gameObject.SetActive(false);
            GolemDash.transform.parent.gameObject.SetActive(true);
            GolemMissile.transform.parent.gameObject.SetActive(true);
        }
        else if (SwitchCharacter.instance.activeCharacter.name == "Player_Human")
        {
            HumanBlade.transform.parent.gameObject.SetActive(true);
            HumanMagic.transform.parent.gameObject.SetActive(true);
            GolemDash.transform.parent.gameObject.SetActive(false);
            GolemMissile.transform.parent.gameObject.SetActive(false);
        }
    }

    private void BoolToTrue(int id)
    {
        switch (id)
        {
            case 0:
                canMagic = true;
                break;

            case 1:
                canBlade = true;
                break;

            case 2:
                canDash = true;
                break;

            case 3:
                canMissile = true;
                break;
        }
    }

    private void BoolToFalse(int id)
    {
        switch (id)
        {
            case 0:
                canMagic = false;
                break;

            case 1:
                canBlade = false;
                break;

            case 2:
                canDash = false;
                break;

            case 3:
                canMissile = false;
                break;
        }
    }
}
