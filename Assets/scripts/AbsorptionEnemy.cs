using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorptionEnemy : MonoBehaviour
{
    private string collisionTag;
    private GameObject gameManager;

    private void Start()
    {
        collisionTag = this.tag;
        gameManager = GameObject.Find("GameManager");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            switch (collisionTag)
            {
                case "Fly":
                    gameManager.GetComponent<WeelManager>().UnlockCharacter("fly");
                    Debug.Log("La chauve souris est MORTE je l'ai TUER");
                    break;
                case "Golem":
                    gameManager.GetComponent<WeelManager>().UnlockCharacter("golem");
                    Debug.Log("Le Golem est MORT je l'ai TUER");
                    break;
                case "Human":
                    gameManager.GetComponent<WeelManager>().UnlockCharacter("human");
                    Debug.Log("L'humain est MORT je l'ai TUER");
                    break;
            }
        }



    }
}
