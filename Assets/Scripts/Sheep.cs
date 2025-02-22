using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class Sheep : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sheepRenderder;
    [SerializeField]
    private Sprite cutSheepSprite;

    [SerializeField]
    private Wool woolPrefab;
    // 最初の犬の画像
    private Sprite defaultSprite;

    // 移動速度=ランダム
    private float moveSpeed;

    // 初期化処理
    private void Initialize()
    {
        sheepRenderder.sprite = defaultSprite;
        // transform.position = new Vector3(5,0,0);
        transform.position = new Vector3(5,Random.Range(0.0f,4.0f),0); //初期位置をセット
        moveSpeed = -Random.Range(1.0f,2.0f);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultSprite = sheepRenderder.sprite;
        Initialize();
    }
    private void Shaving()
    {
        sheepRenderder.sprite = cutSheepSprite;
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
