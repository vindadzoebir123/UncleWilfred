using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UniRx;
using UnityEngine.SceneManagement;

namespace UncleWilfred
{
    public class ScorePanel : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        public Button restartBtn;

        public void Init(int score)
        {
            scoreText.text = score.ToString();

            restartBtn.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                SceneManager.LoadScene("usefulwords");
            });
        }

    }
}