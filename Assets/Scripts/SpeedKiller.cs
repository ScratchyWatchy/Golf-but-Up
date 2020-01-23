using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedKiller : MonoBehaviour
{
    public float threshold = 0.06f;
    Rigidbody2D rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(rb.velocity.x) < threshold)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Mathf.Abs(rb.velocity.y) < threshold)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
}
