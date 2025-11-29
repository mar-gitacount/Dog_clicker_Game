using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Services.CloudSave;
using UnityEngine;
using UnityEngine.UI;


public class Story : MonoBehaviour
{
    // Start is called before the first frame update
    // セーブデータから、どのストーリーかを読み込む。
    // テキストプレハブ及び、画像プレハブを数字ごとに用意する。
    // セーブデータの数字から引用する。
    // 背景はヒエラルキー内のbgスプライトを書き換える。


    // ストーリーテキストデータ
    [SerializeField] private TextData[] storyTextDatas;
    [SerializeField] private Text storyTextData;
    private ISaveData saveData;
    private int currentTextIndex = 0;
    // private Text TextData[] testStoryTexts;
    private GameObject bgObject;
    

    
    void Start()
    {
        saveData = new PlayerPrefsSaveData();

        if (saveData != null)
        {
            int storyIndex = saveData.LoadStoryProgress();
            Debug.Log("ストーリースクリプト内のロードしたストーリーインデックス: " + storyIndex);
            // !storyIndexに基づいて、storyTextDatasや背景画像を変更する処理を追加する。
            // 例えば、storyIndexが1ならstoryTextDatasを別の配列に変更するなど。
        }
        else
        {
            Debug.LogWarning("SaveLoadManagerが見つかりません。セーブデータを読み込めませんでした。");
        }

        Debug.Log("ストーリースクリプトのStartが呼ばれました。");
        storyTextData.text = storyTextDatas[0].text;
        
        // クリックされたら、次のテキストに変更する処理を入れる。
    }

    // Update is called once per frame
    void Update()
    {
        // クリックしたら、次のテキストに変更する処理を入れる。
        GameObject bgObject = GameObject.Find("bg");
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("右クリック押した！");
            // !とりあえず背景を変えるテスト
            SpriteRenderer bgRenderer = bgObject.GetComponent<SpriteRenderer>();
            Sprite newStorybg = Resources.Load<Sprite>("Images/bgtest");
            bgRenderer.sprite = newStorybg;
            if (currentTextIndex >= storyTextDatas.Length)
            {
                Debug.Log("ストーリーが終了しました。");
                int storyIndex = saveData.LoadStoryProgress();
                // int currentIndex = storyIndex + 1;
                // ストーリーが終了したので、セーブデータを更新する。
                // saveData.SaveStoryProgress(currentIndex); // デフォルトでストーリー1を保存
                // ゲームメインシーンへ移動する処理を入れる。
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
                return;
            }
            storyTextData.text = storyTextDatas[currentTextIndex].text;
            currentTextIndex++;
        }
        
    }
}
