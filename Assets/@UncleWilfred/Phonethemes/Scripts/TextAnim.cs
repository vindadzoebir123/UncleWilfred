using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UncleWilfred.Phonethemes
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class TextAnim : MonoBehaviour
    {
        protected Rigidbody2D rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public virtual void Init(string text)
        {
            GetComponent<TextMeshPro>().text = text;
        }
    }
}