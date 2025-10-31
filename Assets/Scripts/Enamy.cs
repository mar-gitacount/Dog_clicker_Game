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
    void Start()
    {
        // SpawnBall();
        // enamyAtackGenerator = GetComponent<EnamyAtackGenerator>();

        enamyAtackGenerator.wallet = wallet;
        enamyAtackGenerator.hp = hp;
        enamyAtackGenerator.CreateEnamyBall();
        saveData = new PlayerPrefsSaveData();
        int storyIndex = saveData.LoadStoryProgress();
        // GameObject enemyPrefab = Resources.Load<GameObject>("EnamyDatas/" + storyIndex);
        // GameObject enemyPrefab = Resources.Load<GameObject>("EnamyDatas/1");
        // 保存されたストーリー進行度に基づいてエネミーデータを読み込む
        var enemyPrefab = Resources.LoadAll<EnamyData>("EnamyDatas/"+ storyIndex);
        if (enemyPrefab != null)
        {
            Debug.Log("エネミーデータの読み込み:" + storyIndex);
        }
        else
        {
            Debug.LogWarning("エネミーデータの読み込みに失敗しました:" + storyIndex);
        }
        

    }

    public void Update()
    {
        // Ball が null か、画面外に出た場合
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
