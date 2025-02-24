using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class Sheep : MonoBehaviour
{
    // 初期の犬の画像が格納されている。
    [SerializeField]private SpriteRenderer sheepRenderder;
    [SerializeField]private Sprite cutSheepSprite;
    // ?犬の画像配列 テストコード　ここに複数の犬を配置する。
    [SerializeField] private Sprite[] Dogssprites;
    [SerializeField]private Wool woolPrefab;
    // ?最初の犬の画像テスト
    private Sprite defaultSprite;

    // 移動速度=ランダム
    private float moveSpeed;

    // 犬の初期データ
    public DogData dogData;
    // 毛の量
    private int woolCnt;

    // 初期化処理
    private void Initialize()
    {
        // スプライトレンダー内の画像がセットされている。
        sheepRenderder.sprite = defaultSprite;
        // transform.position = new Vector3(5,0,0);
        transform.position = new Vector3(5,Random.Range(0.0f,4.0f),0); //初期位置をセット
        moveSpeed = -Random.Range(1.0f,2.0f);

        // ?色のデータ
        sheepRenderder.color = dogData.color;
        // ?毛のデータ
        woolCnt = dogData.woolCnt;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ?ランダムに抽出するテストコード
        // if(Dogssprites.Length > 0)
        // {
        //     sheepRenderder.sprite = Dogssprites[Random.Range(0,Dogssprites.Length)];
        // }
        defaultSprite = sheepRenderder.sprite;
        Initialize();
    }
    private void Shaving()
    {
        //? 刈り取る毛がない。
        if(woolCnt <= 0)return;
        // ?　3-40パーセントの毛を刈り取る.
        var shavingWool = (int)(dogData.woolCnt * Random.Range(0.3f,0.4f));
        //?今犬に残ってる毛よりおおい毛は取れないので上限。
        if(woolCnt < shavingWool) shavingWool = woolCnt;
        // ?今回刈り取る分を毛から減らす。
        woolCnt -= shavingWool;
        // !以下の処理に入らない
        if(woolCnt <= 0)
        {
            // ?毛を刈り取ったあとの画像に差し替える。
            sheepRenderder.sprite = cutSheepSprite;
            Debug.Log("毛がなくなった。");
            sheepRenderder.color = Color.white;
        }


        // ここも、犬のカット後の処理に変更する。
        // sheepRenderder.sprite = cutSheepSprite;
        var wool = Instantiate(woolPrefab,transform.position,transform.rotation); 
    }

    // Update is called once per frame
    void Update()

    {
        transform.position += new Vector3(moveSpeed,0) * Time.deltaTime;
        if(transform.position.x < -5)
        {
            Initialize();
        }
        

    
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButton(0)== false) return;
        Shaving();      
    }
}
