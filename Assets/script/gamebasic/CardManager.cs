using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public CardDragView cardView;
    public List<CardData> library = new List<CardData>();
    public int drawCount = 5;

    private Queue<CardData> hand = new Queue<CardData>();
    private TerritoryData currentTerritory;

    void Awake()
    {
        cardView.OnResolved += ShowNext;
    }

    public void OpenFor(TerritoryData territory)
    {
        currentTerritory = territory;
        hand.Clear();

        List<int> picked = new List<int>();
        int count = Mathf.Min(drawCount, library.Count);

        while (picked.Count < count)
        {
            int index = Random.Range(0, library.Count);
            if (!picked.Contains(index))
            {
                picked.Add(index);
                hand.Enqueue(library[index]);
            }
        }

        ShowNext();
    }

    void ShowNext()
    {
        if (hand.Count > 0)
            cardView.Display(hand.Dequeue(), currentTerritory);
        else
            cardView.HideInstant();
    }
}
