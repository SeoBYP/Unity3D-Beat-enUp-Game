using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
public class SceneUI : BaseUI
{
    public UIList CurrentUIList;
    public override void Init()
    {
        UIManager.Instance.SetCanvas(gameObject, false);
    }
}
