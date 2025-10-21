using System.Collections;
using UnityEngine;

public class EnamyBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float HP = 2f;
    private bool isFalling = false;
    private float fallSpeed = 1f;
    // hpクラスを取得。
    // [SerializeField]public HP hp;
    public Wallet wallet;
    public HP hp;

    // ?ゲームオーバー
    // public GameObject gameOverScene;




    public void Initialize()
    {
        // Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), Random.Range(1f, 3f), 0f);


        // transform.position += randomOffset;


        transform.position = new Vector3(Random.Range(-2f, 2f), 1.0f, 0f);
        transform.localScale = Vector3.one * Random.Range(1f, 1.5f);
        if (_rigidbody2D != null)
        {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.gravityScale = 0f;
            _rigidbody2D.freezeRotation = true;

            float jumpPower = 6f;
            _rigidbody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        StartCoroutine(StartFallingAfterDelay(0.5f));
    }

    private IEnumerator StartFallingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isFalling = true;
        if (_rigidbody2D != null)
            _rigidbody2D.velocity = Vector2.zero;
    }

    void Update()
    {
        Debug.Log($"エネミーボールの現在位置: {transform.position}");
        Debug.Log($"エネミーボールの現在HP: {hp.hp}");
        Debug.Log($"エネミーボール参照の所持金: {wallet.money}");
        // hp.hp -= 1;
        // 位置が下になったらマイナス1
        // 籠の位置を確認する。
        // ?籠の位置をもう少しちゃんと取得する。
        if (transform.position.y <= -1.5f)
        {
            // HPを減らす
            hp.hp -= 1f;
            HP -= 1f;
            // wallet.money -= 1;
            Destroy(gameObject);
            // Debug.Log($"エネミーボールのHP: {hp.hp}");
        }
        // HP0になったらゲームオーバー
        GameObject bgObject = GameObject.Find("bg");
        SpriteRenderer bgsr = bgObject.GetComponent<SpriteRenderer>();
        if (HP <= 0f)
        {
            Debug.Log("エネミーボールのHPが0になりました。ゲームオーバーです。");
            // ?ゲームオーバーシーンへ
            // gameOverScene.SetActive(true);

            // bgより上に表示されるようにする。
            Sprite newSprite = Resources.Load<Sprite>("Images/恐ろしい顔の怪物");
            bgsr.sprite = newSprite;
            bgsr.sortingOrder = 1;

        }
        if (isFalling)
        {
            transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        }
        
    }

    void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
