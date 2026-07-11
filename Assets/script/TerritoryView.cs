using UnityEngine;

public class TerritoryView : MonoBehaviour
{
    public TerritoryData data;
    public MapManager map;
    public GameManager game;
    public Renderer body;
    public CardManager cardManager;

    public Color untouchedColor = Color.gray;
    public Color attackingColor = new Color(0.78f, 0.6f, 0.2f);
    public Color capturedColor = new Color(0.23f, 0.39f, 0.57f);

    public void OnPicked()
    {
        var state = map.GetState(data);

        if (state.status == TerritoryStatus.Attacking)
        {
            cardManager.OpenFor(data);
            return;
        }

        int troops = game.resources.Get(ResourceType.Troops);
        if (map.CanStartAttack(data, troops))
        {
            map.StartAttack(data);
            Refresh();
        }
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