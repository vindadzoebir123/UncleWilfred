using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UncleWilfred
{
    [CreateAssetMenu(fileName = "SentenceQuiz", menuName = "UncleWilfred/SentenceQuiz", order = 2)]
    public class SentenceScriptable : ScriptableObject
    {
        public List<SentenceData> listSentence;
    }

    [System.Serializable]
    public class SentenceData
    {
        public string sentence;
        public string[] answers = new string[3];

        public AudioClip audio;
    }
}