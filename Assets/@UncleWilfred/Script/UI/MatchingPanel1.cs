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
        bool isCombo;

        public AudioClip brilliant, amazing, welldone;

        List<Question> questions = new List<Question>();

        float timerCountdown;

        int correctedAnswered;
        int startCount = 0;

        public Action OnFinishPhase;

        public void Init(List<Question> input, IntReactiveProperty score)
        {
            timerCountdown = 0f;
            isCombo = false;

            Observable.Interval(TimeSpan.FromMilliseconds(100)).TakeUntilDestroy(this).Subscribe(x =>
            {
                timerCountdown+=0.1f;
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
        }

        void RenderQuestion()
        {
            ViewQuestions(startCount);
            ViewTextItems(startCount);
        }

        void ViewQuestions(int startCount)
        {
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
                        UsefulWords.Instance.AddScore(5);
                        if(isCombo)
                        {
                            UsefulWords.Instance.AddScore(3, true);
                        }
                        isCombo = true;
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
            if(timerCountdown<=3f)
            {
                AudioManager.Instance.Play(brilliant);
                UsefulWords.Instance.AddScore(15, true);
            }
            else if(timerCountdown<=5f)
            {
                AudioManager.Instance.Play(amazing);
                UsefulWords.Instance.AddScore(5, true);
            }
            else
            {
                AudioManager.Instance.Play(welldone);
            }

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