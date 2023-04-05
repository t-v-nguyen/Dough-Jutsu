using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class KitchenUI : MonoBehaviour
{
    public TMP_Text pantryText;
    public GameObject recipeText;
    public GameObject recipePrefab;
    public List<Item> presetPantryList;
    public Recipe presetRecipe;
    public GameObject instructionPanel;
    public GameObject failPanel;
    public GameObject bakeObject;
    public Button bakeButton;
    Dictionary<Ingredient, int> inventory = new Dictionary<Ingredient, int>();

    // Start is called before the first frame update
    void Start()
    {
        Pantry.PantryItems = presetPantryList;
        Pantry.PantryItems.Add(presetRecipe);
        if (pantryText != null)
        {
            pantryText.text = GetPantryContents();
            GetRecipes();
        }
        instructionPanel.SetActive(false);
        failPanel.SetActive(false);
    }

    public string GetPantryContents()
    {
        string contents = "Pantry:\n";
        
        foreach (Item item in Pantry.PantryItems)
        {
            Debug.Log(item);
            Ingredient ingredient = item as Ingredient;
            if (!item.isRecipe)
            {
                int quantity;
                if (inventory.TryGetValue(ingredient, out quantity))
                {
                    inventory[ingredient] = quantity + 1;
                }
                else
                {
                    inventory[ingredient] = 1;
                }
            }
        }
        foreach (KeyValuePair<Ingredient, int> kvp in inventory)
        {
            contents += $"{kvp.Key.itemName}: {kvp.Value} \n";
        }
        return contents;
    }
    public void GetRecipes()
    {
        float i = .5f;
        float buttonSpacing = 5f;
        foreach (Item item in Pantry.PantryItems)
        {
            if (item.isRecipe)
            {
                Recipe recipe = (Recipe)item;
                GameObject recipeItem = Instantiate(recipePrefab, recipeText.transform);
                RectTransform buttonRectTransform = recipeItem.GetComponent<RectTransform>();
                buttonRectTransform.anchoredPosition = new Vector2(0, -i * (buttonRectTransform.rect.height + buttonSpacing));
                Transform textChild = recipeItem.transform.Find("RecipeName");
                textChild.GetComponent<TMP_Text>().text = recipe.identified ? recipe.itemName : "Unknown recipe";

                Transform inspectChild = recipeItem.transform.Find("Inspect");
                inspectChild.GetComponent<Button>().onClick.AddListener(() =>
                {
                    InspectClick(recipe);
                });

                i += 1;
            }
        }
    }

    public void InspectClick(Recipe recipe)
    {
        instructionPanel.SetActive(true);
        string displayText = "";
        for (int i = 0; i < recipe.recipleIngredient.Count; i++)
        {
            displayText += recipe.recipleIngredient[i].itemName + ": " + recipe.recipeAmount[i].ToString() + "\n";
        }
        instructionPanel.transform.Find("ingredients").GetComponent<TMP_Text>().text = displayText;
        if (recipe.identified)
        {
            instructionPanel.transform.Find("Grade").GetComponent<TMP_Text>().text = "Grade: " + recipe.grade;
        }
        else
        {
            instructionPanel.transform.Find("Grade").GetComponent<TMP_Text>().text = "Grade: U";
        }

        bakeButton.onClick.RemoveAllListeners();
        bakeButton.onClick.AddListener(() => BakeRecipe(recipe));
    }

    public void CloseInstruction()
    {
        instructionPanel.SetActive(false);
    }
    public void CloseFail()
    {
        failPanel.SetActive(false);
    }

    public bool FollowRecipe(Recipe recipe)
    {
        bool sufficientIngredients = true;
        for (int i = 0; i < recipe.recipleIngredient.Count; i++)
        {
            Ingredient ingredient = recipe.recipleIngredient[i];
            int requiredAmount = recipe.recipeAmount[i];

            if (inventory.ContainsKey(ingredient))
            {
                int playerAmount = inventory[ingredient];
                if (playerAmount < requiredAmount)
                {
                    // Player does not have enough of this ingredient
                    sufficientIngredients = false;
                    break;
                }
                else
                {
                    // Player has enough of this ingredient

                }
            }
            else
            {
                // Player does not have this ingredient
                sufficientIngredients = false;
                break;
            }
        }
        return sufficientIngredients;
    }

    public void BakeRecipe(Recipe recipe)
    {
        if (FollowRecipe(recipe))
        {
            RemoveIngredients(recipe);

            recipe.identified = true;
            instructionPanel.transform.Find("Grade").GetComponent<TMP_Text>().text = "Grade: " + recipe.grade;
            foreach (Transform child in recipeText.transform)
            {
                Destroy(child.gameObject);
            }
            GetRecipes();
            pantryText.text = GetPantryContents();
        }
        else
        {
            failPanel.SetActive(true);
        }
    }

    public void RemoveIngredients(Recipe recipe)
{
    for(int i=0; i < recipe.recipleIngredient.Count; i++)
    {
        inventory[recipe.recipleIngredient[i]] -= recipe.recipeAmount[i];
    }
}
}
