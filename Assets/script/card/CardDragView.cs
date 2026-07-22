using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class CardDragView : MonoBehaviour
{
    public GameManager game;
    public RectTransform card;
    public CanvasGroup group;
    public Image artwork;
    public TMP_Text sourceText;
    public TMP_Text promptText;
    public TMP_Text leftText;
    public TMP_Text rightText;
    public float threshold = 200f;

    public event Action OnResolved;

    public CardData data;
    private TerritoryData territory;
    private Vector2 startPos;
    public Image attackPower, supply, troop, morale;
    public List<CardData> cards;
    CanvasGroup canvasGroup;
    void Start()
    {
        canvasGroup=GetComponentInParent<CanvasGroup>();
        startPos = card.anchoredPosition;
        HideInstant();
        data = cards[Random.Range(0, cards.Count-1)];
        GetComponent<Image>().sprite = data.image;

    }

    public void NextCard()
    {
        cards.Remove(data);
        if (cards.Count != 0)
        {
            data = cards[Random.Range(0, cards.Count - 1)];
            GetComponent<Image>().sprite = data.image;
        }
        else
        {
            FindObjectOfType<CardManager>().ClosePanel();
        }
        

    }
    private void Update()
    {
        int width = Screen.width;
        attackPower.gameObject.SetActive(false);
        supply.gameObject.SetActive(false);
        troop.gameObject.SetActive(false);
        morale.gameObject.SetActive(false);

        float realMousePos = Input.mousePosition.x - width/2;
        //print(realMousePos);

        
        if (realMousePos >100&&canvasGroup.alpha!=0)//”“±ﬂ
        {
            transform.localEulerAngles = new Vector3(0, 0, -realMousePos/ 30);
            LoadRightCardData();
            print("right");
            if(Input.GetMouseButtonDown(0)&&canvasGroup.alpha!=0)
            {
                NextCard();
            }
        }
        else if (realMousePos < - 100&& canvasGroup.alpha != 0)//◊Û±ﬂ
        {
            transform.localEulerAngles = new Vector3(0, 0, -realMousePos / 30);
            LoadLeftCardData();
            print("left");
            if (Input.GetMouseButtonDown(0)&&canvasGroup.alpha != 0)
            {
                NextCard();
            }
        }
        else
        {
            transform.localEulerAngles = Vector3.zero;
        }
    }

    private void LoadLeftCardData()
    {
        if (data.leftChoice.moraleDelta != 0)
            morale.gameObject.SetActive(true);
        if (data.leftChoice.suppliesDelta != 0)
            supply.gameObject.SetActive(true);
        if (data.leftChoice.troopsDelta != 0)
            troop.gameObject.SetActive(true);
        if (data.leftChoice.attackPower != 0)
            attackPower.gameObject.SetActive(true);
    }
    private void LoadRightCardData()
    {
        if (data.rightChoice.moraleDelta != 0)
            morale.gameObject.SetActive(true);
        if (data.rightChoice.suppliesDelta != 0)
            supply.gameObject.SetActive(true);
        if (data.rightChoice.troopsDelta != 0)
            troop.gameObject.SetActive(true);
        if (data.rightChoice.attackPower != 0)
            attackPower.gameObject.SetActive(true);
    }

    public void Display(CardData cardData, TerritoryData terr)
    {
        data = cardData;
        territory = terr;
        sourceText.text = cardData.sourceName;
        promptText.text = cardData.promptText;
        leftText.text = cardData.leftChoice.label;
        rightText.text = cardData.rightChoice.label;

        if (artwork != null)
            artwork.sprite = cardData.image;

        card.anchoredPosition = startPos;
        group.alpha = 1f;
        group.interactable = true;
        group.blocksRaycasts = true;
    }

    public void HideInstant()
    {
        group.alpha = 0f;
        group.interactable = false;
        group.blocksRaycasts = false;
    }

   

    void Choose(CardEffect effect)
    {
        game.ChooseSide(effect, territory);
        card.anchoredPosition = startPos;
        OnResolved?.Invoke();
    }
}
