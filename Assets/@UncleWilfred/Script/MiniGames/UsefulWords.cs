using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

namespace UncleWilfred
{
    public class UsefulWords : MonoBehaviour
    {
        QuestionScriptable question;
        List<Question> questions = new List<Question>();

        public MemorizePanel memorizePanel;
        public MatchingPanel1 matchingPanel;

        void Start()
        {
            Init();
        }

        void Init()
        {
            question = Resources.Load<QuestionScriptable>("Level1Data");
            Debug.Log("Load questions : " + question.questions.Count);

            memorizePanel.gameObject.SetActive(false);
            matchingPanel.gameObject.SetActive(false);

            questions = question.questions.OrderBy( x => UnityEngine.Random.value ).ToList( );


            InitiatePhase1();
        }

        public void InitiatePhase1()
        {
            List<Question> phase1 = new List<Question>();

            for(int i=0;i<15;i++)
            {
                phase1.Add(questions[i]);
            }
            memorizePanel.gameObject.SetActive(true);
            memorizePanel.Init(phase1);

            memorizePanel.OnCompleteMemorizeAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                memorizePanel.gameObject.SetActive(false);
                matchingPanel.gameObject.SetActive(true);
                matchingPanel.Init(phase1);
                matchingPanel.OnFinishPhase = InitPhase2;
            });
        }

        void InitPhase2()
        {
            List<Question> phase2 = new List<Question>();
            for(int i=15;i<25;i++)
            {
                phase2.Add(questions[i]);
            }
            memorizePanel.gameObject.SetActive(true);
            matchingPanel.gameObject.SetActive(false);
            memorizePanel.Init(phase2);

            memorizePanel.OnCompleteMemorizeAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                memorizePanel.gameObject.SetActive(false);
                matchingPanel.gameObject.SetActive(true);
                matchingPanel.Init(phase2);
                matchingPanel.OnFinishPhase = CompleteLevel;
            });
        }

        void CompleteLevel()
        {
            Debug.Log("Complete levels");
        }
    }
}