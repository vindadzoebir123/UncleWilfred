using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace UncleWilfred
{
    public class PanelResultMenu : Menu<PanelResultMenu>
    {
        public PanelResultView view;

        public override void Initialize()
        {
            view.Init();

            view.backBtn.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                PanelTitleMenu.Show();
                //call play window here
            });

           view.nextBtn.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(x => {
               //play game here
               PanelLevel1Menu.Show();
           });
            
        }

        public static void Show(string level)
        {
            Open();
            Instance.view.Open(level);
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