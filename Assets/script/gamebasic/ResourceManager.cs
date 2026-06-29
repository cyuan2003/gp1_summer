using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<ResourceType, int> values = new Dictionary<ResourceType, int>();

    public event Action OnChanged;

    public void Setup(GameConfig config)
    {
        values[ResourceType.Troops] = config.startTroops;
        values[ResourceType.Supplies] = config.startSupplies;
        values[ResourceType.Morale] = config.startMorale;
        OnChanged?.Invoke();
    }

    public int Get(ResourceType type)
    {
        return values[type];
    }

    public void Add(ResourceType type, int amount)
    {
        values[type] += amount;
        OnChanged?.Invoke();
    }

    public void ApplyEffect(CardEffect effect)
    {
        values[ResourceType.Troops] += effect.troopsDelta;
        values[ResourceType.Supplies] += effect.suppliesDelta;
        values[ResourceType.Morale] += effect.moraleDelta;
        OnChanged?.Invoke();
    }

    public bool AnyDepleted()
    {
        return values[ResourceType.Troops] <= 0
            || values[ResourceType.Supplies] <= 0
            || values[ResourceType.Morale] <= 0;
    }
}
