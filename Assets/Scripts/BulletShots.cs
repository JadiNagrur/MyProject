using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShots : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = transform.up * speed;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
