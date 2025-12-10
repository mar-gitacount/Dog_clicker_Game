using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NameInput : MonoBehaviour
{
    [SerializeField] private InputField nameInputField;
    [SerializeField] private Button button;
    private ISaveData saveData;
    // Start is called before the first frame update
    void Start()
    {
        saveData = new PlayerPrefsSaveData();
        Debug.Log($"現在の名前入力フィールドの値: {nameInputField.text}");
        button.onClick.AddListener(OnButtonClicked);
        nameInputField.text = saveData.LoadMainCharacterName(saveData.LoadNow());

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log($"現在の名前入力フィールドの値: {nameInputField.text}");
    }
    private void OnButtonClicked()
    {
        saveData = new PlayerPrefsSaveData();
        string playerName = nameInputField.text;
        string current = saveData.LoadNow().ToString();
        saveData.SaveMainCharacterName(playerName,saveData.LoadNow());
        Debug.Log($"名前入力フィールドの値がボタン押下で取得されました: {playerName}"+"\n"+"現在のセーブ番号:"+current);
        // ここで名前を保存する処理を追加できます。
    }
}
