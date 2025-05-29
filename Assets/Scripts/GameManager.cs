using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float score = 0f;
    public float gameOverHeight = 15f;
    public Transform player;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public CinemachineVirtualCamera gameOverCam;
    

    private bool isGameOver = false;
    private float highestY = 0f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (isGameOver) return;

        UpdateScore();
        if (player.position.y < highestY - 10f)
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
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
