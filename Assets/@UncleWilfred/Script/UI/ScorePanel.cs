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
        public Button restartBtn, nextBtn;

        public void Init(int score, int totalScore, bool end)
        {

            scoreText.text = string.Format("{0} / {1}", score, totalScore);

            restartBtn.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                SceneManager.LoadScene("usefulwords");
            });

            restartBtn.gameObject.SetActive(end);
            nextBtn.gameObject.SetActive(!end);

            Show();

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