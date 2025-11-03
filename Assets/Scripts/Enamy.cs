using UnityEngine;

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
    // HPと所持金の情報を渡す。
    [SerializeField] private EnamyAtackGenerator enamyAtackGenerator;
    public TextEditor[] EnamyBallKindArray;

    private ISaveData saveData;
    public EnamyData enemydata;
    // 最初のプレイヤーの所持金
    public int startingMoney = 0;
    public HP enamyHp;

    public int enamyIndex = 0;
    
    public int storyindex = 1;
    void Start()
    {
        Debug.Log("エネミースタート処理開始");
        // SpawnBall();
        // enamyAtackGenerator = GetComponent<EnamyAtackGenerator>();
        startingMoney = (int)wallet.money;
        enamyAtackGenerator.wallet = wallet;
        enamyAtackGenerator.hp = hp;
        enamyAtackGenerator.CreateEnamyBall();
        saveData = new PlayerPrefsSaveData();

        // !あとでけす
        // enamyIndex = 1;
        saveData.SaveStoryProgress(1);
        

        int storyIndex = saveData.LoadStoryProgress();
        // GameObject enemyPrefab = Resources.Load<GameObject>("EnamyDatas/" + storyIndex);
        // GameObject enemyPrefab = Resources.Load<GameObject>("EnamyDatas/1");
        // 保存されたストーリー進行度に基づいてエネミーデータを読み込む
        var enemyPrefab = Resources.Load<EnamyData>("EnamyDatas/"+ storyIndex);
        if (enemyPrefab != null)
        {
            Debug.Log("エネミーデータの読み込み:" + storyIndex);
            // Debug.Log("敵のHP" + enemyPrefab[0].hp);
            enemydata = enemyPrefab;
            // 既存のenamyHPオブジェクトに固有のデータを設定
            enamyHp.hp = enemydata.hp;
        }
        else
        {
            Debug.LogWarning("エネミーデータの読み込みに失敗しました:" + storyIndex);
        }
        

    }

    public void Update()
    {
        Debug.Log("マネー:" + wallet.money);
        Debug.Log("敵のHpupdate" + enamyHp.hp);
        Debug.Log("エネミーインデックス:" + enamyIndex);
        // storyData配列に基づいて、次のエネミーデータを読み込む。
        var storyData = Resources.Load<StoryData>("StoryDatas/" + storyindex);
       
        // エネミーのHPが0以下になったら、次のエネミーデータを読み込む。
        if(enamyHp.hp <= 0)
        {
            enamyIndex += 1;
            Debug.Log("ストーリーの大きさ:" + storyData.enamys.Length);
            // ストーリー内のエネミーデータがもうなければ、処理を終了する。
            if (storyData.enamys.Length <= enamyIndex)
            {
                // 敵を倒したときの処理、ストーリーシーンへ移動や他の敵の生成など
                Debug.Log("ストーリー内のエネミーデータがもうありません。");
                // ストーリーシーンへ移動する。
                UnityEngine.SceneManagement.SceneManager.LoadScene("StoryScene");
                return;
            }
            // ストーリー内にあるエネミーデータインデックスを参照する。
            // 新しく敵オブジェクトを作成する。
            Debug.Log("ストーリーデータ内の次のエネミーデータインデックス:" + enamyIndex);
            int enamydata = storyData.enamys[enamyIndex];
            // var enemyPrefab = Resources.Load<EnamyData>("EnamyDatas/"+ enamydata);
            
            var enemyPrefab = Resources.Load<EnamyData>("EnamyDatas/" + enamydata);
            enamyHp.hp = enemyPrefab.hp;
            Debug.Log("ストーリーデータ内の次のエネミーデータインデックス:" + enamydata);
            
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
}
