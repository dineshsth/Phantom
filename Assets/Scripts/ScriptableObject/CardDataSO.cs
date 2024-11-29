using UnityEngine;

[CreateAssetMenu(fileName = "Card Data", menuName = "Card Object SO/Card Data")]
public class CardDataSO : ScriptableObject
{
    public Sprite sprite;
    public string cardName;
    public int cardNumber;
}