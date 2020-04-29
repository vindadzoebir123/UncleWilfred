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

        Vector3 startPos;

        Rigidbody2D rigidbody;
        [SerializeField]
        private float speed = 1f;
        void Start()
        {
            startPos = transform.position;
            rigidbody = GetComponent<Rigidbody2D>();
        }

        public override void Init(string text)
        {
            // float velX = Random.Range(-1f,1f);
            // rb.velocity = new Vector2(velX, -1f);
            // StartCoroutine(ChangeVelocity());
            float distance = Random.Range( 1f,2f);
            tween = transform.DOLocalMoveX(transform.position.x + distance, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
            
            base.Init(text);
        }

        void Update()
        {
            rigidbody.velocity = new Vector2(0, speed);
        }

        IEnumerator ChangeVelocity()
        {
            while(gameObject.activeSelf)
            {
                rb.velocity = new Vector2(-rb.velocity.x, -1f);

                yield return new WaitForSeconds(1.5f);

            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if(col.tag == "base")
            {
                ResetPosition();
                // Destroy(gameObject);
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