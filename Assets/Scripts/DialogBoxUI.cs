using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogBoxUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TextMeshPro;
    [SerializeField] private Button yesBtn;
    [SerializeField] private Button noBtn;
    [SerializeField] private InventoryManager manager;
 
    private void Awake()
    {
        gameObject.SetActive(false);

    }

    public int Show()
    {
        int Dialog = 0;
        gameObject.SetActive(true);
        ButtonPress(() => {
            Dialog = 1;
            
        }, () => {
            Dialog = 2;
            
        });
        return Dialog;
        
    }

    public void ButtonPressYes()
    {
        gameObject.SetActive(false);
    }

    public void ButtonPressNo()
    {
        gameObject.SetActive(false);
    }

    private void ButtonPress(Action ButtonYes,Action ButtonNo)
    {
        yesBtn.onClick.RemoveAllListeners();
        noBtn.onClick.RemoveAllListeners();

        yesBtn.onClick.AddListener(() => {
            ButtonYes();
            gameObject.SetActive(false);
        });
        noBtn.onClick.AddListener(() => {
            ButtonNo();
            gameObject.SetActive(false);
        });
    }

}
