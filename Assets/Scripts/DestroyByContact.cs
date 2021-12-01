using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public int health;
    public GameObject explosion;
    public GameObject playerExplosion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("bullet"))
        {
            if (health < 0)
            {
                explosion.transform.position = this.transform.position;
                explosion.GetComponent<ParticleSystem>().Play();
                other.gameObject.SetActive(false);
                this.gameObject.SetActive(false);

            }

            else
            {
                health -= 1;
            }
        }
        if (other.gameObject.tag.Equals("Player"))
        {
            explosion.transform.position = this.transform.position;
            explosion.GetComponent<ParticleSystem>().Play();
            this.gameObject.SetActive(false);

            playerExplosion.transform.position = other.transform.position;
            playerExplosion.GetComponent<ParticleSystem>().Play();
            other.gameObject.SetActive(false);
        }
    }
}
