using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Card firstCard, secondCard;
    public CardManager cardManager;

    private int totalPairs;
    private int matchedPairs;
    private int turns;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        StartNewGame();
    }

    private void StartNewGame()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cardManager != null)
            {
                cardManager.Initilize();
                totalPairs = cardManager.totalCards / 2;
                matchedPairs = 0;
            }
        }
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
        turns++;
        Card fCard = firstCard;
        Card sCard = secondCard;

        firstCard = null;
        secondCard = null;

        if (fCard.GetCardData().cardNumber == sCard.GetCardData().cardNumber)
        {
            yield return new WaitForSeconds(.5f);
            Matched(fCard, sCard);
        }
        else
        {
            Mismatched(fCard, sCard);
        }
    }
    private void Mismatched(Card fCard, Card sCard)
    {
        fCard.HideCardFace();
        sCard.HideCardFace();
    }
    private void Matched(Card fCard, Card sCard)
    {
        fCard.gameObject.SetActive(false);
        sCard.gameObject.SetActive(false);

        matchedPairs++;
        if(matchedPairs == totalPairs)
        {
            Debug.Log("hello game");
        }
    }
}