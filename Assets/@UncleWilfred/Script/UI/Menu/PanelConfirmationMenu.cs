using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace UncleWilfred
{
    public class PanelConfirmationMenu : Menu<PanelConfirmationMenu>
    {
        public PanelConfirmationView view;
        
        public override void Initialize()
        {
            view.Init();

            view.beginnerBtn.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                //call play window here
            });

            view.testBtn.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                //call setting here
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