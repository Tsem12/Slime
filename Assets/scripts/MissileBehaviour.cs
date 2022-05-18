using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private Transform initPos;
    private Vector2 lookDirection;
    private float lookAngle;

    [SerializeField]
    private GameObject parent;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initPos = GetComponentInChildren<Transform>();
        transform.position = initPos.position;
    }

    void Update()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        if (parent.transform.eulerAngles.y == 0)
        {
            if (lookAngle < 90 && lookAngle >= 0 || lookAngle > -90 && lookAngle <= 0)
                transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
        }
        else
        {
            if (lookAngle > 90 && lookAngle <= 180 || lookAngle < -90 && lookAngle >= -180)
                transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
        }




        Debug.Log(lookAngle);


    }

}
