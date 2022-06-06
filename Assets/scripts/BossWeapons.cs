using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapons : MonoBehaviour
{


    public int meleeDamage = 20;
    [HideInInspector] public Transform player;
    [SerializeField] private int kbForce;


    [Header("Golem")]
    public int laserDamage = 2;
    public GameObject missile;
    [HideInInspector] public float angle;
    [HideInInspector] public Vector2 target;




    public void LaserAttack(GameObject player)
    {
        player.GetComponentInParent<PlayerHealth>().TakeDamage(laserDamage);
    }

    public void MeleeAttack(GameObject player)
    {
        player = GetComponentInChildren<BossAtkHitBox>().player;
        GetComponent<Animator>().SetBool("Melee", false);
        GetComponent<Animator>().SetBool("MeleeP2", false);
        
        if(GetComponentInChildren<BossAtkHitBox>().isInRange == true)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

            player.GetComponentInParent<PlayerHealth>().TakeDamage(meleeDamage);
            StartCoroutine(Knockback(rb));
        }
    }

    public void MissileAttack()
    {
        missile.SetActive(true);

        player = SwitchCharacter.instance.activeCharacter.transform.Find("Feet");
        target = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        angle = Mathf.Atan2(player.position.y - transform.position.y, player.position.x - transform.position.x) * Mathf.Rad2Deg;
        missile.transform.rotation = Quaternion.Euler(0, 0, angle );
        missile.GetComponent<Rigidbody2D>().velocity = new Vector2(target.x * 3f, target.y * 3f);

    }

    public void DoDamage(int damage)
    {
        player.GetComponentInParent<PlayerHealth>().TakeDamage(damage);
    }

    private IEnumerator Knockback(Rigidbody2D rb)
    {
        int kbDirection = transform.eulerAngles.y == 0 ? kbForce : -kbForce;
        GameManager.instance.isInputEnable = false;
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(kbDirection, 150));
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.isInputEnable = true;
    }
}
