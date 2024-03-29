using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExtractPoint : MonoBehaviour
{
    private GameManager gm;
    private void Start()
    {
        gm = GameManager.instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach(Item item in gm.inventory)
            {
                if(!item.isKey) Pantry.PantryItems.Add(item);
                else Pantry.Keys.Add((Key)item);
            }
            SceneManager.LoadScene("ModeMenu");
        }
    }
}
