using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerTuto : MonoBehaviour
{
    [HideInInspector] public bool isDone;
    [SerializeField] DialogueTrigger dialogue;

    private void Start()
    {
        if(dialogue == null)
        {
            dialogue = GetComponent<DialogueTrigger>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isDone)
        {
            dialogue.TrigerDialogue();
            isDone = true;
        }
    }

}
