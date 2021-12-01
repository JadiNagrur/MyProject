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

    private bool isMoving = false;
    public bool gameOver = false;
    Collider col;
    Vector3 pos;
    Camera cam;

    public void Start()
    {
        col = GetComponent<Collider>();
        cam = Camera.main;
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        xMin = -screenBounds.x;
        xMax = screenBounds.x;
        zMin = -screenBounds.y / 4;
        zMax = screenBounds.y - (screenBounds.y / 4);
        rb = this.GetComponent<Rigidbody>();


    }

    public void FixedUpdate()
    {
        if (isMoving)
        {
            rb.MovePosition(pos);
            rb.velocity = pos;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }


        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;
        rb.position = new Vector3(Mathf.Clamp(rb.position.x,xMin,xMax), 
            0.0f, 
            Mathf.Clamp(rb.position.z,zMin,zMax)
            );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);

    }
    public void Update()
    {
        if (!gameOver)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(col.Raycast(ray, out hit, 100.0f))
            {
                isMoving = Input.GetMouseButton(0);
            }
        }
        if (isMoving)
        {

            pos.x = cam.ScreenToWorldPoint(Input.mousePosition).x;
            pos.z = cam.ScreenToWorldPoint(Input.mousePosition).z;

            if ( Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();
                if (bullet != null)
                {
                    bullet.transform.position = shotSpawn.transform.position;
                    bullet.transform.rotation = shotSpawn.transform.rotation;
                    bullet.SetActive(true);
                }

            }
        }
       
    
    }
}
