using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    public float tumble;
    public Rigidbody rb;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;
        rb.velocity = -transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
