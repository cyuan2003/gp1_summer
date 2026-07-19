using System.Collections.Generic;
using UnityEngine;

public enum ResourceType { Troops, Supplies, Morale, Spirit }
public enum CardKind { Request, Raid }

[System.Serializable]
public struct CardEffect
{
    public string label;
    public int troopsDelta;
    public int suppliesDelta;
    public int moraleDelta;
    public int spiritDelta;
    public int attackPower;
    public int nextTurnIncomeBonus;
}

[CreateAssetMenu(fileName = "NewCard", menuName = "WarRoom/Card")]
public class CardData : ScriptableObject
{
    public CardKind kind = CardKind.Request;
    public string sourceName;
    [TextArea] public string promptText;
    public Sprite image;
    public CardEffect leftChoice;
    public CardEffect rightChoice;
    [Range(0f, 1f)] public float baseSuccessChance = 1f;
}

[CreateAssetMenu(fileName = "NewTerritory", menuName = "WarRoom/Territory")]
public class TerritoryData : ScriptableObject
{
    public string territoryName;
    [TextArea] public string[] introLines;
    public int defenseValue = 30;
    public int startThreshold = 10;
    [Range(0f, 1f)] public float raidChanceMin = 0.15f;
    [Range(0f, 1f)] public float raidChanceMax = 0.30f;
    public List<TerritoryData> neighbors = new List<TerritoryData>();
}

[CreateAssetMenu(fileName = "GameConfig", menuName = "WarRoom/Game Config")]
public class GameConfig : ScriptableObject
{
    public int maxTurns = 25;
    public int startTroops = 20;
    public int startSupplies = 20;
    public int startMorale = 50;
    public int startSpirit = 50;
    public int baseTroopsIncome = 5;
    public int baseSuppliesIncome = 5;
    public int cardsPerTerritoryMin = 3;
    public int cardsPerTerritoryMax = 5;
}