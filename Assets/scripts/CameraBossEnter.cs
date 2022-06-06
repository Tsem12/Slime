using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBossEnter : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Camera bossCamera;


    private void Start()
    {
        bossCamera = GetComponent<Camera>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mainCamera.enabled = false;
            bossCamera.enabled = true;
            GameManager.instance.activeCamera = bossCamera;
        }
    }
}
