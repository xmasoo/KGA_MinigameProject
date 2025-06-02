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

    [SerializeField] private GameObject skyPrefab;       // ��� �����̽� ������
    [SerializeField] private float skySliceHeight = 20f;   // �� �����̽� ���� (������ ���� ���̿� �����ϰ� ����)
    [SerializeField] private int initialSkySlices = 3;   // ���� ���� �� �� �� �̸� ��Ƶ���
    [SerializeField] private float spawnAheadDistance = 30f; // �÷��̾ �̸�ŭ ���̿� �ٰ����� ���� ��� ����

    // ���� ������ ��� �������� ������ ����Ʈ(Ǯ�� ���� �ܼ� ����)
    private readonly List<GameObject> activeSkies = new List<GameObject>();
    private float nextSkySpawnY;    // ���������� �����ؾ� �� Y ��ġ��

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
        if (player.position.y < highestY - 20f)//Ư�������̻� �������� ���ӿ���
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
        // �÷��̾ nextSkySpawnY - spawnAheadDistance �̻� �ö󰡸� ���� �����̽� ����
        if (player.position.y + spawnAheadDistance > nextSkySpawnY)
        {
            SpawnNextSky();
        }

        // ȭ�� �Ʒ��� ������ ��� ����� ����
        // ���� ���: ���÷��̾ �ؿ��� 2 * skySliceHeight �Ʒ��� �������� ������
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
