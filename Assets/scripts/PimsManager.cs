using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PimsManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameManager.instance.pimsAmount += 1;
            Debug.Log(GameManager.instance.pimsAmount);
        }
    }
}
