using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorptionEnemy : MonoBehaviour
{
    public PlayerHealth playerHealth;
    [HideInInspector] public bool isFlyUnlocked;
    [HideInInspector] public bool isGolemUnlocked;
    [HideInInspector] public bool isHumanUnlocked;

    private DialogueTriger unlockFly;
    private DialogueTriger unlockGolem;
    private DialogueTriger unlockHuman;
    private string collisionTag;
    private GameObject gameManager;
    private bool canAbsorb;
    private SwitchCharacter switchCharacter;
    [SerializeField] private bool isDead;

    private void Start()
    {
        collisionTag = this.tag;
        gameManager = GameObject.Find("GameManager");
        switchCharacter = GameObject.Find("CharacterManager").GetComponent<SwitchCharacter>();
    }

    private void Update()
    {
        //Debug.Log(canAbsorb);
        if (Input.GetKeyDown(KeyCode.E) && canAbsorb == true && GetComponentInParent<EnemyPatrol>().isDead == true || Input.GetKeyDown(KeyCode.E) && canAbsorb == true && isDead)
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
                        break;
                    case "Golem":
                        if (!SwitchCharacter.instance.isGolemUnlocked)
                        {
                            gameManager.GetComponent<WeelManager>().UnlockCharacter("golem");
                            SwitchCharacter.instance.unlockGolem.TrigerDialogue();
                            SwitchCharacter.instance.isGolemUnlocked = true;
                        }
                        playerHealth.Heal(20);
                        break;
                    case "Human":
                        if (!SwitchCharacter.instance.isHumanUnlocked)
                        {
                            gameManager.GetComponent<WeelManager>().UnlockCharacter("human");
                            SwitchCharacter.instance.unlockHuman.TrigerDialogue();
                            SwitchCharacter.instance.isHumanUnlocked = true;
                        }
                        playerHealth.Heal(30);
                        break;
                }
                Destroy(transform.parent.parent.parent.gameObject);
                switchCharacter.activeCharacter.GetComponent<Animator>().SetTrigger("Absorb");
                GameManager.instance.moneyAmount += Random.Range(AbilitieManager.instance.minMoney, AbilitieManager.instance.maxMoney);
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
