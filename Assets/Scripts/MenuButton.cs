using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;


public class MenuButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    // ?メニューの名前、他に方法があれば書き換える。なんかセンスないコードの書き方。
    [SerializeField] public Text menuName;
    [SerializeField]private GameObject PanelItem;
    
    // 以下は他のタブ一覧を格納Updateで確認
    // public ItemData itemData;
    // 当ボタンをアクティブにし、他のボタンを非アクティブにする。
    // 引数を受け取り、それを一番上にする
    public void itemlastCurst(GameObject item){
        
        Debug.Log($"{item.name}はゲームオブジェクト");        
    }

    public void anotherButtonDisable()
    {
        // !親オブジェクト=MenuContent
        Transform parent = transform.parent;
        // !祖父オブジェクトを取得する。
        Transform grandParent = transform.parent?.parent;
        // フィルター
        Transform Filter = grandParent.Find("Filter");
        Transform parentsSibling = grandParent.Find(menuName.text);
        parentsSibling.gameObject.SetActive(true);
        // このゲームオブジェクトの順序を変更
        parentsSibling.transform.SetAsLastSibling();
        foreach (Transform p in grandParent)
        {
            if(p.name == "MenuContent")continue;
            if(p.name != menuName.text)
            {
                Transform atherMenu = grandParent.Find(p.name);
                // 他のオブジェクトをOFFにする
                atherMenu.gameObject.SetActive(false);
            }
        }
        foreach(Transform sibling in parent)
        {
        //!兄妹ボタンを示しているので一致しない
            Debug.Log($"{menuName.text}はメニューボタン　兄妹ボタン{sibling.name}");
            if(sibling.name == "MenuButton")
            {
                continue;
            }
            if(menuName.text == sibling.name)
            {
                sibling.gameObject.SetActive(true);
                continue;
            }
            // sibling.gameObject.SetActive(false);
            // メニューと同じ階層にあるショップ一覧
            
        }
        // trueの場合ボタンの色を変える。
        // MenuContentはFalseにしない。
        
    }

    public void ToggleActiveState(GameObject targetObject)
    {
        if (targetObject != null)
        {
            // 現在のアクティブ状態を反転
            bool isActive = targetObject.activeSelf;
            targetObject.SetActive(!isActive);

            Debug.Log($"{targetObject.name}のアクティブ状態を{!isActive}に切り替えました。");
        }
        else
        {
            Debug.LogError("ターゲットオブジェクトが設定されていません！");
        }
    }

    public void MoveToBottom(GameObject targetObject)
    {
        if (targetObject != null)
        {
            // ヒエラルキー内で一番下に移動
            targetObject.transform.SetAsLastSibling();
            Debug.Log($"{targetObject.name} をヒエラルキーの一番下に移動しました。");
        }
        else
        {
            Debug.LogError("ターゲットオブジェクトが設定されていません！");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // ボタンの高さをCanvas基準で上から600pxに設定
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            // 親CanvasのRectTransformを取得
            RectTransform canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
            if (canvasRect != null)
            {
                // Canvasの高さを基準に計算
                float canvasHeight = canvasRect.rect.height;
                Debug.Log($"Canvasの高さ: {canvasHeight}");
                float targetYPosition = canvasHeight / 2 - 600; // 上から600px

                // anchoredPositionを設定
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, targetYPosition);
                Debug.Log($"ボタンの高さをCanvas基準で上から600pxに設定しました: {rectTransform.anchoredPosition}");
            }
            else
            {
                Debug.LogError("親CanvasのRectTransformが見つかりません！");
            }
        }
        else
        {
            Debug.LogError("RectTransformが見つかりません！");
        }

        // ボタンのクリックイベントを設定
        // button.onClick.AddListener(anotherButtonDisable);
        // button.onClick.AddListener(() => ToggleActiveState(PanelItem));
        button.onClick.AddListener(() => MoveToBottom(PanelItem));
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log($"{menuName.text}はボタン名");
    }
}
