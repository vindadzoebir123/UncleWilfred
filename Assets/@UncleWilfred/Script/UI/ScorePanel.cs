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
        public TextMeshProUGUI round1Score, round2Score, totalScore;
        public Button restartBtn;

        public void Init(int score1, int total1, int score2, int total2)
        {

            // scoreText.text = string.Format("{0} / {1}", score, totalScore);

            restartBtn.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                SceneManager.LoadScene("usefulwords");
            });

            // restartBtn.gameObject.SetActive(end);
            // nextBtn.gameObject.SetActive(!end);

            Show();

            round1Score.text = string.Format("{0} / {1}", score1, total1);
            round2Score.text = string.Format("{0} / {1}", score2, total2);
            totalScore.text = string.Format("{0} / {1}", score1 + score2, total1 + total2);

            // nextBtn.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(x => {

            // });
        }

        public void Show()
        {
            if(!gameObject.activeSelf)
                gameObject.SetActive(true);
        }

        public void Hide()
        {
            if(gameObject.activeSelf)
                gameObject.SetActive(false);
        }

    }
}