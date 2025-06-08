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

    void Awake()
    {
        // ボタンのクリックイベントでTitleSceneへ遷移
        // button.onClick.AddListener(() => SceneManager.LoadScene(itemName));
    }
    public void SetOnClick(Action action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => action());
    }
}
