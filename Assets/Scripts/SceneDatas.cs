using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneDatas:MonoBehaviour
{
    // シングルトンインスタンス
    public static SceneDatas Instance {get; private set;}
    // シーン管理クラスのインスタンス
    private ISceneManager sceneManager;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } 
        else
        {
            // 二つ目のインスタンスは破棄する
            Destroy(gameObject);
            return;
        }
    }


    void Start()
    {
        sceneManager = gameObject.AddComponent<SceneManagerImplementation>();
        
    }
    // ゲーム画面
    public void MainScene()
    {
        SceneManager.LoadScene("SampleScene");
        
        
        // sceneManager.LoadScene("SampleScene");
    }

}