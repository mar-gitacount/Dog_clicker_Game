using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
public class TitleSchene : MonoBehaviour
{
    // ショップの一覧を作成
    // [SerializeField]Shop shop;
    // デモ用犬データ一覧
    // [SerializeField]private DogButton dogButtonPrefab;
    public List<DogButton> dogButtonList;
    // [SerializeField]private DogGenerator dogGenerator;


    // すでに8個のデータが用意されている。
    public DogData[] dogDatas;
    public DogGenerator dogGenerator;

    public void CreateDog()
    {

    }
    void Awake()
    {
       
    }
    void Start()
    {
         foreach(var dogData in dogDatas)
        {
            var dogCnt = 10;
            for (var i = 0; i < dogCnt; i++ )
            {
                dogGenerator.CreateDog(dogData);

            }
            
        }
    }


}