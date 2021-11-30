using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    // Start is called before the first frame update
    public float scrollSpeed;
    private Vector3 startPosition;
    void Start()
    {
        startPosition.x = transform.position.x;
        startPosition.y = transform.position.y;
        startPosition.z = transform.position.z; 
        
    }

    // Update is called once per frame
    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, this.transform.localScale.y);
        transform.position = startPosition + Vector3.forward * newPosition;
        
    }
}
