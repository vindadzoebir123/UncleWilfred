using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UniRx;
using System;

namespace UncleWilfred
{
    public class SentencePanel : MonoBehaviour
    {
        public TextMeshProUGUI sentenceText;
        public TextMeshProUGUI scoreText;

        public LevelDragText[] textDrag;

        // public Image clockImage;
        // public Color[] clockColor;

        List<SentenceData> listSentence = new List<SentenceData>();
        int index;

        Subject<Unit> onCompleteSentence;

        bool isCombo;
        float timerCountDown;
        public IObservable<Unit> OnCompleteSentenceAsObservable()
        {
            return onCompleteSentence ?? (onCompleteSentence = new Subject<Unit>());
        }

        // void Start()
        // {
        //     Init();
        // }
        public void Init(List<SentenceData> data, IntReactiveProperty score)
        {
            // SentenceScriptable item = Resources.Load<SentenceScriptable>("Sentence_Arabic");
            isCombo = false;

            timerCountDown = 0f;

            Observable.Interval(TimeSpan.FromMilliseconds(100)).TakeUntilDestroy(this).Subscribe(x =>
            {
                timerCountDown+=0.1f;
            });

            listSentence.Clear();
            listSentence = data; //.OrderBy(x => UnityEngine.Random.value).ToList();

            index = 0;

            score.TakeUntilDestroy(this).Subscribe(x => {
                scoreText.text = x.ToString();
            });
            ShowQuiz();
            CalculateTotalScore();
        }

        void CalculateTotalScore()
        {
            int score = listSentence.Count * 10 + ((listSentence.Count-1) * 5);
            UsefulWords.Instance.UpdateTotalScore(score);
        }

        void ShowQuiz()
        {
            sentenceText.text = string.Format(listSentence[index].sentence, "_______");
            List<string> temp = listSentence[index].answers.OrderBy(x => UnityEngine.Random.value).ToList();
            for(int i=0;i<3;i++)
            {
                textDrag[i].Init(temp[i]);

                LevelDragText item = textDrag[i];
                item.onDropOnTarget = delegate{
                    Debug.Log("Answer : " + listSentence[index].answers[0]);
                    Debug.Log("Text : " + item.text.text);
                    if(item.text.text == listSentence[index].answers[0])
                    {
                        sentenceText.text = string.Format(listSentence[index].sentence, "<color=#1CE0FF>" + listSentence[index].answers[0] + "</color>");
                        item.gameObject.SetActive(false);
                        AudioManager.Instance.Play(listSentence[index].audio);
                        float timer = listSentence[index].audio.length + 0.5f;
                        StartCoroutine(ShowNextQuiz(timer));
                        UsefulWords.Instance.AddScore(10);
                        if(isCombo)
                            UsefulWords.Instance.AddScore(5, true);
                        
                        isCombo = true;
                    }
                    else
                    {
                        isCombo = false;
                        item.WrongAnswer();
                    }
                };
            }
        }

        IEnumerator ShowNextQuiz(float delay)
        {
            yield return new WaitForSeconds(delay);

            index++;
            if(index<=listSentence.Count-1)
                ShowQuiz();
            
            else
            {
                //  if(timerCountDown<=3f)
                // {
                //     // AudioManager.Instance.Play(brilliant);
                //     UsefulWords.Instance.AddScore(30, true);
                // }
                // else if(timerCountDown<=5f)
                // {
                //     // AudioManager.Instance.Play(amazing);
                //     UsefulWords.Instance.AddScore(10, true);
                // }
                // else
                // {
                //     AudioManager.Instance.Play(welldone);
                // }
                yield return new WaitForSeconds(0.5f);
                if(onCompleteSentence!=null)
                    onCompleteSentence.OnNext(Unit.Default);
            }
        }
    }
}