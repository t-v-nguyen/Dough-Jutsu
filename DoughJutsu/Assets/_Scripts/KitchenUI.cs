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
    public GameObject instructionPanel;
    public GameObject failPanel;

    // Start is called before the first frame update
    void Start()
    {

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
        Dictionary<Item, int> inventory = new Dictionary<Item, int>();
        foreach (Item item in Pantry.PantryItems)
        {
            if (!item.isRecipe)
            {
                int quantity;
                if(inventory.TryGetValue(item, out quantity))
                {
                    inventory[item] = quantity + 1;
                }
                else
                {
                    inventory[item] = 1;
                }
            }
        }
        foreach(KeyValuePair<Item, int> kvp in inventory)
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
                Recipe recipe = (Recipe) item;
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

                Transform bakeChild = recipeItem.transform.Find("Bake");
                bakeChild.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (FollowRecipe(recipe))
                    {
                        textChild.GetComponent<TMP_Text>().text = recipe.identified ? recipe.itemName : "Unknown recipe";
                        pantryText.text = GetPantryContents();
                    }
                    else
                    {
                        failPanel.SetActive(true);
                    }
                });

                i += 1;
            }
        }
    }

    public void InspectClick(Recipe recipe)
    {
        instructionPanel.SetActive(true);
        string displayText = "";
        for(int i=0; i<recipe.recipleIngredient.Count; i++)
        {
            displayText += recipe.recipleIngredient[i].itemName + ": " + recipe.recipeAmount[i].ToString() + "\n";
        }
        instructionPanel.transform.Find("ingredients").GetComponent<TMP_Text>().text = displayText;
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

        return true;
    }
}
