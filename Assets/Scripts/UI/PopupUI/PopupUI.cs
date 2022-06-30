using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUI : BaseUI
{
    public override void Init()
    {
        Managers.UIManager.Instance.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopupUI()
    {
        Managers.UIManager.Instance.ClosePopupUI(this);
    }
}
