using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System.Linq;
using System;

namespace UncleWilfred
{
    public class MatchingPanel1 : MonoBehaviour
    {
        public Animator animator;
        public AnimationHelper animatorHelper;

        public GameObject questionParent;
        public List<Level1Item> level1Items;
        public List<LevelDragText> textDrags;

        List<Question> questions = new List<Question>();

        int correctedAnswered;
        int startCount = 0;

        public Action OnFinishPhase;

        public void Init(List<Question> input)
        {
            questions = input.OrderBy( x => UnityEngine.Random.value ).ToList( );
            animator.enabled = true;
            animatorHelper.onFinish = delegate{
                animator.enabled = false;
                RenderQuestion();
            };
            startCount = 0;
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
                level1Items[i].OnCorrectAnswered = delegate{
                    correctedAnswered +=1;
                    CheckFinish();
                };
            }
        }

        void CheckFinish()
        {
            if(correctedAnswered>=5)
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
            yield return new WaitForSeconds(0.2f);

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