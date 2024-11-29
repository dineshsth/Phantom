using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private Button playButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button saveButton;

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamepanel;
    [SerializeField] private GameObject gameCompletePanel;
    [SerializeField] private GameObject messagePanel;

    [SerializeField] private TextMeshProUGUI matchesText;
    [SerializeField] private TextMeshProUGUI turnsText;
    [SerializeField] private TextMeshProUGUI messageText;


    private void Start()
    {
        menuPanel.SetActive(true);
        gamepanel.SetActive(false);
        gameCompletePanel.SetActive(false);
        messagePanel.SetActive(false);

        gameManager = GameManager.instance;

        matchesText.text = 0.ToString();
        turnsText.text = 0.ToString();

        playButton.onClick.AddListener(StartGame);
        loadButton.onClick.AddListener(LoadGame);
        saveButton.onClick.AddListener(SaveGame);

        gameManager.OnGameComplete += GameManager_OnGameComplete;
        gameManager.OnTurnUpdated += GameManager_OnTurnUpdated;
        gameManager.OnMatchUpdated += GameManager_OnMatchUpdated;
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(StartGame);
        loadButton.onClick.RemoveListener(LoadGame);
        saveButton.onClick.RemoveListener(SaveGame);

        gameManager.OnGameComplete -= GameManager_OnGameComplete;
        gameManager.OnTurnUpdated -= GameManager_OnTurnUpdated;
        gameManager.OnMatchUpdated -= GameManager_OnMatchUpdated;
    }

    private void StartGame()
    {
        menuPanel.SetActive(false);
        gamepanel.SetActive(true);
        gameManager.StartNewGame();
    }
    private void LoadGame()
    {
        // check if file exist in location;
        if (!File.Exists(SaveSystem.SaveFileName()))
        {
            DisplayMessage("No data to load");
            return;
        }
        menuPanel.SetActive(false);
        gamepanel.SetActive(true);
        SaveSystem.Load();
    }
    private void SaveGame()
    {
        SaveSystem.Save();
        DisplayMessage("Saved !!!");
    }

    private void GameManager_OnMatchUpdated(int value) => matchesText.text = value.ToString();
    private void GameManager_OnTurnUpdated(int value) => turnsText.text = value.ToString();


    private void GameManager_OnGameComplete()
    {
        gameCompletePanel.SetActive(true);
    }

    private void DisplayMessage(string message)
    {
        if (messagePanel.activeSelf) return;

        messagePanel.SetActive(true);
        messageText.text = message;

        Invoke(nameof(SetMessageOff), 1f);
    }
    private void SetMessageOff()
    {
        messagePanel.SetActive(false);
    }
}