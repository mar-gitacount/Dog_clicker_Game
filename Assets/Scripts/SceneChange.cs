using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
     // シーン管理クラスのインスタンス
    private ISceneManager sceneManager;
    private SceneDatas sceneDatas;
    
    // シーン切り替え
    // メインシーン
    [SerializeField]private Button MainScenebutoon;
    private void ChangeScene(string sceneName)
    {
        // シーン切り替え。
        SceneManager.LoadScene(sceneName);
    }
    private void MainScene()
    {
        SceneManager.LoadScene("SampleScene");

    }
    //! シーン切り替えテスト消す。
    private  void testChange()
    {

        SceneManager.LoadScene("SampleScene");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // シーンマネージャーをAddComponentでインスタンス化
        sceneManager = gameObject.AddComponent<SceneManagerImplementation>();
        // タイトル起動。
        if(MainScenebutoon == null)
        {
            Debug.Log("ボタンが設定されていない");
        }
        else
        {
        //! シーン切り替えの関数を設定する。
        //   MainScenebutoon.onClick.AddListener(sceneDatas.MainScene);
        // !以下だと動く。
          MainScenebutoon.onClick.AddListener(MainScene);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
