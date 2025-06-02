using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float gameOverHeight = 15f;
    public Transform player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public CinemachineVirtualCamera gameOverCam;

    [SerializeField] Animator animator;
    private readonly int HashGameOver = Animator.StringToHash("isGameOver");

    private float score = 0f;
    private bool isGameOver = false;
    private float highestY = 0f;
    private int highScore = 0;

    [SerializeField] private GameObject skyPrefab;       // 배경 슬라이스 프리팹
    [SerializeField] private float skySliceHeight = 20f;   // 한 슬라이스 높이 (프리팹 본래 높이와 동일하게 세팅)
    [SerializeField] private int initialSkySlices = 3;   // 게임 시작 시 몇 개 미리 깔아둘지
    [SerializeField] private float spawnAheadDistance = 30f; // 플레이어가 이만큼 높이에 다가오면 다음 배경 생성

    // 실제 스폰된 배경 조각들을 보관할 리스트(풀링 없이 단순 관리)
    private readonly List<GameObject> activeSkies = new List<GameObject>();
    private float nextSkySpawnY;    // “다음으로 스폰해야 할 Y 위치”

    [SerializeField] AudioClip gameOverSound;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        highScore = PlayerPrefs.GetInt("HighScore", 0);

        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);

        nextSkySpawnY = player.position.y - skySliceHeight;
        for (int i = 0; i < initialSkySlices; i++)
        {
            SpawnNextSky();
        }
    }

    private void Update()
    {
        if (isGameOver) return;

        UpdateScore();
        UpdateBg();
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
            scoreText.text = score.ToString();
        }
    }

    void UpdateBg()
    {
        // 플레이어가 nextSkySpawnY - spawnAheadDistance 이상 올라가면 다음 슬라이스 생성
        if (player.position.y + spawnAheadDistance > nextSkySpawnY)
        {
            SpawnNextSky();
        }

        // 화면 아래로 완전히 벗어난 배경은 제거
        // 예를 들어: “플레이어가 밑에서 2 * skySliceHeight 아래로 내려가면 삭제”
        float removeBelowY = player.position.y - (skySliceHeight * 2f);
        for (int i = activeSkies.Count - 1; i >= 0; i--)
        {
            if (activeSkies[i].transform.position.y < removeBelowY)
            {
                Destroy(activeSkies[i]);
                activeSkies.RemoveAt(i);
            }
        }
    }
    private void SpawnNextSky()
    {
        nextSkySpawnY += skySliceHeight;

        // Instantiate
        Vector3 spawnPos = new Vector3(0f, nextSkySpawnY, 10f);
        GameObject newSlice = Instantiate(skyPrefab, spawnPos, Quaternion.identity);
        activeSkies.Add(newSlice);
    }
    public void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
        gameOverCam.gameObject.SetActive(true);
        gameOverCam.transform.position = new Vector3(0, highestY - 40, -10);
        scoreText.gameObject.SetActive(false);

        SoundManager.Instance.PlaySFX(gameOverSound);

        animator.SetBool(HashGameOver, true);

        if (score > highScore)
        {
            highScore = (int)score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
            highScoreText.text = "Score : " + score.ToString() +
            "\nBest : " + highScore.ToString();
    }

    //public void Restart()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}

    public void OnPauseClicked()
    {
        Debug.Log("Pause Clicked");
        Time.timeScale = 0f;
        GameManager.Instance.pausePanel.SetActive(true);
    }
}
