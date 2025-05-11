using System;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Google;
using System.Threading.Tasks;

public class GoogleLoginManager : MonoBehaviour
{
    async void Start()
    {
        try
        {
            // Unity Services 初期化
            await UnityServices.InitializeAsync();
            
            // 既にサインイン済みか確認
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                // 1. Google Sign-In で idToken を取得
                string idToken = await GetGoogleIdToken();
                
                // 2. Unity Authentication でGoogleログイン
                await AuthenticationService.Instance.SignInWithGoogleAsync(
                    idToken,
                    new SignInOptions() // オプション（必要に応じて設定）
                );
                
                // Debug.Log($"サインイン成功！\nUserID: {AuthenticationService.Instance.PlayerId}\nEmail: {AuthenticationService.Instance.PlayerInfo?.Email}");
            }
        }
        catch (AuthenticationException ex)
        {
            Debug.LogError($"認証エラー: {ex.Message}");
        }
        catch (RequestFailedException ex)
        {
            Debug.LogError($"リクエスト失敗: {ex.Message}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"予期せぬエラー: {ex.Message}");
        }
    }

    // Google Sign-In から idToken を取得
    private async Task<string> GetGoogleIdToken()
    {
        GoogleSignIn.Configuration = new GoogleSignInConfiguration
        {
            // WebClientId = "YOUR_WEB_CLIENT_ID", // Google Cloud Consoleで取得
            WebClientId = "427183402862-crca264ptqkevbkf3mpbqc3nvbrdee8f.apps.googleusercontent.com",

            RequestIdToken = true
        };

        var signInResult = await GoogleSignIn.DefaultInstance.SignIn();
        return signInResult.IdToken;
    }
}