using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStart : MonoBehaviour
{
    public Animator animator;
    public Renderer renderer;
    public Collider2D cl;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

            Debug.Log("sddc");
            StartCoroutine(Init());
            
        }
    }

        IEnumerator Init()
        {
            renderer.enabled = true;
            yield return new WaitForSeconds(1);
            animator.SetBool("Entery", true);
            yield return new WaitForSeconds(1);
            animator.SetBool("Idle", true);
        }
}
