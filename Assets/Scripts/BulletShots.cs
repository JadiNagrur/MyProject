using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShots : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public float speed;
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
