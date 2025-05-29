using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private string titleSceneName = "TitleScene";
    [SerializeField] private string gameplaySceneName = "JumpGameScene";

    private Button playBtn, optionBtn, quitBtn;
    private Button restartBtn, menuBtn;
    private GameObject optionPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // 이벤트는 OnEnable/OnDisable에서 걸면 더 안전합니다
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

    private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"[UIManager] Loaded Scene: {scene.name}");
        if (scene.name == titleSceneName)
        {
            // 버튼 오브젝트 이름이 정확한지 콘솔에 출력해보세요
            playBtn = GameObject.Find("PlayBtn")?.GetComponent<Button>();
            optionBtn = GameObject.Find("OptionBtn")?.GetComponent<Button>();
            quitBtn = GameObject.Find("QuitBtn")?.GetComponent<Button>();
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

            if (optionPanel != null)
                optionPanel.SetActive(false);
        }
        else if (scene.name == gameplaySceneName)
        {
            var goPanel = GameManager.Instance.gameOverPanel.transform;
            restartBtn = goPanel.Find("RestartBtn")?.GetComponent<Button>();
            menuBtn = goPanel.Find("MenuBtn")?.GetComponent<Button>();

            Debug.Log($" -> RestartBtn: {restartBtn}, MenuBtn: {menuBtn}");

            if (restartBtn != null)
            {
                restartBtn.onClick.RemoveAllListeners();
                restartBtn.onClick.AddListener(OnRestartClicked);
            }
            if (menuBtn != null)
            {
                menuBtn.onClick.RemoveAllListeners();
                menuBtn.onClick.AddListener(OnMenuClicked);
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
}
