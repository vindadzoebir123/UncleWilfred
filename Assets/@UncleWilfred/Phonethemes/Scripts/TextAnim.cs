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
        public Sprite image;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public virtual void Init(WordImagesPair pair)
        {
            image = pair.image;
            GetComponent<TextMeshPro>().text = pair.words;
        }

        void OnMouseDown()
        {
            // Debug.Log("Click word " + image.name);
            ImagePanelShowing.Instance.Show(image);
        }
    }
}