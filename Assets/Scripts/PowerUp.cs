using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    
    void Start()
    {
        Invoke("HidePowerUp", 10f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<PlayerController>().bullets_PowerUp = true;
            this.gameObject.SetActive(false);
        }
    }

    public void HidePowerUp()
    {
        this.gameObject.SetActive(false);
    }
}
