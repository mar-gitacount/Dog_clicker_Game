using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMenu : MonoBehaviour
{
    [SerializeField] private SelectMenuButton SelectmenuButtonPrefab;
    // [SerializeField] private SaveLoadManager saveLoadManager; // ← 追加
    SaveLoadManager saveLoadManager;

    // シーン名の配列
    private (string sceneName, string displayName)[] sceneNames = {
        ("TitleScene", "タイトル"),
        ("MainScene", "メイン"),
        // ("ShopScene", "ショップ"),
        ("LoginOrSinUpScene 1", "ログイン")
    };

    void Awake()
    {
        if (saveLoadManager == null)
        {
            Debug.Log("SaveLoadManagerを探しています...");
            saveLoadManager = FindObjectOfType<SaveLoadManager>();
        }
        if (saveLoadManager == null)
        {
            // Debug.LogError("SaveLoadManagerが見つかりません。SelectMenuを正しく機能させるために、シーンにSaveLoadManagerを追加してください。");
            // return;
        }

        foreach (var (sceneName, displayName) in sceneNames)
        {
            var menuButton = Instantiate(SelectmenuButtonPrefab, transform);
            menuButton.SetLabel(displayName);
            menuButton.SetOnClick(() =>
            {
                // saveLoadManager.SaveToCloud(); // ここでセーブ
                SceneManager.LoadScene(sceneName); // シーン遷移
            });
        }
    }
}
