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
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if(inventory[0])
            {
                inventory.RemoveAt(0);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)) {
        }
        if (Input.GetKeyDown(KeyCode.Alpha7)) {
        }
        if (Input.GetKeyDown(KeyCode.Alpha8)) {
        }
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
