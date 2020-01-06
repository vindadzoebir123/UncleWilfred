using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using TMPro;

namespace UncleWilfred
{
    public class UsefulWords : MonoBehaviour
    {
        public static UsefulWords Instance;
        QuestionScriptable question;
        List<Question> questions = new List<Question>();
        List<SentenceData> sentences = new List<SentenceData>();

        public AnimatedScore animatedObj;
        public IntReactiveProperty Scoring = new IntReactiveProperty(0);

        public MemorizePanel memorizePanel;
        public MatchingPanel1 matchingPanel;
        public SentencePanel sentencePanel;
        public ScorePanel scorePanel;

        public LanguageSelection languageSelection;

        LanguageSetting selectedLanguage;
        
        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            Init();
        }

        void Init()
        {
            // Debug.Log("Load questions : " + question.questions.Count);

            memorizePanel.gameObject.SetActive(false);
            matchingPanel.gameObject.SetActive(false);
            sentencePanel.gameObject.SetActive(false);
            scorePanel.gameObject.SetActive(false);
            languageSelection.Init();

            languageSelection.OnSelectLanguageAsObservable().TakeUntilDisable(languageSelection.gameObject).Subscribe(x => {
                selectedLanguage = x;
                languageSelection.Hide();
                LoadWordsData();
            });
            // languageSelection.gameObject.SetActive(false);



            // InitiatePhase1();
        }

        void LoadWordsData()
        {
            question = Resources.Load<QuestionScriptable>("WordData_" + selectedLanguage.ToString());
            questions = question.questions; //.OrderBy( x => UnityEngine.Random.value ).ToList( );

            SentenceScriptable item = Resources.Load<SentenceScriptable>("Sentence_" + selectedLanguage.ToString());
            sentences = item.listSentence; //.OrderBy(x => UnityEngine.Random.value).ToList();

            InitiatePhase1();

        }
        public void InitiatePhase1()
        {
            List<Question> phase1 = new List<Question>();

            int number = questions.Count / 2;
            for(int i=0;i<number;i++)
            {
                phase1.Add(questions[i]);
            }
            memorizePanel.gameObject.SetActive(true);
            memorizePanel.Init(phase1);

            memorizePanel.OnCompleteMemorizeAsObservable().TakeUntilDisable(memorizePanel.gameObject).Subscribe(x => {
                memorizePanel.gameObject.SetActive(false);
                matchingPanel.gameObject.SetActive(true);
                matchingPanel.Init(phase1, Scoring);
                // matchingPanel.OnFinishPhase = InitPhase2;
                matchingPanel.OnFinishPhase = InitSentencePhase1;
            });
        }

        public void AddScore(int add, bool animated = false)
        {
            Scoring.Value += add;

            if(animated)
            {
                AnimatedScore item = Instantiate(animatedObj);
                item.transform.SetParent(transform,false);
                item.Init(add);
            }
        }

        void InitSentencePhase1()
        {
            List<SentenceData> phase1 = new List<SentenceData>();

            int number = sentences.Count / 2;
            for(int i=0;i<number;i++)
            {
                phase1.Add(sentences[i]);
            }
            sentencePanel.gameObject.SetActive(true);
            matchingPanel.gameObject.SetActive(false);
            memorizePanel.gameObject.SetActive(false);
            sentencePanel.Init(phase1, Scoring);

            sentencePanel.OnCompleteSentenceAsObservable().TakeUntilDisable(sentencePanel.gameObject).Subscribe(x => {
                sentencePanel.gameObject.SetActive(false);
                InitPhase2();
                // memorizePanel.gameObject.SetActive(false);
                // matchingPanel.gameObject.SetActive(true);
                // matchingPanel.Init(phase1);
                // matchingPanel.OnFinishPhase = InitPhase2;
            });
        }

        void InitSentencePhase2()
        {
            List<SentenceData> phase1 = new List<SentenceData>();

            int number = sentences.Count / 2;
            for(int i=number;i<sentences.Count;i++)
            {
                phase1.Add(sentences[i]);
            }
            sentencePanel.gameObject.SetActive(true);
            matchingPanel.gameObject.SetActive(false);
            memorizePanel.gameObject.SetActive(false);
            sentencePanel.Init(phase1, Scoring);

            sentencePanel.OnCompleteSentenceAsObservable().TakeUntilDisable(sentencePanel.gameObject).Subscribe(x => {
                CompleteLevel();
                // sentencePanel.gameObject.SetActive(false);
                // InitPhase2();
                // memorizePanel.gameObject.SetActive(false);
                // matchingPanel.gameObject.SetActive(true);
                // matchingPanel.Init(phase1);
                // matchingPanel.OnFinishPhase = InitPhase2;
            });
        }

        void InitPhase2()
        {
            List<Question> phase2 = new List<Question>();
            int number = questions.Count/2;
            for(int i=number;i<questions.Count;i++)
            {
                phase2.Add(questions[i]);
            }
            memorizePanel.gameObject.SetActive(true);
            matchingPanel.gameObject.SetActive(false);
            memorizePanel.Init(phase2);

            memorizePanel.OnCompleteMemorizeAsObservable().TakeUntilDisable(memorizePanel.gameObject).Subscribe(x => {
                memorizePanel.gameObject.SetActive(false);
                matchingPanel.gameObject.SetActive(true);
                matchingPanel.Init(phase2, Scoring);
                matchingPanel.OnFinishPhase = InitSentencePhase2;
            });
        }

        void CompleteLevel()
        {
            matchingPanel.gameObject.SetActive(false);
            sentencePanel.gameObject.SetActive(false);
            scorePanel.gameObject.SetActive(true);
            scorePanel.Init(Scoring.Value);
            Debug.Log("Showing score panel");
            // sentencePanel.Init();
        }
    }
}