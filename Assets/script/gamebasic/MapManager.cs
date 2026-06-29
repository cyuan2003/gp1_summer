using System.Collections.Generic;
using UnityEngine;

public enum TerritoryStatus { Untouched, Attacking, Captured, Lost }

public class TerritoryState
{
    public TerritoryData data;
    public TerritoryStatus status = TerritoryStatus.Untouched;
    public int currentAttack = 0;

    public TerritoryState(TerritoryData data)
    {
        this.data = data;
    }

    public float Progress
    {
        get
        {
            if (data.defenseValue <= 0) return 0f;
            return Mathf.Clamp01((float)currentAttack / data.defenseValue);
        }
    }

    public bool IsCaptured
    {
        get { return status == TerritoryStatus.Captured; }
    }
}

public class MapManager : MonoBehaviour
{
    public List<TerritoryData> allTerritories;
    public TerritoryData startTerritory;
    public int raidProgressLoss = 10;
    public int raidResourceLoss = 5;

    private Dictionary<TerritoryData, TerritoryState> states = new Dictionary<TerritoryData, TerritoryState>();

    public void Setup()
    {
        states.Clear();
        foreach (var t in allTerritories)
            states[t] = new TerritoryState(t);
        states[startTerritory].status = TerritoryStatus.Captured;
    }

    public TerritoryState GetState(TerritoryData data)
    {
        return states[data];
    }

    public bool CanStartAttack(TerritoryData target, int currentTroops)
    {
        var state = states[target];
        bool free = state.status == TerritoryStatus.Untouched || state.status == TerritoryStatus.Lost;
        if (!free) return false;
        if (currentTroops < target.startThreshold) return false;
        return IsNextToCaptured(target);
    }

    private bool IsNextToCaptured(TerritoryData target)
    {
        foreach (var neighbor in target.neighbors)
            if (states[neighbor].IsCaptured)
                return true;
        return false;
    }

    public void StartAttack(TerritoryData target)
    {
        states[target].status = TerritoryStatus.Attacking;
    }

    public void AddAttack(TerritoryData target, int amount)
    {
        var state = states[target];
        if (state.status != TerritoryStatus.Attacking) return;

        state.currentAttack += amount;
        if (state.currentAttack >= state.data.defenseValue)
        {
            state.currentAttack = state.data.defenseValue;
            state.status = TerritoryStatus.Captured;
        }
    }

    public List<TerritoryState> GetAttacking()
    {
        var list = new List<TerritoryState>();
        foreach (var pair in states)
            if (pair.Value.status == TerritoryStatus.Attacking)
                list.Add(pair.Value);
        return list;
    }

    public void ResolveRaids(ResourceManager resources)
    {
        foreach (var pair in states)
        {
            var state = pair.Value;
            bool exposed = state.status == TerritoryStatus.Attacking || state.status == TerritoryStatus.Captured;
            if (!exposed) continue;

            float chance = Random.Range(state.data.raidChanceMin, state.data.raidChanceMax);
            if (Random.value < chance)
                ApplyRaid(state, resources);
        }
    }

    private void ApplyRaid(TerritoryState state, ResourceManager resources)
    {
        state.currentAttack = Mathf.Max(0, state.currentAttack - raidProgressLoss);
        ResourceType hit = (ResourceType)Random.Range(0, 3);
        resources.Add(hit, -raidResourceLoss);
    }

    public bool AllCaptured()
    {
        foreach (var pair in states)
            if (!pair.Value.IsCaptured)
                return false;
        return true;
    }
}
