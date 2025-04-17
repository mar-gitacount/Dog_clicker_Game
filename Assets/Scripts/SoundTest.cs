// using UnityEditor.AssetImporters;
using UnityEngine;

public class SoundTest : MonoBehaviour
{

   [SerializeField]private SoundManeger soundManeger;
   [SerializeField]private AudioClip clip1;
   [SerializeField]private AudioClip clip2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        // 左クリック
        if(Input.GetMouseButton(0))
        {
            soundManeger.Play("左クリック");
        }
        // 右クリニック
        if(Input.GetMouseButton(1))
        {
            soundManeger.Play("右クリック");
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            soundManeger.Play("Xキー");
        }
        if(Input.GetKeyDown(KeyCode.Y))
        {
            soundManeger.Play("Yキー");
        }
        // 改悪にした。押し続けるとなる
        if(Input.GetKey(KeyCode.C))
        {
            soundManeger.Play("Cキー");
        }
        
    }
}
