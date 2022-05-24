using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorptionEnemy : MonoBehaviour
{
    public PlayerHealth playerHealth;

    private string collisionTag;
    private GameObject gameManager;
    private bool canAbsorb;
    private SwitchCharacter switchCharacter;

    private void Start()
    {
        collisionTag = this.tag;
        gameManager = GameObject.Find("GameManager");
        switchCharacter = GameObject.Find("CharacterManager").GetComponent<SwitchCharacter>();
    }

    private void Update()
    {
        //Debug.Log(canAbsorb);
        if (Input.GetKeyDown(KeyCode.E) && canAbsorb == true && GetComponentInParent<EnemyPatrol>().isDead == true)
        {
            if(switchCharacter.activeCharacter.name == "Player")
            {
                switch (collisionTag)
                {
                    case "Fly":
                        gameManager.GetComponent<WeelManager>().UnlockCharacter("fly");
                        playerHealth.Heal(10);
                        Debug.Log("La chauve souris est MORTE je l'ai TUER");
                        break;
                    case "Golem":
                        gameManager.GetComponent<WeelManager>().UnlockCharacter("golem");
                        Debug.Log("Le Golem est MORT je l'ai TUER");
                        playerHealth.Heal(20);
                        break;
                    case "Human":
                        gameManager.GetComponent<WeelManager>().UnlockCharacter("human");
                        Debug.Log("L'humain est MORT je l'ai TUER");
                        playerHealth.Heal(30);
                        break;
                }
                Destroy(transform.parent.parent.parent.gameObject);
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
