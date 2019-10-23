using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UncleWilfred
{
    public class PanelConfirmationView : MonoBehaviour
    {
        public Button beginnerBtn, testBtn;

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