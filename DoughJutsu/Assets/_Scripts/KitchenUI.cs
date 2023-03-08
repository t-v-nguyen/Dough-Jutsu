using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        foreach (Item item in Pantry.PantryItems)
        {
            if (item.itemType == Item.ItemType.ingredient) contents += item.itemName + "\n";
        }
        return contents;
    }
    public void GetRecipes()
    {
        float i = .5f;
        float buttonSpacing = 5f;
        foreach (Item item in Pantry.PantryItems)
        {
            if (item.itemType == Item.ItemType.recipe)
            {
                GameObject recipeItem = Instantiate(recipePrefab, recipeText.transform);
                RectTransform buttonRectTransform = recipeItem.GetComponent<RectTransform>();
                buttonRectTransform.anchoredPosition = new Vector2(0, -i * (buttonRectTransform.rect.height + buttonSpacing));
                Transform textChild = recipeItem.transform.Find("RecipeName");
                textChild.GetComponent<TMP_Text>().text = item.identified ? item.name : item.itemName;

                Transform inspectChild = recipeItem.transform.Find("Inspect");
                inspectChild.GetComponent<Button>().onClick.AddListener(() =>
                {
                    InspectClick(item);
                });

                Transform bakeChild = recipeItem.transform.Find("Bake");
                bakeChild.GetComponent<Button>().onClick.AddListener(() =>
                {
                    bool success = FollowRecipe(item);
                    if (success)
                    {
                        textChild.GetComponent<TMP_Text>().text = item.identified ? item.name : item.itemName;
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

    public void InspectClick(Item item)
    {
        instructionPanel.SetActive(true);
        string displayText = item.recipe.Replace(",", "\n");
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

    public bool FollowRecipe(Item recipe)
    {
        int counter = 0;
        bool butter = false;
        bool flour = false;
        bool milk = false;
        bool sugar = false;
        bool water = false;
        bool yeast = false;
        foreach (Item item in Pantry.PantryItems)
        {
            if (item.itemName == "butter")
            {
                butter = true;
            }
            if (item.itemName == "flour")
            {
                flour = true;
            }
            if (item.itemName == "milk")
            {
                milk = true;
            }
            if (item.itemName == "sugar")
            {
                sugar = true;
            }
            if (item.itemName == "water")
            {
                water = true;
            }
            if (item.itemName == "yeast")
            {
                yeast = true;
            }
            counter += 1;
        }

        if (butter && flour && milk && sugar && water && yeast)
        {
            for (int i = Pantry.PantryItems.Count - 1; i >= 0; i--)
            {
                Item item = Pantry.PantryItems[i];
                if (item.itemName == "butter" && butter)
                {
                    butter = false;
                    Pantry.PantryItems.RemoveAt(i);
                }
                if (item.itemName == "flour" && flour)
                {
                    flour = false;
                    Pantry.PantryItems.RemoveAt(i);
                }
                if (item.itemName == "milk" && milk)
                {
                    milk = false;
                    Pantry.PantryItems.RemoveAt(i);
                }
                if (item.itemName == "sugar" && sugar)
                {
                    sugar = false;
                    Pantry.PantryItems.RemoveAt(i);
                }
                if (item.itemName == "water" && water)
                {
                    water = false;
                    Pantry.PantryItems.RemoveAt(i);
                }
                if (item.itemName == "yeast" && yeast)
                {
                    yeast = false;
                    Pantry.PantryItems.RemoveAt(i);
                }
            }
            recipe.identified = true;
            return true;
        }
        else return false;
    }
}
