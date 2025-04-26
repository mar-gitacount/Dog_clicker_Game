using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    // 一時停止もしくは再生テキスト
    [SerializeField] private Text pauseText;

    [SerializeField] private Button button;
    // 停止中のフィルター
    public GameObject PauseFilter;
    private bool isPause = false;

    [SerializeField] private string[] comments = {
        "虚しくなりませんか？",
        "常識を守るのは所詮損得勘定からですよ",
        "小さいルールならみんな破ってます。",
        "全て無です。",
        "それでも頑張ることに意味があるのかも",
        "意味は自分で作るものでしょうねきっと",
        "公告に惑わされず自分の道をいきましょう",
        "アイテムの金額に意味はないです。気にせず"
    }; // コメントを格納する配列

    [SerializeField] private Text commentText; // コメントを表示するTextコンポーネント

    void Start()
    {
        button.onClick.AddListener(PuaseOrPlay);
    }

    public void PuaseOrPlay()
    {
        isPause = !isPause;

        // ゲーム開始もしくは停止
        if (isPause)
        {
            pauseText.text = "再生";
            Time.timeScale = 0;
            PauseFilter.SetActive(isPause);

            // ランダムでセリフを選択して表示
            if (comments.Length > 0)
            {
                int randomIndex = Random.Range(0, comments.Length);
                commentText.text = comments[randomIndex];
                Debug.Log($"選ばれたセリフ: {commentText.text}");
            }
            else
            {
                Debug.LogWarning("コメント配列が空です！");
            }
        }
        else
        {
            pauseText.text = "停止";
            Time.timeScale = 1;
            PauseFilter.SetActive(isPause);

            // 再生時にコメントをクリア（必要に応じて）
            commentText.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 必要に応じて追加の処理を記述
    }
}
