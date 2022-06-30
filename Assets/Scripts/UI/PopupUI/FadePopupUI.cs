using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadePopupUI : PopupUI,IUpdate
{
    enum Images
    {
        Fade,
    }
    private float _elaped = 0;
    private float _speed = 1;
    private static Color _black = Color.black;
    private static Color _blackAlpha = new Color(0, 0, 0, 0);
    private Color start;
    private Color end;
    public override void Init()
    {
        base.Init();
        Bind<Image>(typeof(Images));
    }

    public void FadeIn(float speed)
    {
        gameObject.SetActive(true);
        GetImage((int)Images.Fade).color = _black;
        this._speed = speed;
        start = _black;
        end = _blackAlpha;
        _elaped = 0;

        Managers.UpdateManager.Instance.Listener(this.gameObject);
    }
    public void FadeOut(float speed)
    {
        gameObject.SetActive(true);
        GetImage((int)Images.Fade).color = _blackAlpha;
        this._speed = speed;
        start = _blackAlpha;
        end = _black;
        _elaped = 0;

        Managers.UpdateManager.Instance.Listener(this.gameObject);
    }

    public void OnUpdate()
    {
        _elaped += Time.deltaTime / _speed;
        _elaped = Mathf.Clamp01(_elaped);
        Color color = Color.Lerp(start, end, _elaped);
        GetImage((int)Images.Fade).color = color;
        if(_elaped >= 1.0f)
        {
            if (color.Equals(_blackAlpha))
            {
                ClosePopupUI();
            }
        }
    }
    public override void ClosePopupUI()
    {
        Managers.UpdateManager.Instance.DeleteListener(this.gameObject);
        base.ClosePopupUI();
    }

    public void OnKnockBack()
    {
        throw new System.NotImplementedException();
    }
}
