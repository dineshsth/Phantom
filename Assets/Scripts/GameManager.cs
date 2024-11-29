using System.Collections;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Card firstCard, secondCard;
    public CardManager cardManager;

    private int totalPairs;
    private int matchedPairs;
    private int turns;

    public event Action OnCardFlip;
    public event Action OnMatch;
    public event Action OnMismatch;

    private void Awake()
    {
        instance = this;
    }

    public void StartNewGame()
    {
        if (cardManager != null)
        {
            cardManager.Initilize();
            totalPairs = cardManager.totalCards / 2;
            matchedPairs = 0;
        }
    }

    public void PlayFlipCardSound() => OnCardFlip?.Invoke();
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
            OnMatch?.Invoke();
            yield return new WaitForSeconds(.5f);
            Matched(fCard, sCard);
        }
        else
        {
            OnMismatch?.Invoke();
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
        if (matchedPairs == totalPairs)
        {
            Debug.Log("hello game");
        }
    }
}