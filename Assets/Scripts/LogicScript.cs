using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public float speed;
    [SerializeField, Min(1.0f)] float speedIncreaseMultiplier;

    public GameObject gameOverScreen;
    public GameObject titleScreen;

    public GameObject[] spawners;
    public GameObject player;
    public TurtleController playerController;

    public Text scoreCounter;
    public Text highScoreCounter;
    public Text pauseIcon;

    public float timeBetweenScores;
    private float timer = 0.0f;

    public GameObject gameOverHighScoreText;
    public GameObject pauseButton;

    private int score = 0;
    private bool gameRunning = false;

    [SerializeField] private int acceleratesAt;

    private int highScore = 0;

    private bool pausedGame = false;

    [SerializeField] Animator openAndCloseAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<TurtleController>();

        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreCounter.text = "HIGH SCORE: " + highScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameRunning)
        {
            if (timer < timeBetweenScores)
            {
                timer += Time.deltaTime;
            }
            else
            {
                increaseScore();
                timer = 0.0f;
            }
        }
    }

    private void increaseScore()
    {
        score++;
        scoreCounter.text = score.ToString();

        // Update Speed
        if (score % acceleratesAt == 0)
        {
            speed *= speedIncreaseMultiplier;
            playerController.movementSpeed *= ((speedIncreaseMultiplier - 1) / 2) + 1;

            foreach (var spawner in spawners)
            {
                SpawnScript spawnerController = spawner.GetComponent<SpawnScript>();
                spawnerController.minSpawnRate /= speedIncreaseMultiplier;
                spawnerController.maxSpawnRate /= speedIncreaseMultiplier;
            }
        }
    }

    public void restartGame()
    {
        StartCoroutine(restartAnimation());
    }

    private IEnumerator restartAnimation()
    {
        openAndCloseAnimator.SetTrigger("StartClosingAnimation");
        yield return new WaitForSeconds(4f/6f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    [ContextMenu("Game Over")]
    public void gameOver()
    {
        gameRunning = false;
        gameOverScreen.SetActive(true);
        pauseButton.SetActive(false);
        foreach (var spawner in spawners)
        {
            spawner.SetActive(false);
        }
        if (score > highScore)
        {
            gameOverHighScoreText.SetActive(true);
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public void handleStartGameAnimation()
    {
        if (!gameRunning)
        {
            StartCoroutine(startGame());
        }
    }

    IEnumerator startGame()
    {
        gameRunning = true;
        openAndCloseAnimator.SetTrigger("StartClosingAnimation");
        yield return new WaitForSeconds(4f/6f);
        scoreCounter.text = "0";
        titleScreen.SetActive(false);
        foreach (var spawner in spawners)
        {
            spawner.SetActive(true);
        }
        player.SetActive(true);
        pauseButton.SetActive(true);
        openAndCloseAnimator.SetTrigger("StartOpenAnimation");
    }

    public void exit()
    {
        Application.Quit();
    }

    public void togglePause()
    {
        if (pausedGame)
        {
            Time.timeScale = 1.0f;
            pauseIcon.fontSize = 110;
            pauseIcon.text = "=";
            pausedGame = false;
            playerController.turtleIsAlive = true;
        }
        else
        {
            Time.timeScale = 0.0f;
            pauseIcon.fontSize = 70;
            pauseIcon.text = "V";
            pausedGame = true;
            playerController.turtleIsAlive = false;
        }
    }
}
