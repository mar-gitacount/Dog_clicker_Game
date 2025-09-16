using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    private SpriteRenderer sr;
    private float timer = 0f;

    [SerializeField] private Sprite[] faceSprites;

    void Start()
    {
        Debug.Log("顔テストスタート");
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = true;         // 画像を表示
        SetRandomPosition();
        SetRandomSprite();         // ランダム画像
    }

    // ランダムな位置に移動するメソッド
    void SetRandomPosition()
    {
        GameObject bgObject = GameObject.Find("bg");
        if (bgObject != null)
        {
            SpriteRenderer bgRenderer = bgObject.GetComponent<SpriteRenderer>();
            if (bgRenderer != null && bgRenderer.sprite != null)
            {
                // 背景のサイズ（ワールド座標での幅・高さ）を取得
                Vector2 bgSize = bgRenderer.sprite.bounds.size;
                Vector3 bgPos = bgObject.transform.position;

                // 背景の中心を基準に、画像が背景内に収まるようにランダム座標を計算
                float x = Random.Range(bgPos.x - bgSize.x / 2f, bgPos.x + bgSize.x / 2f);
                float y = Random.Range(bgPos.y - bgSize.y / 2f, bgPos.y + bgSize.y / 2f);

                transform.position = new Vector3(x, y, transform.position.z);
            }
            else
            {
                // 背景が見つからない場合はデフォルト範囲
                float x = Random.Range(-5f, 5f);
                float y = Random.Range(-3f, 3f);
                transform.position = new Vector3(x, y, transform.position.z);
            }
        }
        else
        {
            // 背景が見つからない場合はデフォルト範囲
            float x = Random.Range(-5f, 5f);
            float y = Random.Range(-3f, 3f);
            transform.position = new Vector3(x, y, transform.position.z);
        }
    }

    // ランダムな画像（スプライト）に変更するメソッド
    void SetRandomSprite()
    {
        if (faceSprites != null && faceSprites.Length > 0)
        {
            int index = Random.Range(0, faceSprites.Length);
            sr.sprite = faceSprites[index];
        }
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // スペースキーで即座に削除
            Destroy(gameObject);
            
        }
        if (timer >= 50f)
        {
            Destroy(gameObject);
            Debug.Log("ゲームオーバーです。怖いシーンに切り替えます。");
            GameObject bgObject = GameObject.Find("bg");
            SpriteRenderer bgsr = bgObject.GetComponent<SpriteRenderer>();
            // 背景を変えて次の画像へ
            Sprite newSprite = Resources.Load<Sprite>("Images/恐ろしい顔の怪物");
            bgsr.sprite = newSprite;
            // bgより上に表示されるようにする。
            bgsr.sortingOrder = 1;
        }
        // 一定時間後にオブジェクトを削除
        // Destroy(gameObject); // 5秒後に削除
    }
}
