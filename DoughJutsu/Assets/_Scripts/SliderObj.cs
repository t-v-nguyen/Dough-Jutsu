using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderObj : MonoBehaviour
{
    public TMP_Text textComp;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        Transform textTransform = gameObject.transform.Find("SliderAmount");
        textComp = textTransform.gameObject.GetComponent<TMP_Text>();
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        textComp.text = slider.value.ToString();
    }
}
