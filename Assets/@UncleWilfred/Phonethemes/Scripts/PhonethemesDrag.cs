using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Linq;

namespace UncleWilfred.Phonethemes
{
    public class PhonethemesDrag : MonoBehaviour
    {
        [SerializeField]
        private SentenceScriptable sentenceData;
        [SerializeField]
        private Sprite[] spritesImages;
        [SerializeField]
        private TextMeshProUGUI txtQuestion;
        [SerializeField]
        private LevelDragText[] txtAnswers;
        [SerializeField]
        private Image spriteImage, bubbles;
        [SerializeField]
        private Transform imageTransform;
        [SerializeField]
        Color[] colors;

        private SentenceData[] sentences;
        private int currentIndex;
        public void Start()
        {
            GetQuestions();

            currentIndex = 0;

            ShowQuestion();
        }

        void GetQuestions()
        {
            sentences = new SentenceData[sentenceData.listSentence.Count];
            for(int i=0;i<sentences.Length;i++)
            {
                sentences[i]= sentenceData.listSentence[i];
            }
        }

        private void ShowQuestion()
        {
            bubbles.color = Color.white;
            imageTransform.localScale = Vector3.zero;
            txtQuestion.text = string.Format(sentences[currentIndex].sentence, "_______");

            List<string> temp = sentences[currentIndex].answers.OrderBy(x => UnityEngine.Random.value).ToList();
            for(int i=0;i<3;i++)
            {
                txtAnswers[i].Init(temp[i]);

                LevelDragText item = txtAnswers[i];
                item.onDropOnTarget = delegate{
                    Debug.Log("Answer : " + sentences[currentIndex].answers[0]);
                    Debug.Log("Text : " + item.text.text);
                    if(item.text.text == sentences[currentIndex].answers[0])
                    {
                        txtQuestion.text = string.Format(sentences[currentIndex].sentence, "<color=#1CE0FF>" + sentences[currentIndex].answers[0] + "</color>");
                        item.gameObject.SetActive(false);
                        StartCoroutine(ShowCorrectResults(sentences[currentIndex].answers[0]));
                    }
                    else
                    {
                        item.WrongAnswer();
                    }
                };
            }
        }

        IEnumerator ShowCorrectResults(string answer)
        {
            Color newColor;
            if(answer.StartsWith("f"))
            {
                newColor = colors[0];
            }
            else if(answer.StartsWith("l"))
                newColor = colors[1];
            else
                newColor = colors[2];

            bubbles.DOColor(newColor, 0.5f);
            
            yield return new WaitForSeconds(0.5f);

            spriteImage.sprite = spritesImages[currentIndex];
            Debug.Log("Sprite images : " + spritesImages[currentIndex]);
            imageTransform.DOScale(Vector3.one, 0.3f);
            yield return new WaitForSeconds(1.5f);
            currentIndex++;

            if(currentIndex<sentences.Length)
                ShowQuestion();
        }
    }
}