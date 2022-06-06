using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAttacks : MonoBehaviour
{
    [HideInInspector] public Collider2D player;
    public bool canDamage;
    public int range;
    public int damage;
    [SerializeField] private  LayerMask layer;
    public GameObject atkHitBox;
    void Update()
    {
        player = Physics2D.OverlapCircle(atkHitBox.transform.position, range, layer);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(atkHitBox.transform.position, range);
    }

    public void LaunchCoroutine(Rigidbody2D rb, Transform target, GameObject hitBox, int kbForce)
    {
        StartCoroutine(Knockback(rb, target, hitBox, kbForce));
    }

    private IEnumerator Knockback(Rigidbody2D rb, Transform target, GameObject hitBox, int kbForce)
    {
        int kbDirection = hitBox.transform.position.x - target.position.x < 0 ? kbForce : -kbForce;
        GameManager.instance.isInputEnable = false;
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(kbDirection, 150));
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.isInputEnable = true;
    }

}
