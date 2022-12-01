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
    public enum MyEnum : int
    {
        A, B, C, D
    }
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
            gameObject.SetActive(false);
        }, () => {
            Dialog = 2;
            gameObject.SetActive(false);
        });
        return Dialog;
        
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
