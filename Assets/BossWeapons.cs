using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapons : MonoBehaviour
{
    public int laserDamage = 2;
    public int meleeDamage = 20;

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
}
