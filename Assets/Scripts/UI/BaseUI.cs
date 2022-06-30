using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Managers;
public class Define
{
    public enum UIEvents
    {
        Click,
        Drag,
    }
}

public abstract class BaseUI : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> objectDic = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init();

    //리플렉션을 활용해서,현재 UI들을 objectDic에 넣어준다.
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        if (objectDic.ContainsKey(typeof(T)))
            return;
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        objectDic.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Utils.FindChild(gameObject, names[i], true);
            else
                objects[i] = Utils.FindChild<T>(gameObject, names[i], true);
            if (objects[i] == null)
                Debug.Log("Failed To Bind " + names[i]);
        }
    }

    protected T Get<T>(int index) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (objectDic.TryGetValue(typeof(T), out objects) == false)
        {
            return null;
        }
        return objects[index] as T;
    }

    protected GameObject GetGameObject(int index) { return Get<GameObject>(index); }
    protected Text GetText(int index) { return Get<Text>(index); }
    protected Button GetButton(int index) { return Get<Button>(index); }
    protected Image GetImage(int index) { return Get<Image>(index); }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    public static void BindEvent(GameObject go,Action<PointerEventData> action, Define.UIEvents type = Define.UIEvents.Click)
    {
        EventHandler handler = Utils.GetOrAddComponent<EventHandler>(go);
        switch (type)
        {
            case Define.UIEvents.Click:
                handler.OnClickHandler -= action;
                handler.OnClickHandler += action;
                break;
            case Define.UIEvents.Drag:
                handler.OnDragHandler -= action;
                handler.OnDragHandler += action;
                break;
        }
    }
}

public class EventHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerClickHandler
{
    //추가하고 싶은 이벤트들을 연동시킨다.
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnBeginHandler = null;
    public Action<PointerEventData> OnDragHandler = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickHandler != null)
        {
            GameAudioManager.Instance.Play2DSound("ButtonClicked");
            OnClickHandler.Invoke(eventData);
        }
            
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (OnBeginHandler != null)
            OnClickHandler.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragHandler != null)
            OnClickHandler.Invoke(eventData);
    }

}
