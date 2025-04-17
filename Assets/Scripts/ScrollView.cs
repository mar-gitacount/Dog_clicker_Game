using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]

public class ScrollView : MonoBehaviour
{
    //! コンテントデータを取得し、適したサイズに設定等をする。
    [SerializeField] private RectTransform originalContent; // 元のContent
    private RectTransform currentContent;

    // Start is called before the first frame update
   void Start()
{
    ScrollRect scrollRect = GetComponent<ScrollRect>();

    // Viewportがnullの場合、新しく作成して設定
    if (scrollRect.viewport == null)
    {
        GameObject viewportObject = new GameObject("Viewport", typeof(RectTransform));
        RectTransform viewportRect = viewportObject.GetComponent<RectTransform>();
        viewportRect.SetParent(transform, false); // ScrollViewの子オブジェクトに設定
        viewportRect.anchorMin = Vector2.zero;
        viewportRect.anchorMax = Vector2.one;
        viewportRect.sizeDelta = Vector2.zero;
        viewportRect.pivot = new Vector2(0.5f, 0.5f);

        // MaskとImageコンポーネントを追加（ScrollRectに必要）
        viewportObject.AddComponent<Mask>().showMaskGraphic = false;
        viewportObject.AddComponent<Image>().color = new Color(0, 0, 0, 0); // 透明な背景

        // ScrollRectにViewportを設定
        scrollRect.viewport = viewportRect;
    }

    Debug.Log($"{scrollRect.viewport}ビューポートチェック");

    // 元のContentを複製して使用
    currentContent = Instantiate(originalContent, scrollRect.viewport.transform);
    scrollRect.content = currentContent;

    // ドッグコンテンツ一覧を参照する。
    Debug.Log($"{currentContent.name}スクロール確認。");
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
