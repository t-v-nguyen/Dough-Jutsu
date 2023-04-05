using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    public float delay = 2f;
    public float toggleInterval = 5f;

    private GameObject laserObject;
    // Start is called before the first frame update
    void Start()
    {
        laserObject = gameObject.transform.GetChild(0).gameObject;
        StartCoroutine(ToggleObjectCoroutine());
    }

    // Update is called once per frame
    private IEnumerator ToggleObjectCoroutine()
    {
        yield return new WaitForSeconds(delay);
        while (true)
        {
            yield return new WaitForSeconds(toggleInterval);
            laserObject.SetActive(true);

            yield return new WaitForSeconds(toggleInterval);
            laserObject.SetActive(false);
        }
    }
}
