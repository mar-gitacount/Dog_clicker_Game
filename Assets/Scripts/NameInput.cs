using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NameInput : MonoBehaviour
{
    [SerializeField] private InputField nameInputField;
    [SerializeField] private Button button;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"現在の名前入力フィールドの値: {nameInputField.text}");
        button.onClick.AddListener(OnButtonClicked);

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log($"現在の名前入力フィールドの値: {nameInputField.text}");
    }
    private void OnButtonClicked()
    {
        string playerName = nameInputField.text;
        Debug.Log($"名前入力フィールドの値がボタン押下で取得されました: {playerName}");
        // ここで名前を保存する処理を追加できます。
    }
}
