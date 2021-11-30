using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    Camera mainCamera;
    private Vector2 screenBounds;
    float xMin, xMax, zMin, zMax;
    public float tilt;

    
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;
    void Start()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        xMin = -screenBounds.x;
        xMax= screenBounds.x;
        zMin = -screenBounds.y / 4;
        zMax = screenBounds.y - (screenBounds.y / 4);
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        float moveHz = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHz, 0.0f, moveV);
        rb.velocity = movement * speed;
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, xMin, xMax), 0.0f, Mathf.Clamp(rb.position.z, zMin, zMax));
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x* -tilt);
    }

    public void Update()
    {
        if(Input.GetMouseButton(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GameObject bullet = ObjectPooler.sharedInstance.GetPooledObject();
            if(bullet != null)
            {
                bullet.transform.position = shotSpawn.transform.position;
                bullet.transform.rotation = shotSpawn.transform.rotation;
                bullet.SetActive(true);
            }
        }
        
    }
}
