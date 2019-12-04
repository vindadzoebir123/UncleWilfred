using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

namespace UncleWilfred
{
    public class Level1Item : MonoBehaviour
    {
        public Image imageSprite;
        public TextMeshProUGUI textImage;

        public Action OnCorrectAnswered;
        string answer;

        public void Init(Sprite sprite, string answer)
        {
            this.answer = answer;
            imageSprite.sprite = sprite;
            textImage.text = answer;
            textImage.gameObject.SetActive(false);
        }

        public bool CheckAnswer(string check)
        {
            if(check == answer)
            {
                RenderTrue();
                return true;
            }
            return false;
        }

        void RenderTrue()
        {
            textImage.gameObject.SetActive(true);
            if(OnCorrectAnswered!=null)
                OnCorrectAnswered();
        }
    }
}