using UnityEngine;
using UnityEngine.UI;

public class TerritoryView : MonoBehaviour
{
    public TerritoryData data;
    public MapManager map;
    public GameManager game;
    public Renderer body;
    public CardManager cardManager;
    public TextSequence introDialog;

    private bool introShownForThis = false;

    public Color untouchedColor = Color.gray;
    public Color attackingColor = new Color(0.78f, 0.6f, 0.2f);
    public Color capturedColor = new Color(0.23f, 0.39f, 0.57f);

    public Button button;
    public GameObject challengePanel;



    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            challengePanel.SetActive(false);
            cardManager.OpenFor(data);

        });
    }
    public void OnPicked()
    {
        var state = map.GetState(data);

        if (state.status == TerritoryStatus.Attacking)
        {
            //cardManager.OpenFor(data);
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                challengePanel.SetActive(false);
                cardManager.OpenFor(data);

            });
            challengePanel.gameObject.SetActive(true);
            return;
        }

        int troops = game.resources.Get(ResourceType.Troops);
        if (!map.CanStartAttack(data, troops)) return;

        if (!introShownForThis && introDialog != null && data.introLines.Length > 0)
        {
            introShownForThis = true;
            introDialog.onFinished.AddListener(StartAttackNow);
            introDialog.BeginWith(data.introLines);
            return;
        }

        StartAttackNow();
    }

    void StartAttackNow()
    {
        introDialog.onFinished.RemoveListener(StartAttackNow);
        map.StartAttack(data);
        Refresh();
    }

    public void Refresh()
    {
        var state = map.GetState(data);
        switch (state.status)
        {
            case TerritoryStatus.Untouched: body.material.color = untouchedColor; break;
            case TerritoryStatus.Attacking: body.material.color = attackingColor; break;
            case TerritoryStatus.Captured: body.material.color = capturedColor; break;
            case TerritoryStatus.Lost: body.material.color = untouchedColor; break;
        }
    }
}