using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboTextController : MonoBehaviour
{
    private int comboCounter = 0;
    TextMeshProUGUI comboText;
    // Start is called before the first frame update
    void Start()
    {
        comboText = GetComponent<TextMeshProUGUI>();
        comboText.text = comboCounter.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //setComboCounter(getComboCounter() + 1);
    }

    public int getComboCounter()
    {
        return comboCounter;
    }

    public void setComboCounter(int value)
    {
        comboCounter = value;
        comboText.text = value.ToString();
    }
}
