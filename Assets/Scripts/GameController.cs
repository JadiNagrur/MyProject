using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector3 spawnValues;
    private Vector2 screenBounds;
    Camera mainCamera;
    public int hazardCountt;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    void Start()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        Invoke("Waves",1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCountt; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), 0, screenBounds.y + 0.3f);
                Quaternion spawnRotation = Quaternion.identity;
                GameObject hazrad = ObjectPooler.SharedInstance.GetPooledObject();

                if (hazrad != null)
                {
                    hazrad.transform.position = spawnPosition;
                    hazrad.transform.rotation = spawnRotation;
                    hazrad.SetActive(true);
                }
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
      
       
      

    }

    public void Waves()
    {
        StartCoroutine("SpawnWaves");
    }
}
