using UnityEngine;

public class Enamy : MonoBehaviour
{
    [SerializeField] private EnamyBall ballPrefab;
    private EnamyBall currentBall;

    public Wallet wallet;
    public HP hp;

    // エネミーの攻撃の種類をループさせて生成する。
    // shopスクリプトを参考にする。
    // 攻撃をリストにして、ループさせる。
    // 攻撃リスト内にエネミーデータがある。
    // EnmyBallインスタンスをEnemyAtackGeneratorに生成させる。
    // EnamyBallには各Enemyの攻撃情報や数値を渡す。
    // EnamyBallはその情報を元に属性を変化させる。
    // HPと所持金の情報を渡す。
    [SerializeField] private EnamyAtackGenerator enamyAtackGenerator;
    void Start()
    {
        // SpawnBall();
        // enamyAtackGenerator = GetComponent<EnamyAtackGenerator>();
        enamyAtackGenerator.wallet = wallet;
        enamyAtackGenerator.hp = hp;
        enamyAtackGenerator.CreateEnamyBall();
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
