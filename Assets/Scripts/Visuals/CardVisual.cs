using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardVisual : MonoBehaviour
{
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI DescriptionText;
    public Image CardImage;

    public void Initialize(string title, string description, Sprite sprite)
    {
        TitleText.text = title;
        DescriptionText.text = description;
        CardImage.sprite = sprite;
    }
}
