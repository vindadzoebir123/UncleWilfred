using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System.Linq;

namespace UncleWilfred
{
    public class MatchingPanel1 : MonoBehaviour
    {
        public Animator animator;
        public AnimationHelper animatorHelper;

        public GameObject questionParent;
        public List<Level1Item> level1Items;

        List<Sprite> spriteList = new List<Sprite>();
        List<string> textList = new List<string>();
        List<Question> questions = new List<Question>();

        public void Init(List<Question> input)
        {
            questions = input;
            animator.enabled = true;
            ScrambleSprites();
            ScrambleText();
            animatorHelper.onFinish = delegate{
                animator.enabled = false;
                ViewQuestions();
            };
        }

        void ViewQuestions()
        {
            questionParent.SetActive(true);
            int count = level1Items.Count;
            for(int i=0;i<count;i++)
            {
                level1Items[i].Init(spriteList[i], textList[i]);
            }
        }

        void ScrambleSprites()
        {
            spriteList.Clear();

            for(int i=0;i<questions.Count;i++)
            {
                spriteList.Add(questions[i].sprite);
            }

            spriteList = spriteList.OrderBy( x => UnityEngine.Random.value ).ToList( );
        }

        void ScrambleText()
        {
            textList.Clear();

            for(int i=0;i<questions.Count;i++)
            {
                textList.Add(questions[i].text);
            }

            textList = textList.OrderBy(x => UnityEngine.Random.value).ToList( );
        }
        
    }
}