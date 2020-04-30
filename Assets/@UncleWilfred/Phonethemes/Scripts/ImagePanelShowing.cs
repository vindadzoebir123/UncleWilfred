using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace UncleWilfred
{
    public class ImagePanelShowing : MonoBehaviour
    {
        [SerializeField]
        private GameObject imagePanel;
        [SerializeField]
        private Image image;
        [SerializeField]
        private Button closeButton;

        public static ImagePanelShowing Instance;

        void Awake()
        {
            Instance = this;
            closeButton.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                if(imagePanel.activeSelf)
                    imagePanel.SetActive(false);
            });
        }

        public void Show(Sprite sprite)
        {
            image.sprite = sprite;

            if(!imagePanel.activeSelf)
                imagePanel.SetActive(true);
        }
    }
}