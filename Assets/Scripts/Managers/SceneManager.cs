using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;          // ����Ƽ�� ���Ŵ����� �̸��� ��ġ�� ��� �����뵵

public class SceneManager : MonoBehaviour
{
    private LoadingUI loadingUI;
    private BaseScene curScene;      //������ �ϳ��� �� ������Ʈ�� �������̹Ƿ� ������Ƽ�� get�� �Ἥ ���ӿ�����Ʈ���� �������� ��.

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

        
        // oper.allowSceneActivation = false;          //�ε��� �ٵǾ ����ȯ�� ��������.
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

