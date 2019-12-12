using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System;
using DG.Tweening;

namespace UncleWilfred
{
    public class LevelDragText : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        public TextMeshProUGUI text;
        string answer;
        public Action onDropOnTarget;

        public void Init(string answer)
        {
            this.answer = answer;
            text.text = answer;
            text.color = Color.white;
            Return();
            gameObject.SetActive(true);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            transform.position += (Vector3) eventData.delta;
            transform.GetComponent<Image>().raycastTarget = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // Debug.Log("Pointer : " + eventData.pointerEnter.name);
            if(eventData.pointerEnter == null)
                Return();

            if(eventData.pointerEnter.tag == "Image")
            {
                Level1Item item = eventData.pointerEnter.GetComponent<Level1Item>();
                if(item.CheckAnswer(answer))
                {
                    gameObject.SetActive(false);
                    Debug.Log("Deactivate game object");
                }
                else
                    WrongAnswer();
                
                return;
            }
            else if(eventData.pointerEnter.tag == "Sentence")
            {
                if(onDropOnTarget!=null)
                {
                    onDropOnTarget();
                    return;
                }
            }
            Return();
        }

        public void WrongAnswer()
        {
            text.color = Color.red;
            transform.DOPunchPosition(new Vector2(5f, 5f), 0.2f).OnComplete(delegate{
                Return();
                text.color = Color.white;
            });
        }

        public void Return()
        {
            transform.localPosition = Vector3.zero;
            transform.GetComponent<Image>().raycastTarget = true;
        }
    }
}