using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResultStar : BaseUI
{
    enum Images
    {
        ClearStar,
        NonClearStar,
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
    }

    public void UnSetClearStar()
    {
        GetImage((int)Images.ClearStar).gameObject.SetActive(false);
        GetImage((int)Images.NonClearStar).gameObject.SetActive(true);
    }

    public void SetClearStar()
    {
        GetImage((int)Images.ClearStar).gameObject.SetActive(true);
        GetImage((int)Images.NonClearStar).gameObject.SetActive(false);
    }

}
