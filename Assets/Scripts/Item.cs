using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // !赤文字コメントや変数はあとで設定する。
    //! アイテム画像
    //! [SerializeField]private SpriteRenderer itemRenderer
    // [SerializeField]private Item itemhab;
    public ItemData itemData;
   //道具を使う。 
    void Start()
    {
        // ?とりあえずゲーム画面上のオブジェクトが取得できるか確認する。
        // ?アイテムの特性スクリプトが無い場合、以下を実行する。
        // ?アイテムの特性はアイテムデータオブジェクトをつかって特性を設定する。
        // ?アイテムボタンオブジェクトを押下したら実行
        Debug.Log("アイテムを使用しました。");
        // 関数実行。
        // itemData.InvokeFunction();
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("アイテム");
        
    }
   private void OnMouseOver()
   {
     if(Input.GetMouseButton(0)== false) return;
     Debug.Log("クリックされました。");
     itemData.InvokeFunction();
   }
}
