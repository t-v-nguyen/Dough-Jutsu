using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    public List<Item> inventory;
    public GameObject inventoryUI;

    public void Update()
    {
    }

    public void UpdateInventory()
    {
        for (int i=1; i<=inventory.Count; i++)
        {
            Transform childTransform = inventoryUI.transform.Find("slot"+i);

            if (childTransform != null)
            {
                GameObject childGameObject = childTransform.gameObject;
                childGameObject.GetComponent<TMP_Text>().text = inventory[i-1].name;
            }
        }
    }
}
