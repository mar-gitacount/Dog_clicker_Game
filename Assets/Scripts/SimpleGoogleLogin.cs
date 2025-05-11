using System;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Google;
using System.Threading.Tasks;

public class SimpleGoogleLogin : MonoBehaviour
{
    async void Start()
    {Debug.Log("Google Login Start");
        await UnityServices.InitializeAsync(); // Unity Services 初期化
        
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            string idToken = await GetGoogleIdToken(); // GoogleサインインしてIDトークン取得

            await AuthenticationService.Instance.SignInWithGoogleAsync(
                idToken,
                new SignInOptions() // サインインオプション
            );

            Debug.Log($"サインイン成功！UserID: {AuthenticationService.Instance.PlayerId}");
        }
    }

    private async Task<string> GetGoogleIdToken()
    {
        GoogleSignIn.Configuration = new GoogleSignInConfiguration
        {
            WebClientId = "427183402862-crca264ptqkevbkf3mpbqc3nvbrdee8f.apps.googleusercontent.com", // あなたのWebClientID
            RequestIdToken = true
        };

        var signInResult = await GoogleSignIn.DefaultInstance.SignIn();
        return signInResult.IdToken;
    }
}
