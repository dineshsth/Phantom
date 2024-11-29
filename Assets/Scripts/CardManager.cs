using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public CardLayoutManager cardLayoutManager;
    public CardCollectionSO cardCollectionSO;

    public List<Card> cards = new();
    public Card cardPrefab;

    public int rows = 5;
    public int columns = 5;
    public int totalCards;
    public float cardRotationDuration = 1f;

    public List<int> selectedNumbers = new();
    public List<int> pairedCardIndices = new();

    static readonly System.Random random = new();


    public void Initilize()
    {
        totalCards = rows * columns;

        List<int> tempPairs = GeneratePairData();
        SelectRandomPair(tempPairs);
        ShufflePair(selectedNumbers);
        GenerateCards();
    }

    private List<int> GeneratePairData()
    {
        List<int> pairData = new();
        for (int i = 0; i < totalCards / 2; i++)
        {
            pairData.Add(i);
        }

        return pairData;
    }
    private void SelectRandomPair(List<int> list)
    {
        for (int i = 0; i < totalCards / 2; i++)
        {
            int index = random.Next(list.Count);
            int randNumber = list[index];
            selectedNumbers.Add(randNumber);
            selectedNumbers.Add(randNumber);
            list.RemoveAt(index);
        }
    }
    private void ShufflePair(List<int> list)
    {
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            (list[j], list[i]) = (list[i], list[j]);
        }
    }

    private void GenerateCards()
    {
        cardLayoutManager.enabled = true;

        cards.Clear();
        foreach (Transform child in cardLayoutManager.transform)
        {
            Destroy(child.gameObject);
        }

        cardLayoutManager.rows = rows;
        cardLayoutManager.columns = columns;
        for (int i = 0; i < rows * columns; i++)
        {
            Card newCard = Instantiate(cardPrefab, cardLayoutManager.transform);
            newCard.gameObject.name = cardCollectionSO.cardDataSO[selectedNumbers[i]].cardName;
            newCard.Initilize(cardCollectionSO.cardDataSO[selectedNumbers[i]], cardRotationDuration, i);
            cards.Add(newCard);
        }

        Invoke(nameof(SetGridOff), .2f);
    }

    private void SetGridOff()
    {
        cardLayoutManager.enabled = false;
        DisableCardOnLoad();
    }

    private void DisableCardOnLoad()
    {
        if (pairedCardIndices.Count > 0)
        {
            for (int i = 0; i < pairedCardIndices.Count; i++)
            {
                cards[pairedCardIndices[i]].gameObject.SetActive(false);
            }
        }
    }

    #region save and laod
    public void Save(ref CardManagerSaveData data)
    {
        data.rows = rows;
        data.columns = columns;
        data.selectedNumbers = selectedNumbers.ToArray();
        data.pairedCardIndex = pairedCardIndices.ToArray();
    }
    public void Load(CardManagerSaveData data)
    {
        rows = data.rows; columns = data.columns;
        totalCards = rows * columns;
        selectedNumbers = data.selectedNumbers.ToList();
        pairedCardIndices = data.pairedCardIndex.ToList();

        GenerateCards();
    }
    #endregion
}

[System.Serializable]
public struct CardManagerSaveData
{
    public int rows;
    public int columns;

    public int[] selectedNumbers;
    public int[] pairedCardIndex;
}