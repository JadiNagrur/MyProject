using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    Camera mainCamera;
    private Vector2 screenBounds;
    void Start()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        this.transform.localScale = new Vector3(screenBounds.x * 2, 0.1f, screenBounds.y * 2);
      
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            collision.gameObject.SetActive(false);
        }
    }
    
}
