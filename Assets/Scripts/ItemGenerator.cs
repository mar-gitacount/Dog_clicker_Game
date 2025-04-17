using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    // 道具プレハブをセットする。道具の特性は、おそらく道具プレハブ内で修正する。
    [SerializeField]private Item itemPrefab;
    
    // アイテムを利用するために作る。
    public void CreateItem(ItemData itemData)

    {
        // アイテムの初期化。
        var item = Instantiate(itemPrefab);
        item.itemData = itemData;

    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 道具を使う

}
