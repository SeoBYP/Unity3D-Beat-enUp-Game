using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class GemShopPopup : BaseUI
{
    enum Buttons
    {
        GemList1BuyBtn,
        GemList2BuyBtn,
        GemList3BuyBtn,
        GemList4BuyBtn,
        GemList5BuyBtn,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.GemList1BuyBtn).gameObject, OnBuy1BtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.GemList2BuyBtn).gameObject, OnBuy2BtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.GemList3BuyBtn).gameObject, OnBuy3BtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.GemList4BuyBtn).gameObject, OnBuy4BtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.GemList5BuyBtn).gameObject, OnBuy5BtnClicked, Define.UIEvents.Click);
    }

    private void OnBuy1BtnClicked(PointerEventData data)
    {
        Text text = GetButton((int)Buttons.GemList1BuyBtn).GetComponentInChildren<Text>();
        print("Buy : " + text.text);
    }
    private void OnBuy2BtnClicked(PointerEventData data)
    {
        Text text = GetButton((int)Buttons.GemList2BuyBtn).GetComponentInChildren<Text>();
        print("Buy : " + text.text);
    }
    private void OnBuy3BtnClicked(PointerEventData data)
    {
        Text text = GetButton((int)Buttons.GemList3BuyBtn).GetComponentInChildren<Text>();
        print("Buy : " + text.text);
    }
    private void OnBuy4BtnClicked(PointerEventData data)
    {
        Text text = GetButton((int)Buttons.GemList4BuyBtn).GetComponentInChildren<Text>();
        print("Buy : " + text.text);
    }
    private void OnBuy5BtnClicked(PointerEventData data)
    {
        Text text = GetButton((int)Buttons.GemList5BuyBtn).GetComponentInChildren<Text>();
        print("Buy : " + text.text);
    }
    public void SetActive(bool state)
    {
        this.gameObject.SetActive(state);
    }
}
