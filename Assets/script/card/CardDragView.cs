using UnityEngine;
using TMPro;

public class CardDragView : MonoBehaviour
{
    public GameManager game;
    public RectTransform card;
    public CanvasGroup group;
    public TMP_Text sourceText;
    public TMP_Text promptText;
    public TMP_Text leftText;
    public TMP_Text rightText;
    public float threshold = 200f;

    private CardData data;
    private TerritoryData territory;
    private Vector2 startPos;
    private bool dragging = false;

    void Start()
    {
        startPos = card.anchoredPosition;
        Hide();
    }

    public void Show(CardData cardData, TerritoryData terr)
    {
        data = cardData;
        territory = terr;
        sourceText.text = cardData.sourceName;
        promptText.text = cardData.promptText;
        leftText.text = cardData.leftChoice.label;
        rightText.text = cardData.rightChoice.label;
        card.anchoredPosition = startPos;
        group.alpha = 1f;
        group.interactable = true;
        group.blocksRaycasts = true;
        Debug.Log("Show called");
    }

    void Hide()
    {
        group.alpha = 0f;
        group.interactable = false;
        group.blocksRaycasts = false;
    }

    public void OnBeginDrag()
    {
        if (group.alpha < 1f) return;
        dragging = true;
    }

    public void OnDrag()
    {
        if (!dragging) return;
        card.anchoredPosition = new Vector2(Input.mousePosition.x - Screen.width / 2f, startPos.y);
    }

    public void OnEndDrag()
    {
        if (!dragging) return;
        dragging = false;

        float offset = card.anchoredPosition.x - startPos.x;

        if (offset <= -threshold)
            Choose(data.leftChoice);
        else if (offset >= threshold)
            Choose(data.rightChoice);
        else
            card.anchoredPosition = startPos;
    }

    void Choose(CardEffect effect)
    {
        game.ChooseSide(effect, territory);
        card.anchoredPosition = startPos;
        Hide();
    }
}