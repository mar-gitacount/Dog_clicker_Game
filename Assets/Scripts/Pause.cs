using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    // 一時停止もしくは再生テキスト
    [SerializeField]private Text pauseText;

    [SerializeField]private Button button;
    // 停止中のフィルター
    public GameObject PauseFilter;
    private bool isPause = false;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(PuaseOrPlay);    
    }
    public void PuaseOrPlay()
    {
        isPause = !isPause;
        // ゲーム開始もしくは停止
        if(isPause)
        {
            pauseText.text = "再生";
            Time.timeScale = 0;
            PauseFilter.SetActive(isPause);
        }
        else
        {
            pauseText.text = "停止";
            Time.timeScale = 1;
            PauseFilter.SetActive(isPause);
        }
        // pauseText.text = isPause ? "再生" : "停止";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
