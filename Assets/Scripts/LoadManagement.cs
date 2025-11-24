using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManagement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private LoadStart loadStartButtonPrefab;
    [SerializeField] private SaveLoadManager saveLoadManager;
    void Start()
    {
        for (int i = 1; i <= 5; i++)
        {
           
            if (saveLoadManager.JsonLoadFromLocal(i) == null)
            {
                Debug.Log(i + "番目のセーブデータは存在しません。");
                continue;
            }
            var loadStartButton = Instantiate(loadStartButtonPrefab, transform);
            loadStartButton.SetLabel(i.ToString()); // ボタンにシーン名を表示
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
