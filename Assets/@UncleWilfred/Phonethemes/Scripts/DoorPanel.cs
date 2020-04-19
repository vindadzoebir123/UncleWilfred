using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace UncleWilfred.Phonethemes
{
    public class DoorPanel : MonoBehaviour
    {
        [SerializeField]
        private Button door1, door2, door3, nextBtn;
        [SerializeField]
        private Animator doorAnim;
        [SerializeField]
        private List<WorldPanel> worldPanel = new List<WorldPanel>();
        [SerializeField]
        private GameObject phase2;

        private GameObject currentSelectedWorld;

        private int countPeeking;

        void Start()
        {
            Init();
            countPeeking = 0;
        }

        void Init()
        {
            door1.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                doorAnim.SetTrigger("1");
            });

            door2.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                doorAnim.SetTrigger("2");
            });

            door3.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                doorAnim.SetTrigger("3");
            });

            nextBtn.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                ShowPhase2();
            });

            nextBtn.gameObject.SetActive(false);

            ResetView();
        }

        void ResetView()
        {
            // door1.gameObject.SetActive(true);
            // door2.gameObject.SetActive(true);
            // door3.gameObject.SetActive(true);
            transform.localScale = Vector3.one;
            transform.localPosition = Vector3.zero;
        }

        public void ShowWorld1()
        {
            worldPanel[0].Init();
            currentSelectedWorld = door1.gameObject;
        }

        public void ShowWorld2()
        {
            worldPanel[1].Init();
            currentSelectedWorld = door2.gameObject;
        }

        public void ShowWorld3()
        {
            worldPanel[2].Init();
            currentSelectedWorld = door3.gameObject;
        }

        public void FinishPeekAnim()
        {
            Debug.Log("Called finish peek anim");
            ResetView();
            currentSelectedWorld.SetActive(false);
            Debug.Log(currentSelectedWorld);
            countPeeking +=1;

            if(countPeeking>=3)
                nextBtn.gameObject.SetActive(true);
            
        }

        void ShowPhase2()
        {
            gameObject.SetActive(false);
            phase2.SetActive(true);
        }
    }
}