using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;

public class SoundOption : BaseUI
{
    enum Buttons
    {
        AlramToggle,
        VibrationToggle,
    }
    enum Sliders
    {
        EffectSlider,
        BGMSlider,
    }
    enum Images
    {
        EffectIcon_On,
        EffectIcon_Off,
        BGMIcon_On,
        BGMIcon_Off,
    }
    enum Texts
    {
        EffectCurrentSoundText,
        BGMCurrentSoundText,
    }

    enum Sounds
    {
        BGM,
        Effect,
    }
    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Slider>(typeof(Sliders));
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));
        BindEvents();

        GetImage((int)Images.BGMIcon_Off).gameObject.SetActive(false);
        GetImage((int)Images.EffectIcon_Off).gameObject.SetActive(false);
    }
    private void BindEvents()
    {
        BindEvent(GetButton((int)Buttons.AlramToggle).gameObject, OnAlramBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.VibrationToggle).gameObject, OnVibrationBtnClicked, Define.UIEvents.Click);
        Get<Slider>((int)Sliders.EffectSlider).onValueChanged.AddListener(delegate { OnEffectSliderValueChanged();});
        Get<Slider>((int)Sliders.BGMSlider).onValueChanged.AddListener(delegate { OnBGMSliderValueChanged();});

        Get<Slider>((int)Sliders.EffectSlider).value = GameAudioManager.Instance.EffectSound;
        Get<Slider>((int)Sliders.BGMSlider).value = GameAudioManager.Instance.BGMSound;
    }
    private void OnAlramBtnClicked(PointerEventData data)
    {
        Vector3 currentVec = GetButton((int)Buttons.AlramToggle).transform.localPosition;
        if(currentVec != Vector3.zero)
        {
            GetButton((int)Buttons.AlramToggle).transform.localPosition += new Vector3(90,0,0);
        }
        else
        {
            GetButton((int)Buttons.AlramToggle).transform.localPosition -= new Vector3(90, 0, 0);
        }
    }
    private void OnVibrationBtnClicked(PointerEventData data)
    {
        Vector3 currentVec = GetButton((int)Buttons.VibrationToggle).transform.localPosition;
        if (currentVec != Vector3.zero)
        {
            GetButton((int)Buttons.VibrationToggle).transform.localPosition += new Vector3(90, 0, 0);
        }
        else
        {
            GetButton((int)Buttons.VibrationToggle).transform.localPosition -= new Vector3(90, 0, 0);
        }
    }
    private void OnEffectSliderValueChanged()
    {
        SetCurrentText(Sliders.EffectSlider, Texts.EffectCurrentSoundText, Images.EffectIcon_On, Images.EffectIcon_Off,Sounds.Effect);
    }
    private void OnBGMSliderValueChanged()
    {
        SetCurrentText(Sliders.BGMSlider, Texts.BGMCurrentSoundText, Images.BGMIcon_On, Images.BGMIcon_Off,Sounds.BGM);
    }

    public void SaveSoundOption()
    {
        GameAudioManager.Instance.SaveSounds();
    }

    private void SetCurrentText(Sliders slider,Texts text, Images OnIcon,Images OffIcon,Sounds sounds)
    {
        Slider temp = Get<Slider>((int)slider);
        float value = temp.value * 100;
        int currentvalue = Mathf.CeilToInt(value);
        if (sounds == Sounds.BGM)
            GameAudioManager.Instance.SetBGMSound(currentvalue);
        if (sounds == Sounds.Effect)
            GameAudioManager.Instance.SetEffectSound(currentvalue);
        GetText((int)text).text = currentvalue.ToString();
        if(temp.value == 0)
        {
            GetImage((int)OnIcon).gameObject.SetActive(false);
            GetImage((int)OffIcon).gameObject.SetActive(true);
        }
        else
        {
            GetImage((int)OnIcon).gameObject.SetActive(true);
            GetImage((int)OffIcon).gameObject.SetActive(false);
        }
    }
}
