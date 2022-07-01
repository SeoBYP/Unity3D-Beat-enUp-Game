using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class GoldShopPopup : BaseUI
{
    enum Buttons
    {
        GoldList1BuyBtn,
        GoldList2BuyBtn,
        GoldList3BuyBtn,
        GoldList4BuyBtn,
        GoldList5BuyBtn,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.GoldList1BuyBtn).gameObject, OnBuy1BtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.GoldList2BuyBtn).gameObject, OnBuy2BtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.GoldList3BuyBtn).gameObject, OnBuy3BtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.GoldList4BuyBtn).gameObject, OnBuy4BtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.GoldList5BuyBtn).gameObject, OnBuy5BtnClicked, Define.UIEvents.Click);
    }

    private void OnBuy1BtnClicked(PointerEventData data)
    {
        Text text = GetButton((int)Buttons.GoldList1BuyBtn).GetComponentInChildren<Text>();
        print("Buy : " + text.text);
    }
    private void OnBuy2BtnClicked(PointerEventData data)
    {
        Text text = GetButton((int)Buttons.GoldList2BuyBtn).GetComponentInChildren<Text>();
        print("Buy : " + text.text);
    }
    private void OnBuy3BtnClicked(PointerEventData data)
    {
        Text text = GetButton((int)Buttons.GoldList3BuyBtn).GetComponentInChildren<Text>();
        print("Buy : " + text.text);
    }
    private void OnBuy4BtnClicked(PointerEventData data)
    {
        Text text = GetButton((int)Buttons.GoldList4BuyBtn).GetComponentInChildren<Text>();
        print("Buy : " + text.text);
    }
    private void OnBuy5BtnClicked(PointerEventData data)
    {
        Text text = GetButton((int)Buttons.GoldList5BuyBtn).GetComponentInChildren<Text>();
        print("Buy : " + text.text);
    }
    public void SetActive(bool state)
    {
        this.gameObject.SetActive(state);
    }
}
