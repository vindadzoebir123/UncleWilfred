using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UncleWilfred.Phonethemes
{
    [CreateAssetMenu(fileName = "WordImages", menuName = "UncleWilfred/PhonethemesImages", order = 4)]
    public class SOWordImages : ScriptableObject
    {
        public List<WordImagesPair> words;
    }

    [System.Serializable]
    public class WordImagesPair
    {
        public string words;
        public Sprite image;
    }
}