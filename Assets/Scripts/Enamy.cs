using UnityEngine;
using System.Collections;


public class Enamy : MonoBehaviour
{
    [SerializeField] private EnamyBall ballPrefab;
    private EnamyBall currentBall;

    public Wallet wallet;
    public HP hp;

    // エネミーの攻撃の種類をループさせて生成する。
    // shopスクリプトを参考にする。
    // セーブデータから、EnamyDatasを参照する。
    // 攻撃をリストにして、ループさせる。
    // 攻撃リスト内にエネミーデータがある。
    // EnmyBallインスタンスをEnemyAtackGeneratorに生成させる。
    // EnamyBallには各Enemyの攻撃情報や数値を渡す。
    // EnamyBallはその情報を元に属性を変化させる。
    // HPと所持金の情報を渡す.

    // 敵の画像表示用
    [SerializeField]private SpriteRenderer enamySpriteRenderer;
    [SerializeField] private EnamyAtackGenerator enamyAtackGenerator;
    public TextEditor[] EnamyBallKindArray;

    private Sprite defaultSprite;
    private ISaveData saveData;
    public EnamyData enemydata;
    // 最初のプレイヤーの所持金
    public int startingMoney = 0;
    public HP enamyHp;

    public int enamyIndex = 0;
    private StoryData storyData;

    public int storyIndex;
    SaveLoadManager saveLoadManager;
    void Start()
    {
        Debug.Log("エネミースタート処理開始");
        // SpawnBall();

        // enamySpriteRenderer.sprite = defaultSprite;
        // defaultSprite = enamySpriteRenderer.sprite;
        // enamyAtackGenerator = GetComponent<EnamyAtackGenerator>();
        startingMoney = (int)wallet.money;
        enamyAtackGenerator.wallet = wallet;
        enamyAtackGenerator.hp = hp;
    
        
       
       
        saveData = new PlayerPrefsSaveData();


        storyIndex = saveData.LoadStoryProgress();
        var storyData = Resources.Load<StoryData>("StoryDatas/" + storyIndex);

        // ?テストコード
        enemydata = Resources.Load<EnamyData>("EnamyDatas/"+ storyData.enamys[0]);
       
        Initialize();
        return;
        string enamypicturePath = enemydata.picturePath;
        Sprite newSprite  = Resources.Load<Sprite>("EnamySprites/"+ enamypicturePath);
        // newSprite.color = enemydata.color;
        Vector2 worldSize = enamySpriteRenderer.sprite.bounds.size;
        Vector3 scale = enamySpriteRenderer.transform.lossyScale;
        Vector2 actualSize = new Vector2(worldSize.x * scale.x, worldSize.y * scale.y);

        if(newSprite != null)
        {
            enamySpriteRenderer.sprite = newSprite;
            Debug.Log("エネミー画像の読み込み成功:" + enamypicturePath);
        }
        enamySpriteRenderer.color = enemydata.color;
        Debug.Log("エネミーの色:" + enemydata.color);
        enamyHp.hp = enemydata.hp;
        return;

        // enamyIndex = 1;
        // saveData.SaveStoryProgress(1);
        
        // セーブデータをロード
        
        // GameObject enemyPrefab = Resources.Load<GameObject>("EnamyDatas/" + storyIndex);
        // GameObject enemyPrefab = Resources.Load<GameObject>("EnamyDatas/1");
        // 保存されたストーリー進行度に基づいてエネミーデータを読み込む
        // var storyData = Resources.Load<StoryData>("StoryDatas/" + storyIndex);
        var enamyIndex = storyData.enamys[0];
        var enemyPrefab = Resources.Load<EnamyData>("EnamyDatas/"+ enamyIndex);

        if (enemyPrefab != null)
        {
            Debug.Log("エネミーデータの読み込み:" + enamyIndex);
            // enamySpriteRenderer.color = enemyPrefab.color;
            // Debug.Log("エネミーデータの色:" + enemyPrefab.color);
            enemydata = enemyPrefab;
            // 既存のenamyHPオブジェクトに固有のデータを設定
            enamyHp.hp = enemydata.hp;
        }
        else
        {
            Debug.LogWarning("エネミーデータの読み込みに失敗しました:" + storyIndex);
            StartCoroutine(LoadNextScene());
            //読みこむ失敗したらストーリー画面へ遷移させる。
       
            storyIndex = 10000;
            enemyPrefab = Resources.Load<EnamyData>("EnamyDatas/" + storyIndex);
            enemydata = enemyPrefab;
            enamyHp.hp = enemydata.hp;
        }
        

    }
    private void Initialize()
    {
        Debug.Log("エネミー初期化処理開始");
        // enamySpriteRenderer.sprite = defaultSprite;
        var storyData = Resources.Load<StoryData>("StoryDatas/" + storyIndex);
        Debug.Log("ストーリーデータの読み込み:" + enamyIndex);
       
        // int enamydata = storyData.enamys[enamyIndex];
        enemydata = Resources.Load<EnamyData>("EnamyDatas/"+ storyData.enamys[enamyIndex]);
        Debug.Log("エネミーアタックジェネレーターに攻撃種類を代入する。" + enemydata.enamyAtackKinds.Length);
        // EnamyAtackGeneratorに攻撃種類を代入する。
        enamyAtackGenerator.EnamyAtackKinds = enemydata.enamyAtackKinds;

        enamyAtackGenerator.CreateEnamyBall();

        string enamyPicturePath = enemydata.picturePath;
        // 画像を変更する。
        Sprite newSprite  = Resources.Load<Sprite>("EnamySprites/" + enamyPicturePath);
        // enemydata.enamyRenderder = newSprite.GetComponent<SpriteRenderer>();
        Vector2 worldSize = enamySpriteRenderer.sprite.bounds.size;
        Vector3 scale = enamySpriteRenderer.transform.lossyScale;
        Vector2 actualSize = new Vector2(worldSize.x * scale.x, worldSize.y * scale.y);
        if(newSprite != null)
        {
            enamySpriteRenderer.sprite = newSprite;
            Debug.Log("エネミー画像の読み込み成功:" + enamyPicturePath);
        }
        
        enamySpriteRenderer.color = enemydata.color;
        enamyHp.hp = enemydata.hp;

    }

    public void Update()
    {
        
        Debug.Log("エネミーHP(エネミーオブジェクト)" + enamyHp.hp);
        Debug.Log("エネミーインデックス:" + enamyIndex);
        // storyData配列に基づいて、次のエネミーデータを読み込む。
        var storyData = Resources.Load<StoryData>("StoryDatas/" + storyIndex);
        Debug.Log("ストーリーデータの読み込み:" + storyIndex);
        // エネミーのHPが0以下になったら、次のエネミーデータを読み込む。
        if(enamyHp.hp <= 0)
        {
            Debug.Log("エネミーのHPが0以下になりました。次のエネミーデータを読み込みます。");
            enamyIndex += 1;

            if(storyData.enamys.Length <= enamyIndex)
            {
                Debug.Log("ストーリー内のエネミーデータがもうありません。");
                // saveData.SaveMoney(wallet.money);
                Debug.Log("ストーリー進行度をセーブします。" + storyIndex + "右に1足す" + (storyIndex + 1));
                int currentIndex = storyIndex + 1;
                saveData.SaveStoryProgress(currentIndex); // ストーリー進行度を更新
                Debug.Log("マネー:" + wallet.money);
                // saveLoadManager.saveToLocal(); 
                // ストーリーシーンへ移動する。
                StartCoroutine(LoadNextScene());
                // UnityEngine.SceneManagement.SceneManager.LoadScene("StoryScene");
                return;
            }
            Initialize();
            return;
        }
        
       
        if (currentBall == null || IsBallOutOfScreen(currentBall))
        {
            
            if (currentBall != null)
            {
                // Destroy(currentBall.gameObject); // まだ存在する場合は削除
            }
            // SpawnBall(); // 新しい Ball を生成
        }
    }

    // 画面外判定
    bool IsBallOutOfScreen(EnamyBall ball)
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(ball.transform.position);
        return viewportPos.y < 0; // 下に出たら true
    }

    void SpawnBall()
    {
        currentBall = Instantiate(ballPrefab, transform.position, transform.rotation);
    }
    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(3f); // 少し待ってログ確認
        UnityEngine.SceneManagement.SceneManager.LoadScene("StoryScene");
    }
}
