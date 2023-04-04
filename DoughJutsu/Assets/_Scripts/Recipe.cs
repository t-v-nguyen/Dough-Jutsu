using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class Recipe : Item
{
    Recipe()
    {
        isRecipe = true;
    }
    public bool identified;
    public enum Grade 
    {
        S, A, B, C, D, U

    }
    public List<Ingredient> recipleIngredient;
    public List<int> recipeAmount;
    public Grade grade;
}