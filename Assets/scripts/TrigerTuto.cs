using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerTuto : MonoBehaviour
{
    private bool isDone;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isDone)
        {
            GetComponent<DialogueTriger>().TrigerDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isDone = true;
    }
}
