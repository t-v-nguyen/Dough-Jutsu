using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject loadingBar;
    public Image fillBar;
    public List<Item> Inventory;
    [SerializeField] LootTable lootTable;


    private void Awake()
{
    if (instance == null)
    {
        instance = this;
    }
    else
    {
        Destroy(gameObject);
    }
}
}
