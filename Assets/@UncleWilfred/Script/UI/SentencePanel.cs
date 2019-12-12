using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

namespace UncleWilfred
{
    public class SentencePanel : MonoBehaviour
    {
        public TextMeshProUGUI sentenceText;

        public LevelDragText[] textDrag;

        List<SentenceData> listSentence = new List<SentenceData>();
        int index;

        // void Start()
        // {
        //     Init();
        // }
        public void Init()
        {
            SentenceScriptable item = Resources.Load<SentenceScriptable>("SentenceQuiz");
            listSentence = item.listSentence.OrderBy(x => UnityEngine.Random.value).ToList();

            index = 0;

            ShowQuiz();
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
                        StartCoroutine(ShowNextQuiz());
                    }
                    else
                        item.WrongAnswer();
                };
            }
        }

        IEnumerator ShowNextQuiz()
        {
            yield return new WaitForSeconds(1f);

            index++;
            if(index<=listSentence.Count-1)
                ShowQuiz();
        }
    }
}