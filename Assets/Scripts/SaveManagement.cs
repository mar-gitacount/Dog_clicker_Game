using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManagement : MonoBehaviour
{
    [SerializeField] private SaveLoadStart saveLoadStartPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= 5; i++)
        {
            var saveLoadStart = Instantiate(saveLoadStartPrefab, transform);
            if (saveLoadStart == null)
            {
                Debug.LogError("SaveLoadStartのインスタンス化に失敗しました。");
                continue;
            }
            // 現在のセーブ番号を設定
            saveLoadStart.saveIndex = i;
            saveLoadStart.GetComponentInChildren<UnityEngine.UI.Text>().text = i.ToString(); // ボタンにシーン名を表示
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
