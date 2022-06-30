using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Managers
{
    public enum UIList
    {
        TitleUI,
        LobbyUI,
        SingleGameLobbyUI,
        SingleGameUI,
        BossStageUI,
    }
    public enum SubUIList
    {
        CharacterSlot,
        NormalItemSlot,
        RareItemSlot,
        UniqueItemSlot,
        NormalItemShopSlot,
        RareItemShopSlot,
        UniqueItemShopSlot,
        TradeItemSlot,
    }

    class UIManager : Manager<UIManager>
    {
        static int _order = 10;
        Dictionary<UIList, BaseUI> UIDic = new Dictionary<UIList, BaseUI>();
        Stack<PopupUI> _popupStack = new Stack<PopupUI>();

        private const string SceneUIPath = "UIPrefabs/SceneUI/";
        private const string PopupUIPath = "UIPrefabs/PopupUI/";
        private const string SubUIPath = "UIPrefabs/SubUI/";

        private const string ItemIconPath = "Sprites/ItemIcon/";
        private const string ItemTypeIconPath = "Sprites/ItemTypeIcon/";
        private const string JobIconPath = "Sprites/JobIcon/";
        private const string CharacterIconPath = "Sprites/CharacterIcon/";
        private const string CharacterPopupIconPath = "Sprites/CharacterPopupIcon/";

        public void SetCanvas(GameObject go,bool sort = true)
        {
            Canvas canvas = Utils.GetOrAddComponent<Canvas>(go);
            SceneUI sceneUI = go.GetComponent<SceneUI>();
            if(sceneUI != null)
            {
                if (sceneUI.CurrentUIList == UIList.SingleGameLobbyUI)
                {
                    canvas.renderMode = RenderMode.ScreenSpaceCamera;
                    canvas.worldCamera = Camera.main;
                }
            }
            else
            {
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.overrideSorting = true;
            }

            if (sort)
            {
                _order++;
                canvas.sortingOrder = _order;
            }
            else
            {
                canvas.sortingOrder = 0;
            }
        }

        public void FadeIn(float targetTime = 1.0f)
        {
            FadePopupUI Fade = ShowPopupUI<FadePopupUI>();
            if (Fade != null)
                Fade.FadeIn(targetTime);
        }
        public void FadeOut(float targetTime = 1.0f)
        {
            FadePopupUI Fade = ShowPopupUI<FadePopupUI>();
            if (Fade != null)
                Fade.FadeOut(targetTime);
        }
        public void Add<T>(UIList ui, bool active = true) where T : BaseUI
        {
            if (Constains(ui) == false)
            {
                GameObject go = ResourcesManager.Instance.Instantiate(SceneUIPath + ui);
                T newObject = go.GetComponent<T>();
                newObject.Init();
                newObject.gameObject.SetActive(active);
                newObject.transform.SetParent(this.transform);
                UIDic.Add(ui, newObject);
            }
            else
            {
                UIDic[ui].gameObject.SetActive(true);
                UIDic[ui].Init();
            }
        }

        public void Add(UIList ui, BaseUI baseUI)
        {
            if (Constains(ui) == false)
            {
                UIDic.Add(ui, baseUI);
            }
        }

        public T Get<T>(UIList ui) where T : BaseUI
        {
            if (Constains(ui))
                return UIDic[ui] as T;
            return null;
        }

        public T SubUILoad<T>(SubUIList ui,Transform parent) where T : BaseUI
        {
            GameObject go = ResourcesManager.Instance.Instantiate(SubUIPath + ui, parent);
            if(go == null)
            {
                Debug.Log("Load SubUI Failed");
                return null;
            }
            T newObject = go.GetComponent<T>();
            newObject.Init();
            return newObject;
        }

        public IItemSlot LoadItemSlot(Transform parent,int itemID = 0)
        {
            if (ItemDataManager.Instance.CheckContains(itemID) == false)
                return null;
            string rarity = ItemDataManager.Instance.GetString(itemID, ItemData.RARITY);
            GameObject go = null;
            switch (rarity)
            {
                case "Normal":
                    go = ResourcesManager.Instance.Instantiate(SubUIPath + SubUIList.NormalItemSlot, parent);
                    break;
                case "Rare":
                    go = ResourcesManager.Instance.Instantiate(SubUIPath + SubUIList.RareItemSlot, parent);
                    break;
                case "Unique":
                    go = ResourcesManager.Instance.Instantiate(SubUIPath + SubUIList.UniqueItemSlot, parent);
                    break;
            }
            if (go == null)
            {
                Debug.Log("Load SubUI Failed");
                return null;
            }
            IItemSlot newObject = go.GetComponent<IItemSlot>();
            newObject.IInit(itemID);
            return newObject;
        }

        public IItemShopSlot LoadItemShopSlot(Transform parent, int itemID = 0)
        {
            if (ItemDataManager.Instance.CheckContains(itemID) == false)
                return null;
            string rarity = ItemDataManager.Instance.GetString(itemID, ItemData.RARITY);
            GameObject go = null;
            switch (rarity)
            {
                case "Normal":
                    go = ResourcesManager.Instance.Instantiate(SubUIPath + SubUIList.NormalItemShopSlot, parent);
                    break;
                case "Rare":
                    go = ResourcesManager.Instance.Instantiate(SubUIPath + SubUIList.RareItemShopSlot, parent);
                    break;
                case "Unique":
                    go = ResourcesManager.Instance.Instantiate(SubUIPath + SubUIList.UniqueItemShopSlot, parent);
                    break;
            }
            if (go == null)
            {
                Debug.Log("Load SubUI Failed");
                return null;
            }
            IItemShopSlot newObject = go.GetComponent<IItemShopSlot>();
            newObject.IInit(itemID);
            return newObject;
        }

        public bool Constains(UIList ui)
        {
            if (UIDic.ContainsKey(ui))
                return true;
            return false;
        }

        //Scene을 열어주는 코드
        //public T ShowSceneUI<T>(string name = null) where T : SceneUI
        //{
        //    if (string.IsNullOrEmpty(name))
        //        name = typeof(T).Name;

        //    GameObject go = Managers.ResourcesManager.Instance.Instantiate(SceneUIPath + name);
        //    T scene = Utils.GetOrAddComponent<T>(go);
        //    //sceneUI = scene;
        //    //sceneUI.Init();
        //    //go.transform.SetParent(Root.transform);
        //    return scene;
        //}
        //Popup을 열어주는 코드
        public T ShowPopupUI<T>(string name = null,Transform parent = null) where T : PopupUI
        {
            if (string.IsNullOrEmpty(name))
                name = typeof(T).Name;
            GameObject go = Managers.ResourcesManager.Instance.Instantiate(PopupUIPath + name);
            T popup = Utils.GetOrAddComponent<T>(go);
            popup.Init();
            if (parent == null)
                go.transform.SetParent(this.transform);
            else
                go.transform.SetParent(parent);
            _popupStack.Push(popup);
            //GameAudioManager.Instance.Play2DSound("PopupOpen");
            return popup;    
        }

        public void ClosePopupUI(PopupUI popup)
        {
            if (_popupStack.Count == 0)
                return;
            if (_popupStack.Pop() != popup)
            {
                Debug.Log("Close Popup Failed");
            }
            ResourcesManager.Instance.Destroy(popup.gameObject);
            _order--;
            //popup = null;
            //ClosePopupUI();
        }

        public void CloseSceneUI(UIList ui)
        {
            if (Constains(ui))
            {
                UIDic[ui].Close();
                _order = 10;
            }
        }

        public void ClosePopupUI()
        {
            if (_popupStack.Count == 0)
                return;
            PopupUI popup = _popupStack.Pop();
            if (popup == null)
                return;
            ResourcesManager.Instance.Destroy(popup.gameObject);
            popup = null;

            _order--;
        }
        public void CloseAllPopupUI()
        {
            while(_popupStack.Count > 0)
            {
                ClosePopupUI();
            }
        }

        public Sprite LoadItemIcon(string name) { return Resources.Load<Sprite>(ItemIconPath + name); }
        public Sprite LoadItemtypeIcon(string name) { return Resources.Load<Sprite>(ItemTypeIconPath + name); }
        public Sprite LoadJobIcon(string name) { return Resources.Load<Sprite>(JobIconPath + name); }
        public Sprite LoadCharacterIcon(string name) { return Resources.Load<Sprite>(CharacterIconPath + name); }
        public Sprite LoadCharacterPopupIcon(string name) { return Resources.Load<Sprite>(CharacterPopupIconPath + $"{name}CharacterIcon"); }
    }

}
