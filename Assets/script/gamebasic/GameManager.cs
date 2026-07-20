using UnityEngine;

public enum GamePhase { Choosing, Resolving, Over }

public class GameManager : MonoBehaviour
{
    public GameConfig config;
    public ResourceManager resources;
    public MapManager map;
    public CardDragView cards;

    public int currentTurn = 1;
    public GamePhase phase = GamePhase.Choosing;

    private int pendingIncomeBonus = 0;

    void Start()
    {
        resources.Setup(config);
        map.Setup();
        BeginTurn();
    }

    void BeginTurn()
    {
        phase = GamePhase.Choosing;
        resources.Add(ResourceType.Troops, config.baseTroopsIncome + pendingIncomeBonus);
        resources.Add(ResourceType.Supplies, config.baseSuppliesIncome);
        pendingIncomeBonus = 0;
    }

    public void ChooseSide(CardEffect effect, TerritoryData territory)
    {
        resources.ApplyEffect(effect);
        if (territory != null && effect.attackPower > 0)
            map.AddAttack(territory, effect.attackPower);
        pendingIncomeBonus += effect.nextTurnIncomeBonus;
    }

   

    void Finish(bool won)
    {
        phase = GamePhase.Over;
    }
}