using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    class GameAudioManager : Manager<GameAudioManager>,IUpdate
    {
        private AudioSource _backGround;
        private List<AudioSource> effectAudioList = new List<AudioSource>();
        private Dictionary<string, AudioClip> clipDic = new Dictionary<string, AudioClip>();

        private int _limitCount = 10;
        private float _intervalTime = 2;
        private float prevTime = 0;

        public float BGMSound { get; private set; }
        public float EffectSound { get; private set; }

        public override void Init()
        {
            BGMSound = DataManager.Instance.GetPlayer(1).PlayerInfo.BGMSound;
            EffectSound = DataManager.Instance.GetPlayer(1).PlayerInfo.EffectSound;
            _backGround = gameObject.GetOrAddComponent<AudioSource>();
            _backGround.spatialBlend = 0;
            _backGround.volume = 1.0f;
            _backGround.playOnAwake = false;

            UpdateManager.Instance.Listener(this.gameObject);
        }

        public void PlayBackGround(string name)
        {
            if (clipDic.ContainsKey(name))
            {
                _backGround.clip = clipDic[name];
                _backGround.volume = BGMSound;
                _backGround.loop = true;
                _backGround.Play();
            }
        }

        AudioSource Pooling()
        {
            AudioSource audioSource = null;
            for (int i = 0; i < effectAudioList.Count; i++)
            {
                if (effectAudioList[i].gameObject.activeSelf == false)
                {
                    audioSource = effectAudioList[i];
                    audioSource.gameObject.SetActive(true);
                    break;
                }
            }
            if (audioSource == null)
            {
                audioSource = Utils.GreateObject<AudioSource>(transform);
                effectAudioList.Add(audioSource);
            }
            return audioSource;
        }

        IEnumerator IDeactiveAudio(AudioSource audio)
        {
            yield return new WaitForSeconds(audio.clip.length);
            audio.gameObject.SetActive(false);
        }

        private void Play(string name, float spatialBlend, Vector3 position)
        {
            if (name == null || clipDic.ContainsKey(name) == false)
            {
                Debug.Log("No Sound In clipDic : " + name);
                return;
            }
            AudioSource audioSource = Pooling();
            audioSource.clip = clipDic[name];
            audioSource.spatialBlend = spatialBlend;
            audioSource.volume = EffectSound;
            audioSource.Play();
            StartCoroutine(IDeactiveAudio(audioSource));
        }

        public void Play2DSound(string name)
        {
            Play(name, 0, Vector3.zero);
        }

        public void LoadSound()
        {
            AudioClip[] audioClips = Resources.LoadAll<AudioClip>("Sound");
            for (int i = 0; i < audioClips.Length; i++)
            {
                if (clipDic.ContainsKey(audioClips[i].name) == false)
                {
                    clipDic.Add(audioClips[i].name, audioClips[i]);
                }
            }
        }

        public void SetBGMSound(float value)
        {
            BGMSound = value / 100;
            if (_backGround != null)
                _backGround.volume = BGMSound;
        }
        public void SetEffectSound(float value) { EffectSound = value / 100; }
        public void SaveSounds() { DataManager.Instance.GetPlayer(1).PlayerInfo.SetSounds(BGMSound, EffectSound); }

        public void OnUpdate()
        {
            if (effectAudioList.Count > _limitCount)
            {
                float elapsedTime = Time.time - prevTime;
                if (elapsedTime > _intervalTime)
                {
                    for (int i = 0; i < effectAudioList.Count; i++)
                    {
                        if (effectAudioList[i].gameObject.activeSelf == false)
                        {
                            AudioSource audioSource = effectAudioList[i];
                            effectAudioList.RemoveAt(i);
                            prevTime = Time.time;
                            Destroy(audioSource.gameObject);
                            return;
                        }
                    }
                }
            }
        }
    }
}

