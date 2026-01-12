using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnamyAtackGenerator : MonoBehaviour
{
    // ?以下はデフォルトでは、testballPrefabを入れている。都度変更する。処理、Enamyごとに変える。
    [SerializeField] private EnamyBall enamyBallPrefab;
    [SerializeField] private EnamyBallSpawner enamyBallSpawner;
    [SerializeField] private GameObject gameOverScene;
    // Start is called before the first frame update
    private float spawnInterval = 2f;  // 生成間隔

    private float timer = 0f;

    public Wallet wallet;
    public HP hp;
    public string[] EnamyAtackKinds;

    // 敵の攻撃データの引数を受け取る
    public void CreateEnamyBall()
    {
        
        int index = Random.Range(0, EnamyAtackKinds.Length);
        string randomAttack = EnamyAtackKinds[index];
        Debug.Log("エネミーアタックジェネレーターで生成する攻撃種類:" + randomAttack);

        enamyBallPrefab.EnamyAtackKinds = EnamyAtackKinds;
        // enamyBallPrefab.Initialize();
        // EnemyBallに画像などのデータなどを渡す。
        // Enamyball秒数を参照して、Spawnを出す。
        var spawner = Instantiate(enamyBallSpawner, transform.position, Quaternion.identity);
        // Instantiate(enamyBallPrefab, transform.position, Quaternion.identity);
        enamyBallPrefab.wallet = wallet;
        enamyBallPrefab.hp = hp;
        // 攻撃の種類
        enamyBallPrefab.EnamyAtackKind = randomAttack;
        // ?エネミーボールにゲームオーバーシーンを渡す。
        enamyBallPrefab.gameOverScene = gameOverScene;
        enamyBallSpawner.ballPrefab = enamyBallPrefab;
        enamyBallSpawner.SpawnBall();
       
        var sprite = spawner.GetComponent<SpriteRenderer>();
        if (sprite != null) sprite.enabled = true;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("エネミーアタックジェネレーターが動いている。");
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            CreateEnamyBall();
            timer = 0f;
        }

    }
     void OnMouseDown()
    {

        // Initialize();
        Debug.Log("オブジェクトがクリックされた。");
        CreateEnamyBall();

        Destroy(gameObject); // クリックされたら削除
    }
    
}
