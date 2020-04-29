using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace UncleWilfred.Phonethemes
{
    public class WorldPanel : MonoBehaviour
    {
        [SerializeField]
        private Button backBtn;

        [SerializeField]
        private WordsScriptable database;

        [SerializeField]
        private TextAnim textObj;
        [SerializeField]
        private Transform textParent, spawnPoint;
        [SerializeField]
        private float delayWords = 0.2f;

        private List<string> words = new List<string>();
        private Vector2 screenWorldPoint;

        public void Init()
        {
            // backBtn.gameObject.SetActive(false);
            backBtn.OnClickAsObservable().TakeUntilDisable(this).Subscribe(X => {
                Hide();

                ClearWordsParent();
            });

            words.AddRange(database.words);

            Show();
            screenWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        }

        void Show()
        {
            if(!gameObject.activeSelf)
                gameObject.SetActive(true);

        }

        void Hide()
        {
            if(gameObject.activeSelf)
                gameObject.SetActive(false);
        }

        public void OnShowAnimationFinished()
        {
            StartCoroutine(GenerateWords());
        }

        IEnumerator GenerateWords()
        {
            Debug.Log("Generate word in worlds");
            while(words.Count>0)
            {
                TextAnim item = Instantiate(textObj);
                item.transform.SetParent(textParent);
                item.transform.position = new Vector2(Random.Range(-screenWorldPoint.x, screenWorldPoint.x), spawnPoint.position.y);
                item.Init(words[0]);
                words.RemoveAt(0);

                yield return new WaitForSeconds(delayWords);
            }

            // backBtn.gameObject.SetActive(true);
        }

        void ClearWordsParent()
        {
            foreach(Transform child in textParent)
            {
                Destroy(child.gameObject);
            }
        }
    }
}