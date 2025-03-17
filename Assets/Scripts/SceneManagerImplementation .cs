// SceneManagerImplementation.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneManagerImplementation : MonoBehaviour, ISceneManager
{
    // シーンを同期的にロード
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // 非同期でシーンをロード
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }

    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;  // ローディングが完了するまで待機
        }
    }

    // シーンをアンロード
    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    // 現在のシーンを再読み込み
    public void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
