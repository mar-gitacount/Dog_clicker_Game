using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMenu : MonoBehaviour
{
       [SerializeField] private SelectMenuButton SelectmenuButtonPrefab;
    // [SerializeField] private Transform buttonParent; // ボタンを並べる親
    // [SerializeField] private GameObject ScrollObject;

    // シーン名の配列
    private (string sceneName, string displayName)[] sceneNames = {
        ("TitleScene", "タイトル"),
        ("MainScene", "メイン"),
        ("ShopScene", "ショップ"),
        ("LoginOrSinUpScene 1", "ログイン")
    };

    void Awake()
    {
        foreach (var (sceneName, displayName) in sceneNames)
        {
            var menuButton = Instantiate(SelectmenuButtonPrefab, transform);
            menuButton.SetLabel(displayName); // ボタンにシーン名を表示（MenuButton側で実装）
            menuButton.SetOnClick(() => SceneManager.LoadScene(sceneName));
            // menuButton.itemlastCurst(ScrollObject);
        }
    }
    
}
