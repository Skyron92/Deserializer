using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ThumbnailUI : MonoBehaviour
{
    public static Story CurrentStory;
    [SerializeField] private TextMeshProUGUI StoryName;
    [SerializeField] private ThumbnailUI _thumbnailUI;
    [SerializeField] private TextMeshProUGUI TitleInputField;
    [SerializeField] private TextMeshProUGUI DescriptionInputField;
    [SerializeField] private Transform ChoiceContent;
    [SerializeField] private GameObject ChoicePrefab;
    private string _guid = Guid.NewGuid().ToString();
    private List<ChoiceUI> ChoiceUIs => ChoiceContent.GetComponentsInChildren<ChoiceUI>().ToList();
    public void Load(int index) {
        StoryName.text = CurrentStory.StoryName;
        Thumbnail thumbnail = CurrentStory.Thumbnails[index];
        _guid = thumbnail.Guid;
        TitleInputField.text = thumbnail.Title;
        DescriptionInputField.text = thumbnail.Description;
        foreach (Choice choice in thumbnail.Choices) {
            GameObject instantiate = Instantiate(ChoicePrefab, ChoiceContent);
            ChoiceUI choiceUI = instantiate.GetComponent<ChoiceUI>();
            choiceUI.Load(choice);
        }
    }
    
    public void MaMethodeDeMerde() {
        if (PlayerPrefs.HasKey(StoryUI.Properties.Prefs.LoadedStory) &&
            Check(PlayerPrefs.GetString(StoryUI.Properties.Prefs.LoadedStory))) {
            Load(PlayerPrefs.GetString(StoryUI.Properties.Prefs.LoadedStory));
            PlayerPrefs.DeleteKey(StoryUI.Properties.Prefs.LoadedStory);
            //Nique toi *2
        }
    }

    private void Load(string storyName) {
            CurrentStory = DataReader.LoadStory(storyName);
            _thumbnailUI.Load(0);
    }

    private static bool Check(string storyName) {
        return DataReader.CheckStory(storyName);
    }
}