using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManuever : MonoBehaviour
{
    public Vector2 startWait;
    public float dodge;
    public Vector2 manueverTime;
    public Vector2 manueverWait;
    private float targetManuever;
    private Rigidbody rb;
    public float smoothing;
    private float currentSpeed;
    Boundary boundary;
    public float tilt;

    void Start()
    {
        StartCoroutine(Evade());
        rb = GetComponent<Rigidbody>();
        currentSpeed = rb.velocity.z;
        boundary = FindObjectOfType<Boundary>();

    }
    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        while (true)
        {
            targetManuever = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(manueverTime.x, manueverTime.y));
            targetManuever = 0;
            yield return new WaitForSeconds(Random.Range(manueverWait.x, manueverWait.y));
        }
    }

    private void FixedUpdate()
    {
        float newManuever = Mathf.MoveTowards(rb.velocity.x, targetManuever, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManuever, 0.0f, currentSpeed);
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, -boundary.screenBounds.x + 0.1f, boundary.screenBounds.x - 0.1f),
            0.0f,
             Mathf.Clamp(rb.position.z, -boundary.screenBounds.y , boundary.screenBounds.y )
            ) ;
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
