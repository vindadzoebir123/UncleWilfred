using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace UncleWilfred
{
    public class LanguageSelection : MonoBehaviour
    {
        public Button arabicBtn, chineseBtn, hungaryBtn, indonesianBtn;

        Subject<LanguageSetting> onSelectLanguage;
        public IObservable<LanguageSetting> OnSelectLanguageAsObservable()
        {
            return onSelectLanguage ?? (onSelectLanguage = new Subject<LanguageSetting>());
        }

        public void Init()
        {
            arabicBtn.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                SelectLanguage(LanguageSetting.Arabic);
            });

            chineseBtn.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                SelectLanguage(LanguageSetting.Chinese);
            });

            hungaryBtn.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                SelectLanguage(LanguageSetting.Other);
            });

            indonesianBtn.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(x => {
                SelectLanguage(LanguageSetting.Other);
            });

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            if(gameObject.activeSelf)
                gameObject.SetActive(false);
        }

        void SelectLanguage(LanguageSetting setting)
        {
            if(onSelectLanguage!=null)
                onSelectLanguage.OnNext(setting);
        }
    }

    public enum LanguageSetting
    {
        Arabic,
        Chinese,
        Other
    }
}