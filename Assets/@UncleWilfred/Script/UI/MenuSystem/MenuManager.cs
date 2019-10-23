using System;
using System.Collections.Generic;
using UnityEngine;

namespace UncleWilfred
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField]private Stack<Menu> menuPopup = new Stack<Menu>();

        [SerializeField] private Menu currentPanel;
//        [SerializeField]private Stack<Menu> menuPanel = new Stack<Menu>();
        public Transform panelParent, popupParent;
        
        public static MenuManager Instance { get; set; }

        private void Awake()
        {
            Instance = this;
        }
        
        void Start()
        {
            PanelTitleMenu.Show();
            // PanelMainMenu.Show();

        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public void CreateInstance<T>() where T : Menu
        {
            var prefab = GetPrefab<T>();

            if (prefab.isPopup)
                Instantiate(prefab, popupParent);
            else
            {
                Instantiate(prefab, panelParent);
            }
        }

        public void OpenMenu(Menu instance)
        {
            if (instance.isPopup)
            {
                menuPopup.Push(instance);
                instance.transform.SetAsLastSibling();
                instance.gameObject.SetActive(true);
            }
            else
            {
//                menuPanel.Push(instance);
//                Menu last = menuPanel.Peek();
//                last.gameObject.SetActive(false);
//                menuPanel.Pop();
//                instance.gameObject.SetActive(true);
                if(currentPanel!=null)
                    currentPanel.gameObject.SetActive(false);
                
                currentPanel = instance;
                instance.gameObject.SetActive(true);
//                Debug.Log("<color=blue>OPEN : " + currentPanel.name + "</color>");

            }
//            if (menuPopup.Count > 0)
//            {
//                if (!instance.isPopup)
//                {
//                    Menu last = menuPopup.Peek();
//                    last.gameObject.SetActive(false);
//                    menuPopup.Pop();
//                }
//            }
//            
//            menuPopup.Push(instance);
//            instance.transform.SetAsLastSibling();
//            instance.gameObject.SetActive(true);
        }

        private T GetPrefab<T>() where T : Menu
        {
            T prefab = Resources.Load<T>(typeof(T).Name);

            if (prefab != null)
                return prefab;
            else
            {
                throw new MissingFieldException("Prefab not found for type " + typeof(T));
            }
        }

        public void CloseMenu(Menu menu)
        {
            if (menuPopup.Count > 0)
            {
                if(menuPopup.Peek()!=menu)
                    return;
                
                CloseTopMenu();
            }

            if (currentPanel != menu)
                return;

//            if (!menu.isPopup)
//            {
//                if (menuPanel.Count == 0 || menuPanel.Peek() != menu)
//                {
//                    return;
//                }
//            }
//            else
//            {
//                if(menuPopup.Count == 0 || menuPopup.Peek()!=menu)
//                    return;
//                    
//            }
            CloseTopMenu();
        }

        private void CloseTopMenu()
        {
            if (menuPopup.Count > 0)
            {
                var instance = menuPopup.Pop();
                instance.gameObject.SetActive(false);
            }
            else
            {
//                Debug.Log("<color=blue>CLOSE : " + currentPanel.name + "</color>");
   
                currentPanel.gameObject.SetActive(false);
                currentPanel = null;
            }
            
//            var instance = menuPopup.Pop();
//            instance.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(menuPopup.Count> 0)
                    menuPopup.Peek().OnBackPressed();
                else
                {
                    currentPanel.OnBackPressed();
                }
            }
        }
    }
}