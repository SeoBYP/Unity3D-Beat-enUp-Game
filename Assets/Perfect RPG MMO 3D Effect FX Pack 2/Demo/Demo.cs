//====================Copyright statement:AppsTools===================//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Demo : MonoBehaviour
{
    string ss = "arrow/arrow_fx_02.prefab&arrow/arrow_fx_03.prefab&dark/dark_fx_01.prefab&dark/dark_fx_03.prefab&dark/dark_fx_04.prefab&dark/dark_fx_05.prefab&dark/dark_fx_06.prefab&dark/dark_fx_07.prefab&dark/dark_fx_08.prefab&dark/dark_fx_09.prefab&dark/dark_fx_10.prefab&dark/dark_fx_11.prefab&dark/dark_fx_12.prefab&dark/dark_fx_13.prefab&dark/dark_fx_14.prefab&dark/dark_fx_15.prefab&dark/dark_fx_16.prefab&dark/dark_fx_17.prefab&dark/dark_fx_18.prefab&dark/dark_fx_19.prefab&dark/dark_fx_20.prefab&dark/dark_fx_21.prefab&dark/dark_fx_22.prefab&dark/dark_fx_23.prefab&dark/shitouo.prefab&fire/fire_fx_01.prefab&fire/fire_fx_02.prefab&fire/fire_fx_03.prefab&fire/fire_fx_04.prefab&fire/fire_fx_05.prefab&fire/fire_fx_06.prefab&fire/fire_fx_07.prefab&fire/fire_fx_08.prefab&fire/fire_fx_09.prefab&fire/fire_fx_10.prefab&fire/fire_fx_11.prefab&fire/fire_fx_12.prefab&fire/fire_fx_13.prefab&fire/fire_fx_14.prefab&fire/fire_fx_15.prefab&fire/fire_fx_16.prefab&fire/fire_fx_17.prefab&fire/fire_fx_18.prefab&fire/fire_fx_19.prefab&fire/fire_fx_20.prefab&fire/fire_fx_21.prefab&fire/fire_fx_22.prefab&fire/fire_fx_23.prefab&fire/fire_fx_24.prefab&fire/fire_fx_25.prefab&fire/fire_fx_26.prefab&fire/fire_fx_27.prefab&fire/fire_fx_28.prefab&fire/fire_fx_29.prefab&fire/fire_fx_30.prefab&fire/fire_fx_31.prefab&fire/fire_fx_32.prefab&fire/fire_fx_34.prefab&fire/fire_fx_35.prefab&fire/fire_fx_36.prefab&fire/fire_fx_37.prefab&fire/fire_fx_38.prefab&fire/fire_fx_39.prefab&fire/fire_fx_40.prefab&fire/fire_fx_41.prefab&fire/fire_fx_42.prefab&fire/fire_fx_43.prefab&fire/fire_fx_44.prefab&fire/fire_fx_45.prefab&fire/fire_fx_47.prefab&fire/fire_fx_48.prefab&fire/fire_fx_49.prefab&fire/fire_fx_50.prefab&fire/fire_fx_51.prefab&fire/fire_fx_52.prefab&fire/fire_fx_53.prefab&fire/fire_fx_54.prefab&fire/fire_fx_55.prefab&fire/fire_fx_56.prefab&fire/fire_fx_57.prefab&fire/fire_fx_58.prefab&fire/fire_fx_59.prefab&ice/ice_fx_01.prefab&ice/ice_fx_02.prefab&ice/ice_fx_03.prefab&ice/ice_fx_04.prefab&ice/ice_fx_05.prefab&ice/ice_fx_06.prefab&ice/ice_fx_07.prefab&ice/ice_fx_08.prefab&ice/ice_fx_09.prefab&ice/ice_fx_10.prefab&ice/ice_fx_11.prefab&ice/ice_fx_12.prefab&ice/ice_fx_13.prefab&ice/ice_fx_14.prefab&ice/ice_fx_15.prefab&ice/ice_fx_16.prefab&ice/ice_fx_17.prefab&ice/ice_fx_18.prefab&ice/ice_fx_19.prefab&ice/ice_fx_20.prefab&ice/ice_fx_21.prefab&ice/ice_fx_22.prefab&ice/ice_fx_23.prefab&lightning/lightning_fx_01.prefab&lightning/lightning_fx_02.prefab&lightning/lightning_fx_03.prefab&lightning/lightning_fx_04.prefab&lightning/lightning_fx_05.prefab&lightning/lightning_fx_06.prefab&lightning/lightning_fx_07.prefab&lightning/lightning_fx_08.prefab&lightning/lightning_fx_09.prefab&lightning/lightning_fx_10.prefab&lightning/lightning_fx_11.prefab&lightning/lightning_fx_12.prefab&lightning/lightning_fx_13.prefab&sacred/sacred_fx_01.prefab&sacred/sacred_fx_02.prefab&sacred/sacred_fx_03.prefab&sacred/sacred_fx_04.prefab&sacred/sacred_fx_05.prefab&sacred/sacred_fx_06.prefab&sacred/sacred_fx_08.prefab&sacred/sacred_fx_09.prefab&sacred/sacred_fx_10.prefab&sacred/sacred_fx_11.prefab&sacred/sacred_fx_12.prefab&sacred/sacred_fx_13.prefab&sacred/sacred_fx_14.prefab&sacred/sacred_fx_15.prefab&sacred/sacred_fx_16.prefab&sacred/sacred_fx_17.prefab&sacred/sacred_fx_18.prefab&sacred/sacred_fx_19.prefab&sacred/sacred_fx_20.prefab&sacred/sacred_fx_21.prefab&sacred/sacred_fx_22.prefab&sacred/sacred_fx_23.prefab&sacred/sacred_fx_24.prefab&sacred/sacred_fx_25.prefab&sacred/sacred_fx_26.prefab&sacred/sacred_fx_27.prefab&sacred/sacred_fx_28.prefab&sacred/sacred_fx_29.prefab&sacred/sacred_fx_30.prefab&sacred/sacred_fx_31.prefab&sacred/sacred_fx_32.prefab&sacred/sacred_fx_33.prefab&sacred/sacred_fx_34.prefab&sacred/sacred_fx_35.prefab&sacred/sacred_fx_36.prefab&sacred/sacred_fx_37.prefab&sacred/sacred_fx_38.prefab&sacred/sacred_fx_39.prefab&sacred/sacred_fx_40.prefab&sacred/sacred_fx_41.prefab&sacred/sacred_fx_42.prefab&toxin/toxin_fx_01.prefab&toxin/toxin_fx_02.prefab&toxin/toxin_fx_03.prefab&toxin/toxin_fx_04.prefab&toxin/toxin_fx_05.prefab&toxin/toxin_fx_06.prefab&toxin/toxin_fx_07.prefab&warning/fire_fx_33.prefab&warning/warning_fx_01.prefab&warning/warning_fx_02.prefab&warning/warning_fx_03.prefab&warning/warning_fx_04.prefab&warning/warning_fx_05.prefab&warning/warning_fx_06.prefab&warning/warning_fx_07.prefab&warning/warning_fx_08.prefab";
    private bool r = false;
    string[] allArray = null;

    public int i = 0;
    public UnityEngine.UI.Text tex;
    public Transform ts;
    private GameObject currObj;

    public void Awake()
    {

        /*string st2322r = "";
        var allFiles = Directory.GetFiles(Application.dataPath + "/Perfect RPG MMO 3D Effect FX Pack 2/Effect/Prefab/Resources", "*.prefab", SearchOption.AllDirectories);
        for (int i = 0; i < allFiles.Length; i++)
        {
            var str = Application.dataPath + "/Perfect RPG MMO 3D Effect FX Pack 2/Effect/Prefab/Resources/";
            allFiles[i] = allFiles[i].Replace(@"\", "/").Replace(str.Replace(@"\", "/"), "");
            st2322r += allFiles[i] + "&";

        }
        Debug.LogError(st2322r);
        return;*/


        allArray = ss.Split('&');
        currObj = GameObject.Instantiate(Resources.Load(allArray[i].ToLower().Replace(".prefab", "")) as GameObject);
        currObj.transform.SetParent(ts);
        currObj.transform.localPosition = Vector3.zero;
        tex.text = "Name: "+ i +" 【" + allArray[i] + "】";
    }

    public void Update()
    {
        if (ts != null && r)
        {
            ts.transform.Rotate(Vector3.up * Time.deltaTime * 90f);
        }
    }

    public void R()
    {
        r = true;
    }

    public void NotR()
    {
        r = false;
    }

    public void RePlay() 
    {
        if (currObj != null)
        {
            currObj.SetActive(false);
            currObj.SetActive(true);
        }
    }

    public void CopyName() 
    {
        var s = allArray[i].ToLower().Replace(".prefab", "");
        s = s.Substring(s.IndexOf("/")+1);
        UnityEngine.GUIUtility.systemCopyBuffer = s;
    }

    public void OnLeftBtClick() 
    {
        i--;
        if (i <= 0)
        {
            i = allArray.Length - 1;
        }
        if (currObj != null)
        {
            GameObject.DestroyImmediate(currObj);
        }
        currObj = GameObject.Instantiate(Resources.Load(allArray[i].ToLower().Replace(".prefab", "")) as GameObject);
        currObj.transform.SetParent(ts);
        currObj.transform.localPosition = Vector3.zero;
        tex.text = "Name: " + i + " 【" + allArray[i] + "】";
    }

    public void OnRightBtClick()
    {
        i++;
        if (i >= allArray.Length)
        {
            i = 0;
        }
        if (currObj != null)
        {
            GameObject.DestroyImmediate(currObj);
        }
        currObj = GameObject.Instantiate(Resources.Load(allArray[i].ToLower().Replace(".prefab", "")) as GameObject);
        currObj.transform.SetParent(ts);
        currObj.transform.localPosition = Vector3.zero;
        tex.text = "Name: " + i + " 【" + allArray[i] + "】";
    }
}
