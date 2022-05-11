using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traveling : MonoBehaviour
{

    public float travelingSpeed = 10f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(travelingSpeed, 0f);
    }
}
