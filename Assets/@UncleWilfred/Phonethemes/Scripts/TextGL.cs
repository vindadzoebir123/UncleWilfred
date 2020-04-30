using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace UncleWilfred.Phonethemes
{
    public class TextGL : TextAnim
    {
        Tween tween;
        TextMeshPro tmp;


        Vector3 startPos;
        void Start()
        {
            startPos = transform.position;
        }
        
        public override void Init(WordImagesPair pair)
        {
            base.Init(pair);
            rb.velocity = new Vector2(0, -1f);

            tmp = GetComponent<TextMeshPro>();
            tween = tmp.DOFade(0, 1f).SetLoops(-1, LoopType.Yoyo);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if(col.tag == "base")
            {
                ResetPosition();
                // rb.isKinematic = true;
                // tween.Kill();

                // tmp.DOFade(1, 0.5f);
                // // tmp.transform.DOScale(new Vector3(0.2f,0.2f,0.2f), 1f).OnComplete(delegate{
                // //     ResetPosition();
                //     // Destroy(gameObject);
                // });
            }
        }

        void OnDestroy()
        {
            tween.Kill();
        }

        void ResetPosition()
        {
            transform.position = startPos;
        }
    }
}