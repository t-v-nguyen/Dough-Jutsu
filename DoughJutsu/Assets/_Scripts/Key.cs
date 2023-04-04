using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class Key : Item
{
    Key()
    {
        isRecipe = false;
        isKey = true;
    }
    public enum Color 
    {
        red, blue, green
    }

    public Color color;
}