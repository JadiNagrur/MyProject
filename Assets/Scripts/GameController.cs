using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    Vector3 spawnValues;
    private Vector2 screenBounds;
    Camera mainCamera;
    public int hazardCountt;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Text scoreText;
    public Text highscore;
    public int score;
    //public GameObject powerUp;
    public GameObject gameOverPanel;
   [HideInInspector] public bool gameOver = false;
    public void Awake()
    {
        Time.timeScale = 1;
    }
    void Start()
    {
        if (!PlayerPrefs.HasKey("highscore"))
        {
            PlayerPrefs.SetInt("highscore", 0);
        }
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        Invoke("Waves",1f);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Random.Range(0, 1000) == 28 && gameOver==false && FindObjectOfType<PlayerController>().GetComponent<PlayerController>().bullets_PowerUp == false)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 0, screenBounds.y + 0.3f);
            //Instantiate(powerUp, spawnPosition, powerUp.transform.rotation);
        }*/
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
                GameObject hazrad = this.GetComponent<ObjectPooler>().GetPooledObject();

                if (hazrad != null)
                {
                    hazrad.transform.position = spawnPosition;
                    hazrad.transform.rotation = spawnRotation;
                    hazrad.SetActive(true);
                }

                yield return new WaitForSeconds(spawnWait);
            }
            Vector3 sPosition = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), 0, screenBounds.y + 0.3f);
            Quaternion sRotation = Quaternion.identity;
            GameObject enemy = this.GetComponent<EnemyPooler>().GetPooledObject();

            if (enemy != null)
            {
                enemy.transform.position = sPosition;
                enemy.transform.rotation = sRotation;
                enemy.SetActive(true);
            }


            yield return new WaitForSeconds(waveWait);
        }
      
       
      

    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        if (PlayerPrefs.GetInt("highscore") < score)
        {
            PlayerPrefs.SetInt("highscore", score);
            highscore.text = "High score: " + PlayerPrefs.GetInt("highscore").ToString();

        }
        //highscore.text = "High score: " + PlayerPrefs.GetInt("highscore").ToString();

    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void Waves()
    {
        StartCoroutine("SpawnWaves");
    }
}
