
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
 
public interface ISceneManager
{
    void LoadScene(string sceneName);  // シーンの読み込み
    void LoadSceneAsync(string sceneName);  // 非同期でシーンを読み込む
    void UnloadScene(string sceneName);  // シーンのアンロード
    void ReloadCurrentScene();  // 現在のシーンを再読み込み
}