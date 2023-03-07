using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public List<Drop> table;

    [NonSerialized]
    int totalWeight = 0;

    [Serializable]
    public class Drop
    {
        public Item drop;
        public int weight;
    }

    private int CalculateTotalWeight()
    {
        totalWeight = 0;
        foreach(Drop item in table)
        {
            totalWeight += item.weight;
        }
        return totalWeight;
    }
    public Item GetDrop()
    {
        int roll = UnityEngine.Random.Range(0, CalculateTotalWeight());
        for(int i=0; i < table.Count; i++)
        {
            roll -= table[i].weight;
            if(roll < 0)
            {
                return table[i].drop;
            }
        }
        return table[0].drop;
    }
}
