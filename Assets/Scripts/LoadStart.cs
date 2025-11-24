using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadStart : MonoBehaviour
{
    // [SerializeField] private LoadButton loadButtonPrefab;
    // Start is called before the first frame update
    [SerializeField] private Text Text;
    [SerializeField] private Button button;
    [SerializeField] private SaveLoadManager saveLoadManager;
    // ループしてボタンを生成する。
    public void SetLabel(string label)
    {
        Text.text = label;
    }
    void Awake()
    {

        button.onClick.AddListener(OnButtonClicked);
        // button.onClick.AddListener(() => SceneManager.LoadScene("TitleScene"));
    }
    private void OnButtonClicked()
    {
        Debug.Log(Text.text + "のLoadStartがクリックされました。");
        // ロードする番号をセーブマネジメントにする。

        // タイトルへ移動
        SceneManager.LoadScene("TitleScene");
        // ロード処理をここに追加する。
    }
    void Start()
    {
        // forを回してロードボタンを生成する。
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
