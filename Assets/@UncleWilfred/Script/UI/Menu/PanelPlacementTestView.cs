using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// using System.Linq;
using DG.Tweening;
using System;

namespace UncleWilfred
{
    public class PanelPlacementTestView : MonoBehaviour
    {
        // public QuestionScriptable questionData;

        public Button btnYes, btnNo;
        public Image spriteImage;
        public TextMeshProUGUI textImage;
        List<Question> questions = new List<Question>();

        List<Transform> cardTransform = new List<Transform>();
        public Transform imageTransform, cardParent;
        public GameObject cardPrefab, buttonParent;

        public Animator spriteAnimator;

        int index, yesScore, noScore;

        public void Init()
        {
            Close();
        }

        
        public void Open()
        {
            if(!gameObject.activeSelf)
                gameObject.SetActive(true);

            ChooseQuestions();
            CreateCards(delegate{
                Debug.Log("Animation complete");
                ShowQuestion();
                // ChooseQuestions();
            });
        }

        void ChooseQuestions()
        {
            imageTransform.gameObject.SetActive(false);
            questions.Clear();
            QuestionScriptable data = Resources.Load<QuestionScriptable>("QuestionData");
            Debug.Log("Data : " + data.questions.Count);
            for(int i=0;i<data.questions.Count;i++)
            {
                questions.Add(data.questions[i]);
            }
            // questions = Resources.Load<QuestionScriptable>("QuestionData").questions;
            Debug.Log("Question count : " + questions.Count );
            yesScore = 0;
            noScore = 0;
            // questions = questionData.questions.OrderBy( x => UnityEngine.Random.value ).ToList( );
            index = 0;
            buttonParent.SetActive(false);
            textImage.gameObject.SetActive(false);
            // ShowQuestion();
        }

        public void ShowNextQuestion()
        {
            // if(yesScore>=7)
            // {
            //     string level = "Expert";
            //     PanelResultMenu.Show(level);
            //     return;
            // }

            // if(noScore>=7)
            // {
            //     string level = "Advance";
            //     PanelResultMenu.Show(level);
            //     return;
            // }
            
            // index++;
            // if(index>=15)
            // {
            //     string level = yesScore>7 ? "Expert" : "Advance";
            //     //go to result screen
            //     PanelResultMenu.Show(level);
            // }
            // else
            //     ShowQuestion();
            index++;

            if(index>=15)
            {
                PanelResultMenu.Show("Expert");
            }
            else
                ShowQuestion();
        }

        public void OnYesSelected()
        {
            ShowNextQuestion();
        }

        public void OnNoSelected()
        {
            // noScore++;
            if(index<7)
            {
                // string level = yesScore>7 ? "Expert" : "Advance";
            //     //go to result screen
                PanelResultMenu.Show("Advance");
            }
            else if(index>=7)
                PanelResultMenu.Show("Expert");
            // else
            //     ShowNextQuestion();
        }

        Sequence seq;
        void CreateCards(Action onAnimationCompleted)
        {
            DestroyAllCards();
            cardTransform.Clear();

            DOTween.Kill(seq);
            seq = DOTween.Sequence();

            for(int i=0;i<15;i++)
            {
                GameObject item = Instantiate(cardPrefab);
                item.transform.SetParent(cardParent, false);
                item.transform.SetAsFirstSibling();
                cardTransform.Add(item.transform);
                RectTransform rect = item.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(900,900);
                seq.Append(rect.DOAnchorPos(new Vector2(0,0), 0.1f));
            }

            seq.OnComplete(delegate{
                if(onAnimationCompleted!=null)
                    onAnimationCompleted();
            });
        }

        void DestroyAllCards()
        {
            for(int i=0;i<cardParent.childCount;i++)
            {
                Destroy(cardParent.GetChild(i).gameObject);
            }
        }

        void ShowQuestion()
        {
            // Debug.Log("Show question after animation complete " + index);
            buttonParent.SetActive(false);
            imageTransform.gameObject.SetActive(false);
            textImage.gameObject.SetActive(false);
            spriteImage.sprite = questions[index].sprite;
            textImage.text = questions[index].text;
            cardTransform[index].DOMove(imageTransform.position, 0.3f).OnComplete(delegate{
                // Debug.Log("Finish animate card transform");
                cardTransform[index].GetComponent<Animator>().enabled = true;
                cardTransform[index].GetComponent<AnimationHelper>().onFinish = delegate
                {
                    imageTransform.gameObject.SetActive(true);
                    textImage.gameObject.SetActive(true);
                    spriteAnimator.SetTrigger("Flip");
                    buttonParent.SetActive(true);
                    Destroy(cardTransform[index].gameObject);
                };
            });
        }

        public void Close()
        {
            if(gameObject.activeSelf)
                gameObject.SetActive(false);
        }
    }
}