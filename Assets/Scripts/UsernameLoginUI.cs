using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Core;
using UnityEngine.SceneManagement;
using Unity.Services.Authentication;
using System.Threading.Tasks;

public class UsernameLoginUI : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Button signUpButton;
    public Button loginButton;
    public Text logText;
    private ISaveData saveData;

    async void Start()
    {
        await InitializeUnityServices();
        saveData = new PlayerPrefsSaveData();

        // ログインされている場合、処理をスキップ
        if (AuthenticationService.Instance.IsSignedIn)
        {
            Log($"既にログイン済み: {AuthenticationService.Instance.PlayerId}");
            return;  // ログイン済みの場合は処理を終了
        }

        Log("ログインしていません。サインアップまたはログインしてください。");

        // ローカルに保存されたユーザー名とパスワードを取得
        if (saveData.LoadUserName() != "")
        {
            Log($"ユーザー名: {saveData.LoadUserName()}");
            Log($"パスワード: {saveData.LoadPassword()}");
            usernameInput.text = saveData.LoadUserName();
            passwordInput.text = saveData.LoadPassword();
            Login();  // ロードされた情報でログインを試みる
        }

        signUpButton.onClick.AddListener(() => SignUp());
        loginButton.onClick.AddListener(() => Login());
    }

    async Task InitializeUnityServices()
    {
        try
        {
            await UnityServices.InitializeAsync();
            Log("Unity Services 初期化完了");
        }
        catch (System.Exception ex)
        {
            LogError($"初期化エラー: {ex.Message}");
        }
    }

    // サインアップ処理
    async void SignUp()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            Log("サインアップ成功！");
            // ここで端末にログイン情報を保存。
            saveData.UserName(username);
            saveData.password(password);
        }
        catch (AuthenticationException e)
        {
            LogError($"サインアップ失敗: {e.Message}");
        }
    }

    // ログイン処理
    async void Login()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        // ログインしていない場合にのみサインインを試みる
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            try
            {
                await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
                // ログイン情報を端末に保存
                saveData.UserName(username);
                saveData.password(password);

                Log($"ログイン成功！UserID: {AuthenticationService.Instance.PlayerId}");
                // ここでタイトル画面に行く
                SceneManager.LoadScene("TitleScene"); // タイトル画面のシーン名に変更してください
            }
            catch (AuthenticationException e)
            {
                LogError($"ログイン失敗: {e.Message}");
            }
        }
        else
        {
            Log("すでにログイン済みです");
        }
    }

    // ログメッセージ表示
    void Log(string message)
    {
        Debug.Log(message);
        if (logText != null)
            logText.text = message;
    }

    // エラーログ表示
    void LogError(string message)
    {
        Debug.LogError(message);
        if (logText != null)
            logText.text = message;
    }
}
