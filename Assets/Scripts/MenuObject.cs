using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuObject : MonoBehaviour
{
    [SerializeField] private Button menuButton;
    [SerializeField] private BuckButtonCanvas backbutton;

    // Start is called before the first frame update
    void Start()
    {
        menuButton.onClick.AddListener(MenuButtonClicked);
        
    }
    private void MenuButtonClicked()
    {
        // nomalFilter.gameObject.SetActive(true);
        backbutton.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
