using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UncleWilfred
{
    [CreateAssetMenu(fileName = "QuestionData", menuName = "UncleWilfred/QuestionData", order = 1)]
    public class QuestionScriptable : ScriptableObject
    {
        public List<Question> questions;
    }

    [System.Serializable]
    public class Question
    {
        public Sprite sprite;
        public string text;
        public AudioClip audio;
    }
}