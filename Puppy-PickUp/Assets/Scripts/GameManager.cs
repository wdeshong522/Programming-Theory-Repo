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
    public TextMeshProUGUI waitForNewLevelsText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;

    public int level;
    public int toysSpawned;
    private int timeRemaining;
    public bool isGameActive;
    private int timeAddedForCompletion = 15;

    private float xSpawnRangeMin = -20.0f;
    private float xSpawnRangeMax = 18.0f;
    private float zSpawnRangeMin = -4.5f;
    private float zSpawnRangeMax = 9.0f;

    public GameObject pauseScreen;
    private bool paused;
    private bool completedLevel = false;
    private int maxLevel;

    // Start is called before the first frame update
    void Start()
    {
        counterScript = GameObject.Find("Box").GetComponent<Counter>();
        level = 1;

        CalculateMaxLevel();
        
    }
       

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }

        if (counterScript.Count <= 0 && isGameActive)
        {
            IncrementLevel();
        }
    }

    void SpawnToys(int levelNum)
    {
        
        toysSpawned = 0;
        int toyCount = toys.Count;
        string output = Convert.ToString(levelNum, 2);
        Char[] letters = output.ToCharArray();
        
        for (int i = 0; i < letters.Length; i++)
        {
            int intLetter = letters[letters.Length - i - 1] - '0';
            if (intLetter == 1)
            {
                Instantiate(toys[i], GenerateSpawnPosition(), toys[i].transform.rotation);
                toysSpawned++;
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

        titleScreen.gameObject.SetActive(false);
        SpawnToys(level);
        UpdateTime(timeRemaining);
        StartCoroutine(TimeCountdown(timeRemaining));
        counterScript.CounterText.text = "Toys Remaining: " + counterScript.Count;
    }

    public void UpdateTime(int timeRemaining)
    {
        timeText.text = "Time Remaining: " + timeRemaining;
    }

    
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

    void PauseGame()
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

    void CalculateMaxLevel()
    {
        maxLevel = (int)(Math.Pow(2, toys.Count) - 1);
    }

    void IncrementLevel()
    {
        completedLevel = true;
        level++;
        if (level <= maxLevel)
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
