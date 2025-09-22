using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameMain : MonoBehaviour
{
    [SerializeField]
    private Button sellButton;

    [SerializeField]
    private Wallet wallet;

    // ホラープレハブ
    [SerializeField] private Face facePrefab;

    private SpriteRenderer bgRenderer;

    private float timer = 0f;
    public GameObject gameOverScene;
    // タイトルシーンへ行くためのオブジェクト
    public GameObject toTitleScene;

    [SerializeField] private Text TextObject;

    // テキストデータの配列
    public TextData[] textDataArray;

    // バッドテキスト
    public TextData[] badTextDataArray;

    // 一時停止ボタン
    public Button pauseButton;


    // 売却ボタンを押下した時に呼ばれる関数,表示されている毛を全て取得し、所持金に追加する。
    private void SellAllWool()
    {
        //画面上の全てのWoolスクリプトが付いたオブジェクトを検索してWool配列woolsに格納
        var wools = FindObjectsByType<Wool>(FindObjectsSortMode.None);
        foreach (var wool in wools)
        {
            wool.sell(wallet);
        }
        SoundManeger.Instance.Play("コイン");

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // var face = Instantiate(facePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        sellButton.onClick.AddListener(SellAllWool);
        ShowRandomText();

        pauseButton.onClick.AddListener(TogglePause);
    }

    // Update is called once per frame
    void Update()
    {
        // ポージングボタンが押下されているとき
        if (isPaused) return;
        
        // 異変起きていて、かつプレイヤーが異変に気付いてスペースをした場合、次の画面へ
        if (Input.GetKeyDown(KeyCode.Space) && timer > 0f)
        {
            // TextObject.text = "こんにちは、おいそぎですか？";
            // int randomIndex = Random.Range(0, textDataArray.Length);
            // TextObject.text = textDataArray[randomIndex].text;
            ShowRandomText();
            Debug.Log($"選ばれたセリフ: {TextObject.text}");
            // フェイスオブジェクトを消す。
            Debug.Log("画面切り替え");
            // シーン切り替え処理をここに追加
            GameObject bgObject = GameObject.Find("bg");
            SpriteRenderer bgsr = bgObject.GetComponent<SpriteRenderer>();
            // 背景を変えて次の画像へ
            // Sprite newSprite = Resources.Load<Sprite>("Images/恐ろしい顔の怪物");
            // bgsr.sprite = newSprite;
            // タイマーを0に戻す。
            var faces = FindObjectsOfType<Face>();
            foreach (var face in faces)
            {
                Destroy(face.gameObject);
            }

            timer = 0f;
            // UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
        }
        else if (Input.GetKeyDown(KeyCode.Space) && timer == 0f)
        {
            Debug.Log("ゲームオーバー");
            GameObject bgObject = GameObject.Find("bg");
            SpriteRenderer bgsr = bgObject.GetComponent<SpriteRenderer>();
            // 背景を変えて次の画像へ
            Sprite newSprite = Resources.Load<Sprite>("Images/恐ろしい顔の怪物");
            bgsr.sprite = newSprite;

            // bgより上に表示されるようにする。
            bgsr.sortingOrder = 1;
        }

        if (timer >= 5f)
        {
            Debug.Log("ゲームオーバーです。怖いシーンに切り替えます。");
            // ゲームオーバー
            gameOverScene.SetActive(true);
            // タイトル行き画面を表示する。
            toTitleScene.SetActive(true);
            // bgの上に怖い顔画像を重ねる
            GameObject bgObject = GameObject.Find("bg");
            if (bgObject != null)
            {
                SpriteRenderer bgsr = bgObject.GetComponent<SpriteRenderer>();

                // 怖い顔画像を新規オブジェクトとして生成し、bgの子にする
                GameObject scaryFaceObj = new GameObject("ScaryFace");
                scaryFaceObj.transform.parent = bgObject.transform;
                scaryFaceObj.transform.localPosition = Vector3.zero; // bgの中心に配置

                SpriteRenderer sr = scaryFaceObj.AddComponent<SpriteRenderer>();
                sr.sprite = Resources.Load<Sprite>("Images/恐ろしい顔の怪物"); // 画像パスは適宜変更
                sr.color = Color.white;
                sr.sortingOrder = bgsr.sortingOrder + 1; // 背景より上に表示

                // 必要ならサイズ調整
                scaryFaceObj.transform.localScale = Vector3.one;
            }

            // シーン切り替え処理（必要なら）
            // UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
        }

        // 怖い画像フラグ判定。
        if (Random.Range(0, 200) == 0 && timer == 0f)
        {
            // 以下をランダムで実行する。
            // 画像パスをランダムに指定してfaceに渡す。
            // ?消す
            // var face = Instantiate(facePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            // 文字に変える
            // テキストプレハブを作り、それを複数つくって、配列にそのプレハブ群を入れる。
            // TextObject.text += "\n"+ShowRandomBadText();
            ShowRandomBadText();
            Debug.Log("怖い画像が出る");
            timer += Time.deltaTime;
        }
        else if (timer > 0f)
        {
            timer += Time.deltaTime;
        }

        // Update内で、1/3の確率を行い、画像に異変を加え、異変変数をtrueにする。5秒たったらゲームオーバーで怖いシーンに切り替える。
        // GameObject bgObject = GameObject.Find("bg");
        // SpriteRenderer bgsr = bgObject.GetComponent<SpriteRenderer>();

        // こわい画像追加オブジェクトを作る。
        // GameObject scaryFaceObj = new GameObject("ScaryFace");
        // scaryFaceObj.transform.parent= bgObject.transform;
        // scaryFaceObj.transform.localPosition = new Vector3(0.5f, 0.5f, 0); // 親の中心に配置


        // // 親要素背景に子をセット
        // SpriteRenderer sr = scaryFaceObj.AddComponent<SpriteRenderer>();
        // sr.color = Color.white;
        // // 以下はいろんな画像に変える。
        // // Sprite newSprite = Resources.Load<Sprite>("Images/恐ろしい顔の怪物");


        // if (bgObject != null)
        // {
        //     bgRenderer = bgObject.GetComponent<SpriteRenderer>();
        //     if (bgRenderer == null)
        //     {
        //         Debug.LogWarning("bgにSpriteRendererが付いていません！");
        //     }
        //     else
        //     {
        //         Debug.Log("BGがセットされています。");
        //     }
        //     if (Input.GetKeyDown(KeyCode.Space) && bgRenderer != null)
        //     {
        // bgRenderer.color = Color.red;
        // sr.sprite = Resources.Load<Sprite>("Images/恐ろしい顔の怪物");
        // sr.sortingOrder = bgsr.sortingOrder + 1;// 背景の上に表示されるようにする。
        // bgObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f);

        //     }
        // SpriteRenderer sr = GetComponent<SpriteRenderer>();
        // Color bgColor = sr.color;
        // Sprite bgSprite = sr.sprite;
        // Debug.Log("背景色: " + bgColor);
        // Debug.Log("背景スプライト: " + bgSprite.name);
        // }
        // else
        // {
        //     Debug.LogWarning("bgがセットされていません！");
        // }
    }

    /// <summary>
    /// textDataArrayからランダムにテキストを選び、TextObjectに表示する
    /// </summary>
    private void ShowRandomText()
    {
        int randomIndex = Random.Range(0, textDataArray.Length);
        TextObject.text = textDataArray[randomIndex].text;
    }
    public void ShowRandomBadText()
    {
        int randomIndex = Random.Range(0, badTextDataArray.Length);
        TextObject.text += "\n"+badTextDataArray[randomIndex].text;
    }

    private bool isPaused = false;

    private void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1f; // ゲーム再開
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0f; // ゲーム一時停止
            isPaused = true;
        }
    }
}
