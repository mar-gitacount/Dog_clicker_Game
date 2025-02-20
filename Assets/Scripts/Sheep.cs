using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Sheep : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sheepRenderder;
    [SerializeField]
    private Sprite cutSheepSprite;

    [SerializeField]
    private Wool woolPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void Shaving()
    {
        sheepRenderder.sprite = cutSheepSprite;
        var wool = Instantiate(woolPrefab,transform.position,transform.rotation); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) == false) return;
        Shaving();
        

    
    }
}
