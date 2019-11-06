using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace UncleWilfred
{
    public class PanelPlacementTestMenu : Menu<PanelPlacementTestMenu>
    {
        public PanelPlacementTestView view;

        public override void Initialize()
        {
            view.Init();

            view.btnYes.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                view.OnYesSelected();
            });

            view.btnNo.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                view.OnNoSelected();
            });
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