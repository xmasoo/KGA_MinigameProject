using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float gameOverHeight = 15f;
    public Transform player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverPanel;
    public CinemachineVirtualCamera gameOverCam;

    [SerializeField] Animator animator;
    private readonly int HashGameOver = Animator.StringToHash("isGameOver");

    private float score = 0f;
    private bool isGameOver = false;
    private float highestY = 0f;
    private int highScore = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        highScore = PlayerPrefs.GetInt("HighScore", 0);

        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if (isGameOver) return;

        UpdateScore();
        if (player.position.y < highestY - 20f)//특정높이이상 떨어지면 게임오버
        {
            GameOver();
        }
        
    }

    void UpdateScore()
    {
        if (player.position.y > highestY)
        {
            highestY = player.position.y;
            score = Mathf.Floor(highestY * 10f);
            scoreText.text = "Score: " + score.ToString();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
        gameOverCam.gameObject.SetActive(true);
        gameOverCam.transform.position = new Vector3(0, highestY - 40, -10);
        scoreText.gameObject.SetActive(false);

        animator.SetBool(HashGameOver, true);

        if (score > highScore)
        {
            highScore = (int)score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
            highScoreText.text = "Score : " + score.ToString() +
            "\nBest : " + highScore.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
