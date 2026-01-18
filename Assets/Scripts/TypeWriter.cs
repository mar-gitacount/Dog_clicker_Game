using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
// using UnityEngine.UI;
public class TypeWriter : MonoBehaviour
{
    // public TMP_Text textUI;
    [SerializeField] public TMP_Text textUI;
    public float interval = 0.5f;

    public void StartTyping(string text)
    {
        Debug.Log("タイプライタースタートタイピングが呼ばれました。");
        text = text.Replace("\\n", "\n");
        StartCoroutine(TypeRoutine(text));
    }

    IEnumerator TypeRoutine(string message)
    {
        textUI.text = "";
        foreach (char c in message)
        {
            Debug.Log("タイプライターで文字を表示中:" + c);
            textUI.text += c;
            yield return new WaitForSeconds(interval);
        }
    }
}
