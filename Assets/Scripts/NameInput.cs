using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NameInput : MonoBehaviour
{
    [SerializeField] private InputField nameInputField;
    [SerializeField] private Button button;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button CancelButton;
    [SerializeField] private Text CharacterNamedisplayText;
    private ISaveData saveData;

    // [SerializeField] private FilterCanvas filterPrefab;
    public FilterCanvas filter;
    // Start is called before the first frame update
    void Start()
    {
        saveData = new PlayerPrefsSaveData();
        Debug.Log($"現在の名前入力フィールドの値: {nameInputField.text}");
        button.onClick.AddListener(OnButtonClicked);
        yesButton.onClick.AddListener(NameInputValue);
        CancelButton.onClick.AddListener(CancelButtonClicked);
        nameInputField.text = saveData.LoadMainCharacterName(saveData.LoadNow());

    }

    // 名前を入力したときの処理、登録する。
    private void NameInputValue()
    {
        saveData = new PlayerPrefsSaveData();
        string playerName = nameInputField.text;
        Debug.Log($"なまえが入力されました: {playerName}");
        string current = saveData.LoadNow().ToString();
        saveData.SaveMainCharacterName(playerName,saveData.LoadNow());
        Debug.Log($"名前入力フィールドの値がボタン押下で取得されました: {playerName}"+"\n"+"現在のセーブ番号:"+current);
        filter.gameObject.SetActive(false);
    }

    private void CancelButtonClicked()
    {
        // キャンセルボタンがクリックされたときの処理
        filter.gameObject.SetActive(false);
        Debug.Log("キャンセルボタンがクリックされました。");
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log($"現在の名前入力フィールドの値: {nameInputField.text}");
    }
    private void OnButtonClicked()
    {
        saveData = new PlayerPrefsSaveData();
        // var filter = Instantiate(filterPrefab, Vector3.zero, Quaternion.identity);
        // filter.SetActive(true);
        filter.gameObject.SetActive(true);

        string playerName = nameInputField.text;
        CharacterNamedisplayText.text = "なまえ:" + playerName+"でいいですか？";

        
        // ここで名前を保存する処理を追加できます。
    }
}
