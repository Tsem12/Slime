using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBossExit : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Camera bossCamera;

    private void Start()
    {
        bossCamera = GetComponentInParent<Camera>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mainCamera.enabled = true;
            bossCamera.enabled = false;
            GameManager.instance.activeCamera = mainCamera;
        }
    }
}
