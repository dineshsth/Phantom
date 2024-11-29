using UnityEngine;
using UnityEngine.UI;

public class ToggleFunctionality : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private int rows, columns;

    private void OnEnable()
    {
        toggle.onValueChanged.AddListener(SendMessage);
    }
    private void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(SendMessage);
    }
    private void SendMessage(bool toggleValue)
    {
        if (toggleValue)
        {
            GameManager.instance.cardManager.SetRowsAndColumns(rows, columns);
        }
    }
}