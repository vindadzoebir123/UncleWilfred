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
        public override void Init(string text)
        {
            base.Init(text);
            rb.velocity = new Vector2(0, -1f);

            tmp = GetComponent<TextMeshPro>();
            tween = tmp.DOFade(0, 1f).SetLoops(-1, LoopType.Yoyo);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if(col.tag == "base")
            {
                rb.isKinematic = true;
                tween.Kill();

                tmp.DOFade(1, 0.5f);
                tmp.transform.DOScale(new Vector3(0.2f,0.2f,0.2f), 1f).OnComplete(delegate{
                    Destroy(gameObject);
                });
            }
        }
    }
}