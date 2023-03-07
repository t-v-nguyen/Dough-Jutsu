using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    private GameManager gm;
    public float fillTime = 1f;
    public float progress = 0f;
    private float timer = 0f;
    private bool isPlayerOnObject = false;
    [SerializeField] LootTable lootTable;

    private void Start()
    {
        gm = GameManager.instance;
        progress = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnObject = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnObject = false;
            gm.loadingBar.SetActive(false);
            timer = 0f;
            progress = 0f;
            gm.fillBar.fillAmount = 0f;
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.F) && isPlayerOnObject)
        {
            gm.loadingBar.SetActive(true);
            timer += Time.deltaTime;
            progress = timer / fillTime;
            gm.fillBar.fillAmount = progress;
        }
        else if(Input.GetKeyUp(KeyCode.F) && progress < 1f)
        {
            gm.loadingBar.SetActive(false);
            timer = 0f;
            progress = 0f;
            gm.fillBar.fillAmount = 0f;
        }

        if (progress >= 1f)
        {
            gm.loadingBar.SetActive(false);
            timer = 0f;
            progress = 0f;
            gm.fillBar.fillAmount = 0f;
            gm.inventory.Add(lootTable.GetDrop());
            gm.UpdateInventory();
            gameObject.SetActive(false);
        }
    }
}