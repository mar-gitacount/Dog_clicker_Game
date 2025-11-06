using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelItemButton : MonoBehaviour
{
    private string itemName = "TitleScene";
    // [SerrializeField] private string Scene;
    [SerializeField] private Button button;
    [SerializeField] private Text itemText;
    [SerializeField] private Image itemImage;
    SaveLoadManager saveLoadManager;
    // [SerializeField] private SaveLoadManager saveLoadManager;

    void Awake()
    {
        // ボタンのクリックイベントでTitleSceneへ遷移
        button.onClick.AddListener(() => SceneManager.LoadScene(itemName));
    }
    public void SetLabel(string label)
    {
        itemText.text = label;
    }
    public void SetOnClick(Action action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => action());
        // button.onClick.AddListener(() => ChangeScene());
        // セーブ処理を追加する。

    }
    public void SetSaveClickChange(string sceneName)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => ChangeScene(sceneName));
    }
    public void ChangeScene(string sceneName)
    {
        // ここでセーブ処理を呼び出す
        // セーブ処理
        Debug.Log("セーブ処理を実行します。");

        // saveLoadManager.SaveToCloud();
        saveLoadManager.saveToLocal();

        // 必要ならローカルセーブも
        // saveLoadManager.SaveToLocal();

        // シーン遷移
        SceneManager.LoadScene(sceneName);
    }
    
}
