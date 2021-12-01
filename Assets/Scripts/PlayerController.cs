﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    Camera mainCamera;
    private Vector2 screenBounds;
    float xMin, xMax, zMin, zMax;
    public float tilt;
    public Text timeTextPowerUp;
    public bool bullets_PowerUp = false;
    private float time = 20.0f;

    public Transform shotSpawn;
    public Transform shotSpawnLeft;
    public Transform shotSpawnRight;
    public float fireRate;
    private float nextFire;

    private bool isMoving = false;
    public bool gameOver = false;
    Collider col;
    Vector3 pos;
    Camera cam;
    int health = 3;
    public GameObject playerExplosion;
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
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, xMin, xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, zMin, zMax)
            );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);

    }
    public void Update()
    {
        if (bullets_PowerUp == true)
        {
            time -= Time.deltaTime * 1;
            timeTextPowerUp.text = time.ToString("f0");
            if (time <= 0)
            {
                bullets_PowerUp = false;
                timeTextPowerUp.text = "";
                time = 15.0f;
            }
        }
        if (!gameOver)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (col.Raycast(ray, out hit, 100.0f))
            {
                isMoving = Input.GetMouseButton(0);
            }
        }
        if (isMoving)
        {

            pos.x = cam.ScreenToWorldPoint(Input.mousePosition).x;
            pos.z = cam.ScreenToWorldPoint(Input.mousePosition).z;

            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                if (bullets_PowerUp)
                {
                    GameObject bullet1 = gameObject.GetComponent<ObjectPooler>().GetPooledObject();
                    if (bullet1 != null)
                    {
                        bullet1.transform.position = shotSpawnLeft.transform.position;
                        bullet1.transform.rotation = shotSpawnLeft.transform.rotation;
                        bullet1.SetActive(true);
                    }
                    GameObject bullet2 = gameObject.GetComponent<ObjectPooler>().GetPooledObject();
                    if (bullet2 != null)
                    {
                        bullet2.transform.position = shotSpawnRight.transform.position;
                        bullet2.transform.rotation = shotSpawnRight.transform.rotation;
                        bullet2.SetActive(true);
                    }
                }
                GameObject bullet = gameObject.GetComponent<ObjectPooler>().GetPooledObject();
                if (bullet != null)
                {
                    bullet.transform.position = shotSpawn.transform.position;
                    bullet.transform.rotation = shotSpawn.transform.rotation;
                    bullet.SetActive(true);
                }

            }
        }


    }

    public void OnTriggerEnter(Collider other)
    {
       if( other.gameObject.tag.Equals("enemyBullet"))
        {
            if (health < 0)
            {
                FindObjectOfType<GameController>().GetComponent<GameController>().gameOver = true;
                other.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
                playerExplosion.transform.position = other.transform.position;
                playerExplosion.GetComponent<ParticleSystem>().Play();
                FindObjectOfType<AudioManager>().GetComponent<AudioManager>().PlayAudio(1);
                Invoke("GameOver", 2f);
            }
            else
            {
                other.gameObject.SetActive(false);
                health -= 1;
            }



        }

} 
    public void GameOver()
    {
        FindObjectOfType<GameController>().GetComponent<GameController>().GameOver();
    }

}
