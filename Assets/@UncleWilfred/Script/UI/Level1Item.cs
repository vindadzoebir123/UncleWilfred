using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UncleWilfred
{
    public class Level1Item : MonoBehaviour
    {
        public Image imageSprite;
        public TextMeshProUGUI textImage;

        public void Init(Sprite sprite, string text)
        {
            imageSprite.sprite = sprite;
            textImage.text = text;
        }
    }
}