using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorController : MonoBehaviour {
    public string colorValue;
    public Image colorSource;
    public InputField colorValueText;
    public Image[] changeImageColor;
    public Text[] changeTextColor;

    public void SetColor()
    {

        colorValueText.GetComponent<InputField>().text = colorValue;

        for (int i = 0; i < changeImageColor.Length; i++){

            changeImageColor[i].GetComponent<Image>().color = colorSource.GetComponent<Image>().color;

        }

        for (int n = 0; n < changeTextColor.Length; n++){

            changeTextColor[n].GetComponent<Text>().color = colorSource.GetComponent<Image>().color;

        }

        Debug.Log("Color " + colorValue + " is set");

    }
}