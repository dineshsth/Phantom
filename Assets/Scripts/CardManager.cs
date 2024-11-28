using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public CardLayoutManager cardLayoutManager;
    public List<Card> cards = new();
    public Card cardPrefab;

    public int rows = 5;
    public int columns = 5;

    private void Start()
    {
        GenerateCards();
    }

    private void GenerateCards()
    {
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
            cards.Add(newCard);
        }
    }
}
