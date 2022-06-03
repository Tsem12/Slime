using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
   
    public float speed;
    public Transform[] waypoints;
    public SpriteRenderer graphics;
    public bool isStatic;
    [HideInInspector] public bool isLeft;

    private Transform target;
    private int destPoint = 0;
    public bool isDead = false;
    public bool isPatrol = true;

    void Start()
    {
        if(!isStatic)
            target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isDead);
        if (!isDead && isPatrol == true)
        {
            if(!isStatic)
            {
                Vector3 dir = target.position - transform.position;
                transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

                if(Vector3.Distance(transform.position, target.position) < 0.3f)
                {
                    destPoint = (destPoint + 1) % waypoints.Length;
                    target = waypoints[destPoint];
                    graphics.flipX = !graphics.flipX;
                }
            }
        }else if (isPatrol == false && !isDead)
        {
            if (transform.position.x - SwitchCharacter.instance.activeCharacter.transform.position.x < 0)
                graphics.flipX = false;
            else
                graphics.flipX = true;
        }
        

        
    }
}
