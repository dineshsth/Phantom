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

    public event Action OnGameComplete;
    public event Action<int> OnTurnUpdated;
    public event Action<int> OnMatchUpdated;

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

    public void SaveGame()
    {
        SaveSystem.Save();
    }
    public void LoadGame()
    {
        SaveSystem.Load();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
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
        OnTurnUpdated?.Invoke(turns);

        Card fCard = firstCard;
        Card sCard = secondCard;

        firstCard = null;
        secondCard = null;

        if (fCard.GetCardData().cardNumber == sCard.GetCardData().cardNumber)
        {
            OnMatch?.Invoke();
            matchedPairs++;
            OnMatchUpdated?.Invoke(matchedPairs);

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


        if (matchedPairs == totalPairs)
            OnGameComplete?.Invoke();
    }


    #region Save and load

    public void Save(ref GameSaveData data)
    {
        data.totalPairs = totalPairs;
        data.matchedPairs = matchedPairs;
        data.turns = turns;
    }
    public void Load(GameSaveData data)
    {
        totalPairs = data.totalPairs;
        matchedPairs = data.matchedPairs; OnMatchUpdated?.Invoke(matchedPairs);
        turns = data.turns; OnTurnUpdated?.Invoke(turns);
    }

    #endregion
}


[System.Serializable]
public struct GameSaveData
{
    public int totalPairs;
    public int matchedPairs;
    public int turns;
}