using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Text restartText;
    public Text gameOverText;
    public AudioSource audioSource;
    public AudioClip backgroundMusic;
    public AudioClip winMusic;
    public AudioClip loseMusic;
    public PlayerController playerController;
    

    private bool gameOver;
    private bool restart;

    public Text scoreText;
    public int score;

    void Start()
    {
       
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        restartText.text = "";
        gameOverText.text = "";
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range (0,hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if(gameOver)
            {
                restartText.text = "Press 'F' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValues)
    {
        score += newScoreValues;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
        if (score >= 100)
        {
            gameOverText.text = "You Win! Game created by Theron Harrison.";
            gameOver = true;
            WinMusic();
        }
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
        LoseMusic();
    }

    public void WinMusic()
    {
        audioSource.loop = false;
        audioSource.clip = winMusic;
        audioSource.Play();
    }

    public void LoseMusic()
    {
        audioSource.loop = false;
        audioSource.clip = loseMusic;
        audioSource.Play();
    }
}


  

    

