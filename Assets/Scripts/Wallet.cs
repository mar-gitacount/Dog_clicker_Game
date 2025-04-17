using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class Wallet : MonoBehaviour
{
    public BigInteger money;
    [SerializeField]
    private Text waletText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // waletText.text = money.ToString("C0");
        waletText.text = money.ToString();

        
    }
}
