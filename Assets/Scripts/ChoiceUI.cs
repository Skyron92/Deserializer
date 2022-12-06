using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ChoiceUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI _descriptionInputField;
    
    public void Load(Choice choice) {
        _descriptionInputField.text = choice.Description;
        
    }
    
}