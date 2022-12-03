using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugPanel : MonoBehaviour
{
    private string input;
    [SerializeField] private GameObject inputField;
    public string GetInput()
    {
        try
        {
            input = inputField.GetComponent<TMP_InputField>().text;
        }
        catch
        {
            Debug.Log("No Text found");
            return null;
        }
        if (input != null)
            return input;

        return null;
    }
}
