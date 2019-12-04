using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

namespace UncleWilfred
{
    public class LevelDragText : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        public TextMeshProUGUI text;
        string answer;
        public void Init(string answer)
        {
            this.answer = answer;
            text.text = answer;
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
            Debug.Log("Pointer : " + eventData.pointerEnter.name);
            if(eventData.pointerEnter.GetComponent<Level1Item>()!=null)
            {
                Level1Item item = eventData.pointerEnter.GetComponent<Level1Item>();
                if(item.CheckAnswer(answer))
                {
                    gameObject.SetActive(false);
                    Debug.Log("Deactivate game object");
                    return;
                }
            }
            Return();
        }

        void Return()
        {
            transform.localPosition = Vector3.zero;
            transform.GetComponent<Image>().raycastTarget = true;
        }

        // void Update()
        // {
            
        // }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("Droping on : " + eventData.pointerEnter.name);
            // if(eventData.pointerDrag.GetComponent<Level1Item>()!=null)
            // {

            // }
            // else
            // {
            //     // transform.SetParent(initParent, false);
            //     // transform.localPosition = initPos;
            // }
        }
    }
}