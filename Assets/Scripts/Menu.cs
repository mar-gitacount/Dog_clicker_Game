using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private MenuButton menuButtonPrefab;
    public List<MenuButton> menuButtonList;
    // 以下にゲームオブジェクトをセットする。
    [SerializeField] private GameObject ScrollObject;
    void Awake()
    {
        var menuButton = Instantiate(menuButtonPrefab,transform);
        // 引数にゲームオブジェクトを渡して表示非表示の設定をする。s
        menuButton.itemlastCurst(ScrollObject);
        Transform parent = transform.parent;
        // menuButtonの祖父オブジェクトを取得する
        // 親オブジェクトの兄妹オブジェクトにいれる。
        
        Transform grandParent = menuButton.transform.parent?.parent;
        menuButton.transform.SetParent(grandParent,false);
        
        // !ゲームオブジェクトをセットする。
        foreach(Transform sibling in parent)
        {

            // var menuButton = Instantiate(menuButtonPrefab,transform);
            // // ?メニュー名を代入する
            // menuButton.menuName.text = sibling.name;
            
            // // メニューと同じ階層にあるショップ一覧
            // Debug.Log($"兄妹{sibling.name}");
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
