using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapons : MonoBehaviour
{
    public int laserDamage = 2;
    public int meleeDamage = 20;
    public SwitchCharacter switchCharacter;
    [HideInInspector] public Transform player;

    [Header("Missile")]
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
            player.GetComponentInParent<PlayerHealth>().TakeDamage(meleeDamage);
    }

    public void MissileAttack()
    {
        missile.SetActive(true);

        player = switchCharacter.activeCharacter.transform.Find("Feet");
        target = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        angle = Mathf.Atan2(player.position.y - transform.position.y, player.position.x - transform.position.x) * Mathf.Rad2Deg;
        missile.transform.rotation = Quaternion.Euler(0, 0, angle );
        missile.GetComponent<Rigidbody2D>().velocity = new Vector2(target.x * 3f, target.y * 3f);

    }

    public void DoDamage(int damage)
    {
        player.GetComponentInParent<PlayerHealth>().TakeDamage(damage);
    }
}
