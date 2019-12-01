using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using System;

namespace UncleWilfred
{
    public class MemorizePanel : MonoBehaviour
    {
        Subject<Unit> onCompleteMemorize;
        public IObservable<Unit> OnCompleteMemorizeAsObservable()
        {
            return onCompleteMemorize ?? (onCompleteMemorize = new Subject<Unit>());
        }

        public Animator animator;
        public AnimationHelper animatorHelper;
        public Animator imageAnim;
        public Image imageSprite;
        public TextMeshProUGUI textImage;
        public GameObject questionParent;

        public float countingTimer = 3f;

        int currentQuestion = 0;

        List<Question> questions = new List<Question>();

        public void Init(List<Question> input)
        {
            questions = input;
            animator.enabled = true;
            currentQuestion = 0;

            Debug.Log("Question count : " + questions.Count);

            animatorHelper.onFinish = delegate{
                animator.enabled = false;
                StartCoroutine(ShowQuestion(countingTimer));
            };
        }

        IEnumerator ShowQuestion(float timer)
        {

            questionParent.SetActive(true);
            imageSprite.sprite = questions[currentQuestion].sprite;
            textImage.text = questions[currentQuestion].text;
            imageAnim.SetTrigger("Flip");

            yield return new WaitForSeconds(timer);

            currentQuestion++;
            if(currentQuestion<questions.Count-1)
            {
                countingTimer = Mathf.Clamp(countingTimer-0.2f, 1f, 3f);
                StartCoroutine(ShowQuestion(countingTimer));
            }
            else
            {
                Debug.Log("Finish memorizing");
                // yield return new WaitForSeconds(timer);
                if(onCompleteMemorize!=null)
                    onCompleteMemorize.OnNext(Unit.Default);
            }
            

        }
    }
}