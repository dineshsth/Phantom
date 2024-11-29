using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private GameObject gameCompletePanel;

    [SerializeField] private TextMeshProUGUI matchesText;
    [SerializeField] private TextMeshProUGUI turnsText;


    private void Start()
    {
        gameCompletePanel.SetActive(false);
        gameManager = GameManager.instance;

        matchesText.text = 0.ToString();
        turnsText.text = 0.ToString();

        gameManager.OnGameComplete += GameManager_OnGameComplete;
        gameManager.OnTurnUpdated += GameManager_OnTurnUpdated;
        gameManager.OnMatchUpdated += GameManager_OnMatchUpdated;
    }

    private void OnDisable()
    {
        gameManager.OnGameComplete -= GameManager_OnGameComplete;
        gameManager.OnTurnUpdated -= GameManager_OnTurnUpdated;
        gameManager.OnMatchUpdated -= GameManager_OnMatchUpdated;
    }

    private void GameManager_OnMatchUpdated(int value) => matchesText.text = value.ToString();
    private void GameManager_OnTurnUpdated(int value) => turnsText.text = value.ToString();


    private void GameManager_OnGameComplete()
    {
        gameCompletePanel.SetActive(true);
    }
}
