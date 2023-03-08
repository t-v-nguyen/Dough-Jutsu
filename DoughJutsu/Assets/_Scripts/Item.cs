using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string itemName;
    public enum ItemType 
    {
        ingredient,
        recipe
    }

    [SerializeField]
    public ItemType itemType;

    public string recipe;
    public bool identified;
}
