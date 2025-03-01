using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DogGenerator : MonoBehaviour
{
    [SerializeField]private Sheep dogPrefab;
    // ドッグデータをランダムで受け取って生成する。
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void CreateDog(DogData dogData)
    {
        var dog = Instantiate(dogPrefab);
        dog.dogData = dogData;
    }

    // Update is called once per frame
}
