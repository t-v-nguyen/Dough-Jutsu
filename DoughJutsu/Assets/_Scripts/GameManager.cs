using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject loadingBar;
    public Image fillBar;
    public List<Item> inventory;
    public GameObject inventoryUI;
    public float timeRemaining = 60;
    public TMP_Text timeText;
    public GameObject displayLose;


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

    private void Start()
    {
        loadingBar.SetActive(false);
        displayLose.SetActive(false);
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimeText();
        }
        else
        {
            displayLose.SetActive(true);
            Time.timeScale = 0f;
            Invoke("PlayerLoses", 2f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && inventory.Count > 0)
        {
            inventory.RemoveAt(0);
            UpdateInventory();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && inventory.Count > 1)
        {
            inventory.RemoveAt(1);
            UpdateInventory();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && inventory.Count > 2)
        {
            inventory.RemoveAt(2);
            UpdateInventory();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && inventory.Count > 3)
        {
            inventory.RemoveAt(3);
            UpdateInventory();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && inventory.Count > 4)
        {
            inventory.RemoveAt(4);
            UpdateInventory();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && inventory.Count > 5)
        {
            inventory.RemoveAt(5);
            UpdateInventory();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) && inventory.Count > 6)
        {
            inventory.RemoveAt(6);
            UpdateInventory();
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) && inventory.Count > 7)
        {
            inventory.RemoveAt(7);
            UpdateInventory();
        }
    }
    public void UpdateInventory()
    {
        if (inventory.Count > 0)
        {
            for (int i = 1; i <= inventory.Count; i++)
            {
                Transform childTransform = inventoryUI.transform.Find("slot" + i);

                if (childTransform != null)
                {
                    GameObject childGameObject = childTransform.gameObject;
                    childGameObject.GetComponent<TMP_Text>().text = inventory[i - 1].itemName;
                }
            }
        }
        for(int i=inventory.Count; i<8; i++)
            {
                Transform childTransform = inventoryUI.transform.Find("slot" + (i+1));

                if (childTransform != null)
                {
                    GameObject childGameObject = childTransform.gameObject;
                    childGameObject.GetComponent<TMP_Text>().text = "slot " + (i+1);
                }
            }
    }

    private void UpdateTimeText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void PlayerLoses()
    {
        SceneManager.LoadScene("ModeMenu");
        Time.timeScale = 1f;
    }
}
