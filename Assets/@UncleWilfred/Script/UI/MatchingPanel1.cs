using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System.Linq;
using System;
using TMPro;
using UniRx;

namespace UncleWilfred
{
    public class MatchingPanel1 : MonoBehaviour
    {
        public Animator animator;
        public AnimationHelper animatorHelper;

        public GameObject questionParent;
        public List<Level1Item> level1Items;
        public List<LevelDragText> textDrags;
        public TextMeshProUGUI scoreText;
        public Image clockImage;
        public Color[] clockColor;
        bool isCombo;

        // int currentScore;

        public AudioClip brilliant, amazing, welldone;

        List<Question> questions = new List<Question>();

        float timerCountdown;

        int correctedAnswered;
        int startCount = 0;

        public Action OnFinishPhase;
        bool isPlay;

        public void Init(List<Question> input, IntReactiveProperty score)
        {
            isPlay = false;
            timerCountdown = 0f;
            isCombo = false;

            Observable.Interval(TimeSpan.FromSeconds(0.2f)).TakeUntilDisable(this).Subscribe(x =>
            {
                if(isPlay)
                {
                    timerCountdown+=0.1f;
                    clockImage.fillAmount = 1 - (timerCountdown/5f);
                    if(timerCountdown<3f)
                        clockImage.color = clockColor[0];
                    else
                        clockImage.color = clockColor[1];
                }
            });

            questions = input.OrderBy( x => UnityEngine.Random.value ).ToList( );
            animator.enabled = true;
            animatorHelper.onFinish = delegate{
                animator.enabled = false;
                RenderQuestion();
            };
            startCount = 0;

            score.TakeUntilDestroy(this).Subscribe(x => {
                scoreText.text = x.ToString();
            });

            CalculateTotalScore();
            
        }

        void CalculateTotalScore()
        {
            int score = (questions.Count * 8) + ((questions.Count-1) * 3);
            UsefulWords.Instance.UpdateTotalScoreRound1(score);
        }

        void RenderQuestion()
        {
            ViewQuestions(startCount);
            ViewTextItems(startCount);
        }

        void ViewQuestions(int startCount)
        {
            isPlay = true;
            timerCountdown = 0;
            clockImage.fillAmount = 1;
            correctedAnswered = 0;

            questionParent.SetActive(true);
            int count = level1Items.Count;
            for(int i=0;i<count;i++)
            {
                int index = startCount + i;
                level1Items[i].Init(questions[index].sprite, questions[index].text);
                level1Items[i].OnCorrectAnswered = delegate(bool correct){
                    if(correct)
                    {
                        
                        correctedAnswered +=1;
                        CheckFinish();
                        UsefulWords.Instance.AddScoreRound1(5);
                        // currentScore +=5;
                        if(isCombo)
                        {
                            UsefulWords.Instance.AddScoreRound1(3, true);
                            // currentScore +=5;
                        }
                        isCombo = true;

                        if(timerCountdown<=3f)
                        {
                            AudioManager.Instance.Play(brilliant);
                            UsefulWords.Instance.AddScoreRound1(3, true);
                        }
                        else if(timerCountdown<=5f)
                        {
                            AudioManager.Instance.Play(amazing);
                            UsefulWords.Instance.AddScoreRound1(2, true);
                        }
                        else
                        {
                            AudioManager.Instance.Play(welldone);
                        }

                        timerCountdown = 0;
                    }
                    else
                        isCombo = false;
                };
            }
        }

        void CheckFinish()
        {
            if(correctedAnswered>=3)
            {
                Debug.Log("Has answered all correctly");
                StartCoroutine(ShowNextPhase());
            }
            else
            {
                Debug.Log("Correct answer : " + correctedAnswered);
            }
        }

        IEnumerator ShowNextPhase()
        {
            // if(timerCountdown<=3f)
            // {
            //     AudioManager.Instance.Play(brilliant);
            //     UsefulWords.Instance.AddScoreRound1(15, true);
            // }
            // else if(timerCountdown<=5f)
            // {
            //     AudioManager.Instance.Play(amazing);
            //     UsefulWords.Instance.AddScoreRound1(5, true);
            // }
            // else
            // {
            //     AudioManager.Instance.Play(welldone);
            // }

            yield return new WaitForSeconds(0.5f);

            if(startCount + level1Items.Count < questions.Count)
                {
                    startCount += level1Items.Count;
                    RenderQuestion();
                }
                else
                {
                    if(OnFinishPhase!=null)
                        OnFinishPhase();
                }
        }

        void ViewTextItems(int startCount)
        {
            List<string> textList = new List<string>();

            int count = textDrags.Count;
            for(int i=0;i<count;i++)
            {
                int index = startCount + i;
                textList.Add(questions[index].text);
            }

            textList = textList.OrderBy(x => UnityEngine.Random.value).ToList();
            for(int i=0;i<count;i++)
            {
                textDrags[i].Init(textList[i]);
                Debug.Log("Init text item : " + i + ", count : " + count);
            }
        }
    }
}