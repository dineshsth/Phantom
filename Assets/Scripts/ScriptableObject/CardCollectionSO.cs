using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Collection", menuName = "Card Object SO/Card Collection")]
public class CardCollectionSO : ScriptableObject
{
    public List<CardDataSO> cardDataSO;
}