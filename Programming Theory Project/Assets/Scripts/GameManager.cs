using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public List<GameObject> toys;
    private Counter counterScript;

    public TextMeshProUGUI timeText;
    //public TextMeshProUGUI scoreText;
    //public TextMeshProUGUI livesText;
    public TextMeshProUGUI waitForNewLevelsText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;

    public int level;
    public int toysSpawned;
    private int timeRemaining;
    //private int score;
    //public int lives;
    //private float spawnRate = 1.0f;
    public bool isGameActive;
    private int timeAddedForCompletion = 15;

    private float xSpawnRangeMin = -20.0f;
    private float xSpawnRangeMax = 18.0f;
    private float zSpawnRangeMin = -4.5f;
    private float zSpawnRangeMax = 9.0f;

    public GameObject pauseScreen;
    private bool paused;
    private bool completedLevel = false;
    private int maxLevel = 15;

    // Start is called before the first frame update
    void Start()
    {
        counterScript = GameObject.Find("Box").GetComponent<Counter>();
        level = 14;
    }
       

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }

        

        if (counterScript.Count <= 0 && isGameActive)
        {
            completedLevel = true;
            level++;
            if(level <= maxLevel)
            {
                timeRemaining += timeAddedForCompletion;
                SpawnToys(level);
                counterScript.CounterText.text = "Toys Remaining: " + counterScript.Count;
            }
            else if (level > maxLevel)
            {
                GameOver();
            }
            
        }
        

    }

    void SpawnToys(int levelNum)
    {
        
        toysSpawned = 0;
        int toyCount = toys.Count;
        string output = Convert.ToString(levelNum, 2);
        Char[] letters = output.ToCharArray();
        
        
        Debug.Log("Level: " + levelNum);
        Debug.Log("Length of string: " + letters.Length);
        
        //Instantiate(toys[i], GenerateSpawnPosition(), toys[i].transform.rotation);
        
        for (int i = 0; i < letters.Length; i++)
        {
            
            Debug.Log("Iterator: " + i); 
            Debug.Log("Place in string: " + letters[letters.Length - i - 1]);
            
            int intLetter = letters[letters.Length - i - 1] - '0';
            Debug.Log("Used for conditional: " + intLetter);
            if (intLetter == 1)
            {
                Debug.Log(toys[i].name);
                Instantiate(toys[i], GenerateSpawnPosition(), toys[i].transform.rotation);
                toysSpawned++;
                Debug.Log(toysSpawned);
            }

        }
        counterScript.Count = toysSpawned;
        
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = UnityEngine.Random.Range(xSpawnRangeMin, xSpawnRangeMax);
        float spawnPosZ = UnityEngine.Random.Range(zSpawnRangeMin, zSpawnRangeMax);

        Vector3 randomPos = new Vector3(spawnPosX, 0.3f, spawnPosZ);

        return randomPos;
    }

    /*public void UpdateLives(int lives)
    {
        livesText.text = "Lives: " + lives;
    }*/

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

        if(level > maxLevel)
        {
            waitForNewLevelsText.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        isGameActive = true;
        timeRemaining = 60;
        //score = 0;
        //lives = 3;
        //spawnRate /= difficulty;

        titleScreen.gameObject.SetActive(false);
        SpawnToys(level);
        UpdateTime(timeRemaining);
        StartCoroutine(TimeCountdown(timeRemaining));
        counterScript.CounterText.text = "Toys Remaining: " + counterScript.Count;
        //UpdateScore(score);
        //UpdateLives(lives);
    }

    public void UpdateTime(int timeRemaining)
    {
        timeText.text = "Time Remaining: " + timeRemaining;
    }

    // Update score with value from target clicked
    IEnumerator TimeCountdown(int timeRemaining)
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(1);
            if (completedLevel == true)
            {
                timeRemaining += timeAddedForCompletion - 1;
                completedLevel = false;
            }
            else
            {
                timeRemaining -= 1;
            }
            UpdateTime(timeRemaining);
            if (timeRemaining < 1)
            {
                GameOver();
            }
        }

    }

    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
