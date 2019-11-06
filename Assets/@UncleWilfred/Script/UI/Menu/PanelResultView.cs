using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UncleWilfred
{
    public class PanelResultView : MonoBehaviour
    {
        public Button backBtn, nextBtn;
        public TextMeshProUGUI textLevel;

        int index = 0;

        public void Init()
        {
            Close();
        }

        
        public void Open(string level)
        {
            if(!gameObject.activeSelf)
                gameObject.SetActive(true);

            if(level == "Advance")
            {
                textLevel.text = string.Format("You are <color=#F8CA93>{0}d</color>!", level);
            }
            else
                textLevel.text = string.Format("You are an <color=#F8CA93>{0}</color>!", level);
        }
       
        public void Close()
        {
            if(gameObject.activeSelf)
                gameObject.SetActive(false);
        }
    }
}