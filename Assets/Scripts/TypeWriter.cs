using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
// using UnityEngine.UI;
public class TypeWriter : MonoBehaviour
{
    // public TMP_Text textUI;
    [SerializeField] public TMP_Text textUI;
    public float interval = 0.002f;
    public Dictionary<int, string> storySoundsWithText = new Dictionary<int, string>();
    public string storySoundsData = "";

    public void StartTyping(string text)
    {
        Debug.Log("タイプライタースタートタイピングが呼ばれました。");
        text = text.Replace("\\n", "\n");
        StartCoroutine(TypeRoutine(text));
    }

    IEnumerator TypeRoutine(string message)
    {
        textUI.text = "";
        Debug.Log("ストーリーテキストの音データ"+storySoundsData);
        // SoundManeger.Instance.Play(storySoundsData);
        foreach (char c in message)
        {
          
            Debug.Log("タイプライターで文字を表示中:" + c);
            textUI.text += c;
            SoundManeger.Instance.Play("カーソル移動2");
            yield return null;
            // yield return new WaitForSeconds(interval);
        }
    }
}
