using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorptionEnemy : MonoBehaviour
{
    public bool isDead;
    public PlayerHealth playerHealth;
    [HideInInspector] public bool isFlyUnlocked;
    [HideInInspector] public bool isGolemUnlocked;
    [HideInInspector] public bool isHumanUnlocked;

    private DialogueTrigger unlockFly;
    private DialogueTrigger unlockGolem;
    private DialogueTrigger unlockHuman;
    private string collisionTag;
    private int bossMoney;
    private GameObject gameManager;
    private bool canAbsorb;
    private SwitchCharacter switchCharacter;
    [SerializeField] private GameObject golemPlayer;
    [SerializeField] private GameObject humanPlayer;

    private void Start()
    {
        collisionTag = this.tag;
        gameManager = GameObject.Find("GameManager");
        switchCharacter = GameObject.Find("CharacterManager").GetComponent<SwitchCharacter>();
    }

    private void Update()
    {
        //Debug.Log(canAbsorb);
        if (Input.GetKeyDown(KeyCode.E) && canAbsorb == true && isDead)
        {
            if(switchCharacter.activeCharacter.name == "Player")
            {
                switch (collisionTag)
                {
                    case "Fly":
                        if (!SwitchCharacter.instance.isFlyUnlocked)
                        {
                            gameManager.GetComponent<WeelManager>().UnlockCharacter("fly");
                            SwitchCharacter.instance.unlockFly.TrigerDialogue();
                            SwitchCharacter.instance.isFlyUnlocked = true;
                        }
                        playerHealth.Heal(10);
                        Destroy(transform.parent.parent.parent.gameObject);
                        break;
                    case "Golem":
                        if (!SwitchCharacter.instance.isGolemUnlocked)
                        {
                            gameManager.GetComponent<WeelManager>().UnlockCharacter("golem");
                            SwitchCharacter.instance.unlockGolem.TrigerDialogue();
                            SwitchCharacter.instance.isGolemUnlocked = true;
                        }
                        playerHealth.Heal(20);
                        Destroy(transform.parent.parent.parent.gameObject);
                        break;
                    case "Human":
                        if (!SwitchCharacter.instance.isHumanUnlocked)
                        {
                            gameManager.GetComponent<WeelManager>().UnlockCharacter("human");
                            SwitchCharacter.instance.unlockHuman.TrigerDialogue();
                            SwitchCharacter.instance.isHumanUnlocked = true;
                        }
                        playerHealth.Heal(30);
                        Destroy(transform.parent.parent.parent.gameObject);
                        break;
                    case "Boss":
                        if (this.name == "GraphicsGolemBoss")
                        {
                            GetComponent<DialogueTrigger>().TrigerDialogue();
                            golemPlayer.GetComponent<MissileLauncher>().missileEnabled = true;
                            Destroy(transform.parent.gameObject);
                            bossMoney = 20;
                        }
                        if (this.name == "GraphicsHumanBoss")
                        {
                            GetComponent<DialogueTrigger>().TrigerDialogue();
                            humanPlayer.GetComponent<HumanMagic>().canMagic = true;
                            Destroy(transform.parent.gameObject);
                            bossMoney = 20;
                        }
                        playerHealth.Heal(100);
                        break;

                }
                switchCharacter.activeCharacter.GetComponent<Animator>().SetTrigger("Absorb");
                GameManager.instance.moneyAmount += Random.Range(2 + AbilitieManager.instance.minMoney, 6 + AbilitieManager.instance.maxMoney) + bossMoney;
            }
            else
            {
                switchCharacter.activeCharacter.GetComponent<PlayerMovement>().indicator.SetTrigger("NeedSlime");
 
            }


        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            canAbsorb = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            canAbsorb = false;

    }
}
