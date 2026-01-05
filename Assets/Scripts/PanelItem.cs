using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelItem : MonoBehaviour
{
    [SerializeField] private PanelItemButton panelItemButtonPrefab;
    [SerializeField] private SaveLoadManager saveLoadManager;

    // Start is called before the first frame update
    private (string sceneName, string displayName)[] sceneNames = {
        ("TitleScene", "タイトル"),
        // ("MainScene", "メイン"),
        // ("ShopScene", "ショップ"),
        ("LoginOrSinUpScene 1", "ログイン")
    };
    void Awake()
    {
        Debug.Log("PanelItemのAwakeが呼ばれました。");
        foreach (var (sceneName, displayName) in sceneNames)
        {
            var panelItemButton = Instantiate(panelItemButtonPrefab, transform);
            panelItemButton.SetLabel(displayName); // ボタンにシーン名を表示

            panelItemButton.SetOnClick(() => {
                if (saveLoadManager != null)
                {
                    // saveLoadManager.SaveToCloud(); // セーブ処理を追加
                }
                else
                {
                    Debug.LogWarning("SaveLoadManagerが見つかりません。セーブできませんでした。");
                }
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            });
        }
    }
}
