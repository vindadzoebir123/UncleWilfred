using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UncleWilfred.Phonethemes
{
     [CreateAssetMenu(fileName = "Words", menuName = "UncleWilfred/Phonethemes", order = 3)]
    public class WordsScriptable : ScriptableObject
    {
        public List<string> words = new List<string>();
    }
}