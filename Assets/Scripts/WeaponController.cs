using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float delay;
    Vector2 screenBounds;
    void Start()
    {
        InvokeRepeating("Fire", delay, fireRate);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Fire()
    {
        if(this.transform.position.z <= -screenBounds.y / 2 || !this.gameObject.activeSelf)
        {
            return;
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
