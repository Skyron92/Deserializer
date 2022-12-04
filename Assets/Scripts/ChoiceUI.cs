using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ChoiceUI : MonoBehaviour {

    private TextMeshProUGUI _descriptionInputField;

    /// <summary>
    /// Load an existing choice
    /// </summary>
    /// <param name="choice"></param>
    public void Load(Choice choice) {
        _descriptionInputField.text = choice.Description;
    }
    
}