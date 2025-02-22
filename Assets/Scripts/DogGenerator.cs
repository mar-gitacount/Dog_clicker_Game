using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DogGenerator : MonoBehaviour
{
    [SerializeField]
    private Sheep dogPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void CreateDog()
    {
        var dog = Instantiate(dogPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            CreateDog();
        }
    }
}
