using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.UI;
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

    //? 一時停止フラグ
    private bool isPaused = false;
    // ? 
    public GameObject pauseMenu;

    private void OnEnable()
    {
        Debug.Log("犬データがアクティブです");
        SheepManager.Instance?.RegisterSheep(this);        
    }
    private void OnDisable()
    {
        SheepManager.Instance?.UnregisterSheep(this);
    }
    // 初期化処理
    private void Initialize()
    {
        // !インスタンスのこの初期化が動き続けている。
        // デフォルトでは、スプライトレンダー内の画像がセットされている。
        sheepRenderder.sprite = defaultSprite;
        // !以下を画像に変える。
        Sprite newSprite = Resources.Load<Sprite>("Images/"+dogData.picturePath);

        Vector2 worldSize = sheepRenderder.sprite.bounds.size;
        Vector3 scale = sheepRenderder.transform.lossyScale;
        Vector2 actualSize = new Vector2(worldSize.x * scale.x, worldSize.y * scale.y);



        // 画像が存在する場合、セットする。
        if(newSprite != null)
        {
            sheepRenderder.sprite = newSprite;
        }
        // transform.position = new Vector3(5,0,0);
        //初期位置をセット
        transform.position = new Vector3(5,Random.Range(0.0f,4.0f),0); 
        // transform.position = new Vector3(5,-4.0f,0); 
        moveSpeed = -Random.Range(1.0f,2.0f);

        // ?色のデータ
        sheepRenderder.color = dogData.color;
        // ?毛のデータ
        woolCnt = dogData.woolCnt;
        Debug.Log("犬データのイニシャライズ");

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
    public void Shaving()
    {
        // ポージングしているとき
        // Transform child = pauseMenu.Find("PauseMenu");
        GameObject obj = GameObject.Find("PauseMenu"); 
        if(obj != null)
        {
            Debug.Log("ポーズフィルター作動。");
            return;

        }
        // if(pauseMenu.activeSelf)
        // {
        //     Debug.Log("停止システム動作成功");
        //     return;
        // }
        if(isPaused)
        {
            return;
        }
        //? 刈り取る毛がない。
        if(woolCnt <= 0)return;
        // ?　3-40パーセントの毛を刈り取る。
        // ?ここで毛の金額の値段を設定する。
        // !後で変える
        // var shavingWool = (int)(dogData.woolCnt * Random.Range(0.3f,0.4f));
        var shavingWool = woolCnt;
        //?今犬に残ってる毛よりおおい毛は取れないので上限。
        if(woolCnt < shavingWool) shavingWool = woolCnt;
        // ?今回刈り取る分を毛から減らす。
        woolCnt -= shavingWool;
        if(woolCnt <= 0)
        {

            // !毛を刈り取ったあとの画像に差し替える。
            // !cutSheepSpriteにはデフォルトのカット画像が代入してあるので、データのカット画像に差し替える。
            Sprite newCutSprite = Resources.Load<Sprite>("Images/"+dogData.cutPicturePath);
            if (newCutSprite !=  null)
            {
                // 犬データに準拠した画像
                sheepRenderder.sprite = newCutSprite;
            }
            else
            {
                // デフォルトのカット画像
                sheepRenderder.sprite = cutSheepSprite;
            }
            
            // sheepRenderder.color = Color.white;
            // 犬データに準拠した色になる。
            sheepRenderder.color = dogData.color;
            SoundManeger.Instance.Play("ワン");
        }


        // ここも、犬のカット後の処理に変更する。
        // sheepRenderder.sprite = cutSheepSprite;
        var wool = Instantiate(woolPrefab,transform.position,transform.rotation); 
        // Woolオブジェクトに今回刈り取った毛を渡す。
        wool.price = shavingWool;
        // 犬の色データを代入する。
        wool.woolColor = dogData.color;
        SoundManeger.Instance.Play("刈り取り");
    }

    // Update is called once per frame
    void Update()

    {
        transform.position += new Vector3(moveSpeed,0) * Time.deltaTime;
        // !イベント
        // if(Input.GetKeyDown(KeyCode.P))
        // {
        //     TogglePause();
        // }

        if(transform.position.x < -5)
        {

            // 画面の外に行ったら初期化する。
            Initialize();
        }
        

    
    }
    // !ポーズ関数→もしかしたらイベントリスナーに訂正する可能性アリ
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseMenu.SetActive(isPaused);
        EventSystem.current.enabled = !isPaused;
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButton(0)== false) return;
        Shaving();      
    }
}
