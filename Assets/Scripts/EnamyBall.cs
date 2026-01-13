using System.Collections;
using UnityEngine;
using UnityEngine.UI;
// using System;
using System.Collections.Generic;


public class EnamyBall : MonoBehaviour
{
    [SerializeField] private SpriteRenderer EnamyballRenderer;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float HP = 2f;
    private bool isFalling = false;
    private float fallSpeed = 1f;
    // hpクラスを取得。
    // [SerializeField]public HP hp;
    public Wallet wallet;
    public HP hp;

    // ?ゲームオーバー
    public GameObject gameOverScene;
    // 攻撃の種類
    public string EnamyAtackKind;

    Dictionary<string, System.Action> attackMap;

    string pendingAttack = null;

    public string[] EnamyAtackKinds;

    

    void Awake()
    {
        attackMap = new Dictionary<string, System.Action>
        {
            { "green", green },
            { "red", red },
            { "blue", blue },
        // 他の攻撃種類と対応するメソッドを追加
        };

    }

    // 呼び出し元から攻撃方法をセットする。
    public void seteAtack(string attackKind)
    {
        pendingAttack = attackKind;
       
    }
   



    public void Initialize()
    {
        // Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), Random.Range(1f, 3f), 0f);
        if(EnamyAtackKinds.Length == 0)
        {
            return;
        }

        //! 以下をスタートに移動!!
        int index = Random.Range(0,EnamyAtackKinds.Length);
        string randomAttack = EnamyAtackKinds[index];
        Debug.Log("エネミーボールのインデックス:" + index);
        pendingAttack = randomAttack;
        seteAtack(randomAttack);

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

    private void green()
    {
        Debug.Log("緑色の処理が実行されました。");
        // 緑色の処理をここに追加
        EnamyballRenderer.color = Color.green;
        if (transform.position.y <= -1.5f)
        {
            
            Destroy(gameObject);
            // Debug.Log($"エネミーボールのHP: {hp.hp}");
        }
       
    }
    private void red()
    {
        // 赤色の処理をここに追加
        Debug.Log("赤色の処理が実行されました。");
        if (transform.position.y <= -1.5f)
        {
            // HPを減らす
            hp.hp -= 1f;
            HP -= 1f;
            // wallet.money -= 1;
            Destroy(gameObject);
            // Debug.Log($"エネミーボールのHP: {hp.hp}");
        }
    }
    private void blue()
    {
        Debug.Log("青色の処理が実行されました。");
        EnamyballRenderer.color = Color.blue;
        if (transform.position.y <= -1.5f)
        {
            
            Destroy(gameObject);
            // Debug.Log($"エネミーボールのHP: {hp.hp}");
        }

        // 青色の処理をここに追加
        
    }

    void Update()
    {
        Debug.Log($"エネミーボールの現在位置: {transform.position}");
        Debug.Log($"エネミーボールの現在HP: {hp.hp}");
        Debug.Log($"エネミーボール参照の所持金: {wallet.money}");
        if(pendingAttack != null && attackMap.ContainsKey(pendingAttack))
        {
            attackMap[pendingAttack].Invoke();
            // pendingAttack = null; // 一度実行したらクリア
        }
        // hp.hp -= 1;
        // 位置が下になったらマイナス1
        // 籠の位置を確認する。
        // ?籠の位置をもう少しちゃんと取得する。
        // HP0になったらゲームオーバー
        GameObject bgObject = GameObject.Find("bg");
        SpriteRenderer bgsr = bgObject.GetComponent<SpriteRenderer>();
        if (hp.hp <= 0f)
        {
            Debug.Log("エネミーボールのHPが0になりました。ゲームオーバーです。");
            // ?ゲームオーバーシーンへ一番初めに表示する。
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
            // UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");

            // gameOverScene.SetActive(true);

            // bgより上に表示されるようにする。
            // Sprite newSprite = Resources.Load<Sprite>("Images/恐ろしい顔の怪物");
            // bgsr.sprite = newSprite;
            // bgsr.sortingOrder = 1;

        }
        if (isFalling)
        {
            transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        }
        
    }

    void OnMouseDown()
    {
        switch (pendingAttack)
        {
            case "green":
                wallet.money += 5;
                Destroy(gameObject);
                Debug.Log("緑色のエネミーボールがクリックされました。所持金が5増えます。");
                break;
            case "red":
                // hp.hp -= 1;
             
                Destroy(gameObject);
                Debug.Log("赤色のエネミーボールがクリックされました。HPが1減ります。");
                break;
            case "blue":
                // wallet.money += 10;
                hp.hp -= 1;
                Destroy(gameObject);
                Debug.Log("青色のエネミーボールがクリックされました。所持金が10増えます。");
                break;
            default:
                Debug.Log("未知のエネミーボールがクリックされました。");
                Destroy(gameObject);
                break;
        }
        
    }

}
