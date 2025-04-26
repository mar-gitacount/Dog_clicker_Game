using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// using System.Diagnostics;

public class Wool : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidbody2D;
   
    [SerializeField]private Coin coinPrefab;

    [SerializeField]private SpriteRenderer woolSpriteRenderer;
    // 羊の色
    public Color woolColor;

     public int price = 100;
    // 売却処理
    public void sell(Wallet wallet)
    {
        var coin = Instantiate(coinPrefab,transform.position,transform.rotation);
        coin.value = price;
        coin.wallet = wallet;
        Destroy(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody2D.AddForce(Quaternion.Euler(0, 0, Random.Range(-15.0f, 15.0f)) * Vector2.up * 4, ForceMode2D.Impulse);
        transform.localScale = Vector3.one * Random.Range(0.4f, 1.5f);
        // 色を変える
        woolColor.a = 0.9f;
        woolSpriteRenderer.color = woolColor; 

    }

    // Update is called once per frame
    void Update()
    {
        // 現在の座標をデバッグログに表示
         Debug.Log($"Woolの現在位置: {transform.position}");

        // y座標が-5未満の場合、オブジェクトを削除
        if (transform.position.y < -5)
        {
            Debug.Log($"Woolが画面外に出ました。削除します: {transform.position}");
            // !画面外に落ちた場合削除する。アイテムの都合上一旦コメントアウト
            // Destroy(gameObject);
        }
    }
}
