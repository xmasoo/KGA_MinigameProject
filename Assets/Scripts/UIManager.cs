using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private string titleSceneName = "TitleScene";
    [SerializeField] private string gameplaySceneName = "JumpGameScene";

    private Button playBtn, optionBtn, quitBtn, closeBtn;
    private Button restartBtn, menuBtn;
    private Button resumeBtn, restartBtn2, menuBtn2;
    private GameObject optionPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != gameplaySceneName)
            return;

        // Esc 키 눌렀을 때
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Time.timeScale 이 1 이면 일시정지, 0 이면 다시 시작
            if (Time.timeScale > 0f)
                OnPauseClicked();
            else
                OnResumeClicked();
        }
    }
    private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1;
        Debug.Log($"[UIManager] Loaded Scene: {scene.name}");
        if (scene.name == titleSceneName)
        {
            playBtn = GameObject.Find("PlayBtn")?.GetComponent<Button>();
            optionBtn = GameObject.Find("OptionBtn")?.GetComponent<Button>();
            quitBtn = GameObject.Find("QuitBtn")?.GetComponent<Button>();
            closeBtn = GameObject.Find("CloseBtn")?.GetComponent<Button>();
            optionPanel = GameObject.Find("OptionPanel");

            Debug.Log($" -> PlayBtn: {playBtn}, OptionBtn: {optionBtn}, QuitBtn: {quitBtn}, Panel: {optionPanel}");

            if (playBtn != null)
            {
                playBtn.onClick.RemoveAllListeners();
                playBtn.onClick.AddListener(OnPlayClicked);
            }
            if (optionBtn != null)
            {
                optionBtn.onClick.RemoveAllListeners();
                optionBtn.onClick.AddListener(OnOptionsClicked);
            }
            if (quitBtn != null)
            {
                quitBtn.onClick.RemoveAllListeners();
                quitBtn.onClick.AddListener(OnQuitClicked);
            }
            if (closeBtn != null)
            {
                closeBtn.onClick.RemoveAllListeners();
                closeBtn.onClick.AddListener(OnCloseClicked);
            }


            if (optionPanel != null)
                optionPanel.SetActive(false);
        }
        else if (scene.name == gameplaySceneName)
        {
            var canvas = GameObject.Find("Canvas");
            var buttons = canvas.GetComponentsInChildren<Button>(true);
            foreach (var btn in buttons)
            {
                switch (btn.name)
                {
                    case "ResumeBtn":
                        resumeBtn = btn;
                        resumeBtn.onClick.RemoveAllListeners();
                        resumeBtn.onClick.AddListener(OnResumeClicked);
                        break;
                    case "RestartBtn":
                        restartBtn2 = btn;
                        restartBtn2.onClick.RemoveAllListeners();
                        restartBtn2.onClick.AddListener(OnRestartClicked);
                        break;
                    case "MenuBtn":
                        menuBtn2 = btn;
                        menuBtn2.onClick.RemoveAllListeners();
                        menuBtn2.onClick.AddListener(OnMenuClicked);
                        break;
                }
            }
        }

    }

    private void OnPlayClicked()
    {
        Debug.Log("Play Clicked");
        SceneManager.LoadScene(gameplaySceneName);
    }

    private void OnOptionsClicked()
    {
        Debug.Log("Options Clicked");
        if (optionPanel != null)
            optionPanel.SetActive(!optionPanel.activeSelf);
    }

    private void OnQuitClicked()
    {
        Debug.Log("Quit Clicked");
        Application.Quit();
    }

    private void OnCloseClicked()
    {
        Debug.Log("Close Clicked");
        optionPanel.SetActive(false);
    }
    private void OnRestartClicked()
    {
        Debug.Log("Restart Clicked");        
        SceneManager.LoadScene(gameplaySceneName);
    }

    private void OnMenuClicked()
    {
        Debug.Log("Menu Clicked");
        SceneManager.LoadScene(titleSceneName);
    }


    public void OnPauseClicked()
    {
        Debug.Log("Pause Clicked");
        Time.timeScale = 0f;
        GameManager.Instance.pausePanel.SetActive(true);
    }
    private void OnResumeClicked()
    {
        Debug.Log("Resume Clicked");
        Time.timeScale = 1.0f;
        GameManager.Instance.pausePanel.SetActive(false);
    }

}
