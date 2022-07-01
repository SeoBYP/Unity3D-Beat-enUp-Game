using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
namespace Managers
{
    public enum Scene
    {
        Title,
        Lobby,
        SingleGameLobby,
        SingleGame,
        MultiGame,
        BossStage,
    }
    class SceneManagerEx : Manager<SceneManagerEx>
    {
        public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

        public void LoadScene(Scene scene)
        {
            StartCoroutine(GameScene(scene));
            //UIManager.Instance.ShowPopupUI<LoadingScenePopupUI>().StartLoadSceneAsync(GetSceneName(scene));
        }

        public void LoadGameScene()
        {

        }

        IEnumerator GameScene(Scene scene)
        {
            UIManager.Instance.FadeOut();
            yield return new WaitForSeconds(1.1f);
            CurrentScene.Clear();
            UIManager.Instance.ShowPopupUI<LoadingScenePopupUI>().StartLoadSceneAsync(GetSceneName(scene));
        }

        string GetSceneName(Scene scene)
        {
            string name = System.Enum.GetName(typeof(Scene), scene);
            return name;
        }
    }

    public abstract class BaseScene : MonoBehaviour
    {
        public Scene sceneType { get; protected set; } = Scene.Title;

        private void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
            if (obj == null)
                ResourcesManager.Instance.Instantiate("EventSystem").name = "@EventSystem";
        }
        public abstract void Clear();
    }
}

