using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DogGenerator : MonoBehaviour
{
    // ドッグプレハブ＝画像を生成する。画像を生成するのでここをいじる？
    // デフォルトの犬の画像がセットされている。
    [SerializeField]private Sheep dogPrefab;
    // ドッグデータをランダムで受け取って生成する。
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void CreateDog(DogData dogData)
    {
        var dog = Instantiate(dogPrefab);
        // !以下のdogData.pcturePathがあるので、画像をセットする。
        Debug.Log($"{dogData.picturePath}を確認する。ここに犬データが格納されている。");
        dog.dogData = dogData;
    }

    // Update is called once per frame
}
