using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SaveLoadStart : MonoBehaviour
{
    [SerializeField] private Button saveLoadButton;
    [SerializeField] private Text buttonText;
    // private SaveLoadManager saveLoadManager;
    private ISaveData saveData;
    public int saveIndex;
    
    // 時間とか話数とか保存する。
    // Start is called before the first frame update
    void Start()
    {
        // saveLoadManager.JsonLoadFromLocal(1);
        saveData = new PlayerPrefsSaveData();
        int storyData = saveData.LoadNow();
        saveIndex = int.Parse(buttonText.text);
        buttonText.text = buttonText.text + "ストーリー" + storyData;
        
        saveLoadButton.onClick.AddListener(OnButtonClicked);


    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnButtonClicked()
    {
        saveData = new PlayerPrefsSaveData();
        Debug.Log(saveIndex+ "のOnEnableが呼ばれました。");
        saveData.SavenNow(saveIndex);
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");

    }
}
