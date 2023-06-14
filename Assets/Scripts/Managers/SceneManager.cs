using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;          // 유니티의 씬매니저와 이름이 겹치는 경우 방지용도

public class SceneManager : MonoBehaviour
{
    private LoadingUI loadingUI;
    private BaseScene curScene;      //씬별로 하나의 씬 컴포넌트만 가질것이므로 프로퍼티에 get을 써서 게임오브젝트에서 가져오면 됨.

    public BaseScene CurScene
    {
        get
        {
            if(curScene == null)
                curScene = GameObject.FindObjectOfType<BaseScene>();
            
            return GameObject.FindObjectOfType<BaseScene>();
        }
    }

    public void Awake()
    {
        LoadingUI ui = Resources.Load<LoadingUI>("UI/LoadingUI");
        this.loadingUI = Instantiate(ui);
        this.loadingUI.transform.SetParent(transform, false);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadingRoutine(sceneName));
    }

    IEnumerator LoadingRoutine(string sceneName)
    {
        loadingUI.FadeOut();
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0f;

        
        // oper.allowSceneActivation = false;          //로딩이 다되어도 씬전환을 하지않음.
        AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);
        while (!oper.isDone)
        {
            loadingUI.SetProgress(Mathf.Lerp(0f, 0.5f, oper.progress));
            yield return null;
        }

        CurScene.LoadAsync();
        while (CurScene.progress < 1f)
        {
            loadingUI.SetProgress(Mathf.Lerp(0.5f, 1.0f, CurScene.progress));
            yield return null;
        }

       
        Time.timeScale = 1f;
        loadingUI.FadeIn();
        yield return new WaitForSeconds(0.5f);
    }
}

