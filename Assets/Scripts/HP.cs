using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    // Start is called before the first frame update
    public float hp = 4;
    [SerializeField] private Text hpText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // hp
        hpText.text = hp.ToString();
    }
}
