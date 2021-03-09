using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoftKeyboard : MonoBehaviour
{

    #region Public Variables
    [Header("User defined")]
    [Tooltip("If the character is uppercase at the initialization")]
    public bool isUppercase = false;


    [Header("UI Inputfields")]
    //public Text[] inputFields;
    public TMP_InputField[] inputFields;
    int selectedField = 0;

    [Header("Essentials")]
    public Transform characters;
    #endregion


    #region Private Variables
    private string Input
    {
        get { return inputFields[selectedField].text; }
        set { inputFields[selectedField].text = value; }
    }

    private Dictionary<GameObject, Text> keysDictionary = new Dictionary<GameObject, Text>();

    private bool capslockFlag;
    #endregion

    #region Monobehaviour Callbacks
    private void Awake()
    {
        capslockFlag = isUppercase;
        CapsLock();
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }


    #region Public Methods
    public void Backspace()
    {
        if (inputFields[selectedField].text.Length > 0)
        {
            inputFields[selectedField].text = inputFields[selectedField].text.Remove(Input.Length - 1);
        }
        else
        {
            return;
        }
    }

    public void Clear()
    {
        inputFields[selectedField].text = "";
    }

    public void CapsLock()
    {
        if (capslockFlag)
        {
            foreach (Text field in transform.GetComponentsInChildren<Text>())
            {
                if(field.text.ToLower() == "backspace" || field.text.ToLower() == "caps lock" || field.text.ToLower() == "clear all" || field.text.ToLower() == ".com")
                    continue;
                field.text = field.text.ToUpper();
            }
        }
        else
        {
            foreach (Text field in transform.GetComponentsInChildren<Text>())
            {
                if (field.text.ToLower() == "backspace" || field.text.ToLower() == "caps lock" || field.text.ToLower() == "clear all" || field.text.ToLower() == ".com")
                    continue;
                field.text = field.text.ToLower();
            }
        }
        capslockFlag = !capslockFlag;
    }

    public void changeFiled(int fieldNumber)
    {
        Debug.Log("Filed Changed: "+ fieldNumber);
        selectedField = fieldNumber;
    }

    public void keyPressed(string key)
    {
        var regexItem = new Regex("^[a-zA-Z]*$");
        if (regexItem.IsMatch(key))
        {
            if (!capslockFlag)
                inputFields[selectedField].text += key.ToUpper();
            else
                inputFields[selectedField].text += key.ToLower();
        }
        else
            inputFields[selectedField].text += key;
    }
    #endregion

    #region Private Methods
    public void GenerateInput(string s)
    {
        if (inputFields[selectedField].text.Length > inputFields[selectedField].characterLimit) { return; }
        inputFields[selectedField].text += s;
    }

    private string ToLowerCase(string s)
    {
        return s.ToLower();
    }

    private string ToUpperCase(string s)
    {
        return s.ToUpper();
    }
    #endregion
}
