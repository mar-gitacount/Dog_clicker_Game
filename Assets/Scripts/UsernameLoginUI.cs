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
    public Button logoutButton;
    public Text logText;
    private ISaveData saveData;

    async void Start()
    {
        await InitializeUnityServices();
        saveData = new PlayerPrefsSaveData();




        // ローカルに保存されたユーザー名とパスワードを取得
        if (saveData.LoadUserName() != "")
        {
            Log($"ユーザー名: {saveData.LoadUserName()}");
            Log($"パスワード: {saveData.LoadPassword()}");
            usernameInput.text = saveData.LoadUserName();
            passwordInput.text = saveData.LoadPassword();
            // Login();  // ロードされた情報でログインを試みる
        }

        signUpButton.onClick.AddListener(() => SignUp());
        loginButton.onClick.AddListener(() => Login());
        logoutButton.onClick.AddListener(() => Logout());
        // ログインされている場合、処理をスキップ
        if (AuthenticationService.Instance.IsSignedIn)
        {
            signUpButton.interactable = false;
            Log($"既にログイン済み: {AuthenticationService.Instance.PlayerId}");
            return;  // ログイン済みの場合は処理を終了
        }


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
            SceneManager.LoadScene("TitleScene"); // タイトル画面のシーン名に変更してください

        }
        catch (AuthenticationException e)
        {
            // LogError($"サインアップ失敗: {e.Message}");
            Log($"サインアップ失敗{e.Message}");
            // await UnityServices.InitializeAsync();
        }
        catch (System.Exception e)
        {
            Log("エラー" + e.Message);
            // LogError($"予期しないエラー: {e.Message}");
        }
    }

    // ログイン処理
    async void Login()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        // ログイン処理開始時にボタンを無効化
        // loginButton.interactable = false;
        // signUpButton.interactable = false;
        // ユーザー名とパスワードの入力欄の空チェック

        // ログインしていない場合にのみサインインを試みる
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            Debug.Log("ログイン処理を開始します");
            try
            {
                Debug.Log("tryの中に入りました");
                // ユーザー名とパスワードの入力欄を無効化
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


                Log($"ログイン失敗: {e.Message}");
                // Debug.Log($"ログインボタンの状態{loginButton.interactable}");
                // loginButton.interactable = true;
                // Debug.Log($"ログインボタンの状態2{loginButton.interactable}");
                // signUpButton.interactable = true;    // ← 追加
                // logoutButton.interactable = false;    // ← 追加
            }
            catch (System.Exception e)
            {
                Log($"予期しないエラー: {e.Message}");
                // LogError($"予期しないエラー: {e.Message}");
                // loginButton.interactable = true;
                // signUpButton.interactable = true;    // ← 追加
                // logoutButton.interactable = false;    // ← 追加
            }
      
        }
        else
        {
            Debug.Log("タイトル画面の文");
            Log("すでにログイン済みです");
            SceneManager.LoadScene("TitleScene"); // タイトル画面のシーン名に変更してください

        }
    }
    // ログアウト処理
    void Logout()
    {
        if (AuthenticationService.Instance.IsSignedIn)
        {
            AuthenticationService.Instance.SignOut();
            Log("ログアウトしました。");
            // ログアウト後はユーザー名とパスワードをクリア
            saveData.UserName("");
            saveData.password("");
            usernameInput.text = "";
            passwordInput.text = "";
        }
        else
        {
            Log("ログインしていません。");
            // logoutButton.interactable = false; // ログアウトボタンを無効化
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
