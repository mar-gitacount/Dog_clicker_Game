using UnityEngine;
using TMPro;

public class TextFloatY : MonoBehaviour
{
    [SerializeField] private TMP_Text textUI;

    // 動きの速さ（小さいほどゆっくり）
    [SerializeField] private float speed = 2f;

    // 動く幅
    [SerializeField] private float amplitude = 10f;

    private float startY;

    void Start()
    {
        if (textUI == null)
        {
            Debug.LogError("textUI がアタッチされていません");
            return;
        }

        startY = textUI.rectTransform.anchoredPosition.y;
    }

    void Update()
    {
        RectTransform rect = textUI.rectTransform;

        float y = startY + Mathf.Sin(Time.time * speed) * amplitude;

        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, y);
    }
}
