using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pantry
{
    public static List<Item> PantryItems {get; set;}
    static Pantry()
    {
        PantryItems = new List<Item>();
    }
    
    
}
