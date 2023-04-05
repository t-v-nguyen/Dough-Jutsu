using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pantry
{
    public static List<Item> PantryItems {get; set;}
    public static List<Key> Keys {get; set;}
    static Pantry()
    {
        PantryItems = new List<Item>();
        Keys = new List<Key>();
    }
}
