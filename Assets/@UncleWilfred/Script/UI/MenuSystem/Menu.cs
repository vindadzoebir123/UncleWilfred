using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UncleWilfred
{
    public abstract class Menu<T> : Menu where T : Menu<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            Instance = (T) this;
        }

        protected virtual void OnDestroy()
        {
            Instance = null;
        }

        protected static void Open()
        {
            if (Instance == null)
            {
                MenuManager.Instance.CreateInstance<T>();
                Instance.Initialize();
            }
            
            MenuManager.Instance.OpenMenu(Instance);
        }

        protected static void Close()
        {
            if (Instance == null)
            {
                return;
            }
            
            MenuManager.Instance.CloseMenu(Instance);
        }

        public override void OnBackPressed()
        {
//            Close();
        }
    }

    public abstract class Menu : MonoBehaviour
    {
        public bool isPopup = false;

        public abstract void OnBackPressed();
        public abstract void Initialize();
    }
}