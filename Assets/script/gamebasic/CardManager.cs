using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public CanvasGroup panel;
    public CardDragView cardView;
    public List<CardData> library = new List<CardData>();
    public int drawCount = 5;

    private Queue<CardData> hand = new Queue<CardData>();
    private TerritoryData currentTerritory;
    private HashSet<TerritoryData> usedThisTurn = new HashSet<TerritoryData>();

    void Awake()
    {
        cardView.OnResolved += ShowNext;
    }

    void Start()
    {
        ClosePanel();
    }

    public bool CanDrawFor(TerritoryData territory)
    {
        return !usedThisTurn.Contains(territory);
    }

    public void OpenFor(TerritoryData territory)
    {
        if (usedThisTurn.Contains(territory)) return;
        usedThisTurn.Add(territory);

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

        OpenPanel();
        ShowNext();
    }

    public void ResetTurn()
    {
        usedThisTurn.Clear();
    }

    void ShowNext()
    {
        if (hand.Count > 0)
        {
            cardView.Display(hand.Dequeue(), currentTerritory);
        }
        else
        {
            cardView.HideInstant();
            ClosePanel();
        }
    }

    void OpenPanel()
    {
        panel.alpha = 1f;
        panel.interactable = true;
        panel.blocksRaycasts = true;
    }

    void ClosePanel()
    {
        panel.alpha = 0f;
        panel.interactable = false;
        panel.blocksRaycasts = false;
    }
}