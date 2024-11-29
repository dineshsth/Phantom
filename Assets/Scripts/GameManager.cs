using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Card firstCard, secondCard;
    public CardManager cardManager;

    private void Awake()
    {
        instance = this;
    }
    public void OnCardSelected(Card card)
    {
        if (firstCard == null)
        {
            firstCard = card;
        }
        else
        {
            secondCard = card;
            StartCoroutine(CheckCards());
        }
    }
    private IEnumerator CheckCards()
    {
        Card fCard = firstCard;
        Card sCard = secondCard;

        firstCard = null;
        secondCard = null;

        if (fCard.GetCardData().cardNumber == sCard.GetCardData().cardNumber)
        {
            yield return new WaitForSeconds(1f);
            fCard.gameObject.SetActive(false);
            sCard.gameObject.SetActive(false);
        }
        else
        {
            fCard.HideCardFace();
            sCard.HideCardFace();
        }
    }
}