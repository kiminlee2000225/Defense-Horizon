using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static bool isGameOver = false;
    public static int enemyKilled = 0;
    public static int currLevel = 1;

    public Text timerText;
    public Text scoreText;
    public Vector3 cameraPosition;
    public float maxDuration = 50;
    public static float countDown;
    public GameObject pm;

    public AudioClip winSFX;
    public AudioClip LostSFX;

    private string thirdLevel = "LevelThree";
    private string secondLevel = "LevelTwo";
    private string firstLevel = "LevelOne";
    private string mainMenu = "MainMenu";

    public GameObject gameOverCanvas;
    public GameObject wallHealthText;
    public GameObject gameWonCanvas;
    public GameObject gameWonFinalCanvas;

    PauseMenuBehaviour pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.SetActive(false);
        gameWonCanvas.SetActive(false);
        gameWonFinalCanvas.SetActive(false);
        enemyKilled = 0;
        isGameOver = false;
        countDown = maxDuration;
        SetTimerText();
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenuBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").transform.position;

        scoreText.text = "Score: " + enemyKilled;

        if (!isGameOver)
        {
            if (countDown > 0.01f)
            {
                countDown -= Time.deltaTime;
                SetTimerText();
            }
            else
            {
                GameWon();
            }
        }
    }

    private void SetTimerText()
    {
        timerText.text = "Defend the turret for " + countDown.ToString("f2") + " seconds!";
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverCanvas.SetActive(true);
        wallHealthText.SetActive(false);

        AudioSource.PlayClipAtPoint(LostSFX, cameraPosition);
        deleteEnemies();
        pm.GetComponent<PlayerMovement>().playerIsDead();

        if (currLevel == 1)
        {
            enemyKilled = 0;
            Invoke("LoadFirstLevel", 4);
        }
        else if (currLevel == 2)
        {
            enemyKilled = 0;
            Invoke("LoadSecondLevel", 4);

        }
        else if (currLevel == 3)
        {
            enemyKilled = 0;
            Invoke("LoadThirdLevel", 4);
        }
    }

    public void GameWon()
    {
        isGameOver = true;
        wallHealthText.SetActive(false);

        AudioSource.PlayClipAtPoint(winSFX, cameraPosition);
        deleteEnemies();
        pm.GetComponent<PlayerMovement>().playerWon();

        var knights = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject knight in knights)
        {
            Destroy(knight);
        }

        var knightsBig = GameObject.FindGameObjectsWithTag("EnemyBig");
        foreach (GameObject knight in knightsBig)
        {
            Destroy(knight);
        }

        isGameOver = true;

        if (currLevel == 1)
        {
            pauseMenu.PauseGameForLoadingScreen();
            gameWonCanvas.SetActive(true);
            enemyKilled = 0;
            ++currLevel;
            Invoke("LoadSecondLevel", 4);
        }
        else if (currLevel == 2)
        {
            pauseMenu.PauseGameForLoadingScreen();
            gameWonCanvas.SetActive(true);
            enemyKilled = 0;
            ++currLevel;
            Invoke("LoadThirdLevel", 4);

        }
        else if (currLevel == 3)
        {
            pauseMenu.PauseGame();
            gameWonFinalCanvas.SetActive(true);
            enemyKilled = 0;
        }
    }

    private void deleteEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        GameObject[] enemiesBig = GameObject.FindGameObjectsWithTag("EnemyBig");
        foreach (GameObject enemy in enemiesBig)
        {
            Destroy(enemy);
        }
        Destroy(GameObject.FindGameObjectWithTag("Spawner"));
    }


    public void LoadThirdLevel()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CapsuleCollider>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
        SceneManager.LoadScene(thirdLevel);
    }

    public void LoadSecondLevel()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CapsuleCollider>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
        SceneManager.LoadScene(secondLevel);
    }

    public void LoadFirstLevel()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CapsuleCollider>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
        SceneManager.LoadScene(firstLevel);
    }

    public void MainMenuButtonClicked()
    {
        currLevel = 1;
        SceneManager.LoadScene(mainMenu);
    } 

}
