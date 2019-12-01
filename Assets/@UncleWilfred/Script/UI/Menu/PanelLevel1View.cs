using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace UncleWilfred
{
    public class PanelLevel1View : MonoBehaviour
    {
        public MemorizePanel memorizePanel;
        public MatchingPanel1 matchingPanel;

        QuestionScriptable question;

        public void Init()
        {
            Close();
            question = Resources.Load<QuestionScriptable>("Level1Data");
            Debug.Log("Load questions : " + question.questions.Count);
            memorizePanel.gameObject.SetActive(false);
            matchingPanel.gameObject.SetActive(false);
        }

        public void Open()
        {
            if(!gameObject.activeSelf)
                gameObject.SetActive(true);

            memorizePanel.gameObject.SetActive(true);
            memorizePanel.Init(question.questions);

            memorizePanel.OnCompleteMemorizeAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                memorizePanel.gameObject.SetActive(false);
                matchingPanel.gameObject.SetActive(true);
                matchingPanel.Init(question.questions);
            });
        }
       
        public void Close()
        {
            if(gameObject.activeSelf)
                gameObject.SetActive(false);
        }
    }
}