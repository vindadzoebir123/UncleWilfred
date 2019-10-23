using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UncleWilfred
{
    public class PanelTitleView : MonoBehaviour
    {
        public Button playBtn, settingBtn, exitBtn;

        public void Init()
        {
            Close();
        }

        
        public void Open()
        {
            if(!gameObject.activeSelf)
                gameObject.SetActive(true);
        }

        public void Close()
        {
            if(gameObject.activeSelf)
                gameObject.SetActive(false);
        }
    }
}