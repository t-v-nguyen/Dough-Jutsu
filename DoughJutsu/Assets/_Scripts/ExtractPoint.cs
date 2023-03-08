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
                Pantry.PantryItems.Add(item);
            }
            SceneManager.LoadScene("ModeMenu");
        }
    }
}
