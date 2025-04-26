using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Comment : MonoBehaviour
{
    [SerializeField] private string[] comments = {
        "こんにちは！",
        "ゲームを楽しんでください！",
        "次のレベルに進みましょう！",
        "素晴らしいプレイです！",
        "頑張ってください！"
    }; // コメントを格納する配列

    [SerializeField] private Text commentText; // コメントを表示するTextコンポーネント

    // Start is called before the first frame update
    void Start()
    {
        // 配列からランダムにコメントを選択してTextに代入
        if (comments.Length > 0)
        {
            int randomIndex = Random.Range(0, comments.Length);
            commentText.text = comments[randomIndex]; // Textコンポーネントに代入
            Debug.Log($"選ばれたコメント: {commentText.text}");
        }
        else
        {
            Debug.LogWarning("コメント配列が空です！");
        }
    }
}
