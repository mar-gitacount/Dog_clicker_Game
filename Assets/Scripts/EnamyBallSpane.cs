using UnityEngine;

public class EnamyBallSpawner : MonoBehaviour
{
    public EnamyBall ballPrefab;  // プレハブ
    [SerializeField] private float spawnInterval = 2f;  // 生成間隔

    private float timer = 0f;

    void Update()
    {
        // 一定時間ごとにボールを生成
        Debug.Log("エネミーボールスポーンが動いている。");
        // Debug.Log($"エネミーボールの現在位置: {transform.position}");
        timer += Time.deltaTime;
        // 位置を追跡する
        if (timer >= spawnInterval)
        {
            SpawnBall();
            timer = 0f;
        }
    }

    public void SpawnBall()
    {
        EnamyBall newBall = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        newBall.Initialize();  // 新しいボールだけ初期化
    }
}
