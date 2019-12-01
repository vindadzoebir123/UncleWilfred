using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace UncleWilfred
{
    public class PanelLevel1Menu : Menu<PanelLevel1Menu>
    {
        public PanelLevel1View view;

        public override void Initialize()
        {
            view.Init();
        }

        public static void Show()
        {
            Open();
            Instance.view.Open();
        }

        public static void Hide()
        {
            Close();
            Instance.view.Close();
        }

        public override void OnBackPressed()
        {
            Hide();
        }
    }
}