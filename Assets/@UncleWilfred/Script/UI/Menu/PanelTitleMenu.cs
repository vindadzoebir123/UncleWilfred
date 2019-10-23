using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace UncleWilfred
{
    public class PanelTitleMenu : Menu<PanelTitleMenu>
    {
        public PanelTitleView view;
        
        public override void Initialize()
        {
            view.Init();

            view.playBtn.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                PanelConfirmationMenu.Show();
                //call play window here
            });

            view.settingBtn.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                //call setting here
            });

            view.exitBtn.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                Application.Quit();
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