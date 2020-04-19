using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace UncleWilfred.Phonethemes
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class TextL : TextAnim
    {
        Tween tween;
        public override void Init(string text)
        {
            float velX = Random.Range(-1f,1f);
            rb.velocity = new Vector2(velX, -1f);
            StartCoroutine(ChangeVelocity());
            
            base.Init(text);
            // float xMove = Random.Range(-1f, 1f);
            // Debug.Log("Word : " + text + ", position : " + transform.position.x + ", x move : " + xMove);
            // tween = rb.DOMoveX(transform.position.x +xMove, 1f).SetLoops(-1, LoopType.Yoyo);
            //  tween = rb.DOMoveX(transform.position.x + xMove, 1f).OnComplete(delegate{
            //      Debug.Log("<color=red>" + text + ", position : " + transform.position.x + "</color>");
            //  });
        }

        IEnumerator ChangeVelocity()
        {
            while(gameObject.activeSelf)
            {
                rb.velocity = new Vector2(-rb.velocity.x, -1f);

                yield return new WaitForSeconds(1f);

            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if(col.tag == "base")
            {
                Destroy(gameObject);
            }
        }

        void OnDestroy()
        {
            tween.Kill();
        }

    }
}