using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UncleWilfred
{
    public class AnimatedScore : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;

        public void Init(int score)
        {
            scoreText.text = "+" + score.ToString();
        }
        public void OnFinishAnimate()
        {
            Destroy(this.gameObject);
        }
    }
}