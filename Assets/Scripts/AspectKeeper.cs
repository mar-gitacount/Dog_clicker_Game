using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]

public class AspectKeeper : MonoBehaviour
{
    // 対象とするカメラ
    [SerializeField]private Camera targetCamera;
    // 目的解像度
    [SerializeField]private Vector2 aspectVec;

    // Update is called once per frame
    void Update()
    {
        // 画面のアスペクト比
        var screenAspect = Screen.width / (float)Screen.height;
        // 目的のアスペクト比 x = 480 y = 800 
        var targetAspect = aspectVec.x / aspectVec.y;
        // 目的のアスペクト比にするための倍率
        var magrate = targetAspect / screenAspect;
        // Viewport初期値でRectを作成
        var viewportRect = new Rect(0,0,1,1);
        // 使用する横幅を変更
        viewportRect.width = magrate;
        if(magrate < 1 ){
            // 使用する横幅を変更する
            viewportRect.width = magrate;
            // 中央よせ→横幅の半分を画面上の半分から引き、中央に寄せる。
            viewportRect.x = 0.5f - viewportRect.width * 0.5f; 
        }
        else
        {
            // 使用する縦幅を変更する
            viewportRect.height = 1 / magrate;
            // 中央よせ→縦幅の半分を画面上の半分から引き、中央に寄せる。
            viewportRect.y = 0.5f - viewportRect.height * 0.5f;
        }
        // カメラのViewportに適用。
        targetCamera.rect = viewportRect;

        
    }
}
