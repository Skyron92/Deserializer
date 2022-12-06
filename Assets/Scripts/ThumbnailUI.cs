using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThumbnailUI : MonoBehaviour
{
    public static Story CurrentStory;
    [SerializeField] private TextMeshProUGUI StoryName;
    [SerializeField] private static ThumbnailUI _thumbnailUI;
    [SerializeField] private TextMeshProUGUI TitleText;
    [SerializeField] private TextMeshProUGUI DescriptionText;
    [SerializeField] private Transform ChoiceContent;
    [SerializeField] private GameObject ChoicePrefab;
    private string _guid = Guid.NewGuid().ToString();
    private List<ChoiceUI> ChoiceUIs => ChoiceContent.GetComponentsInChildren<ChoiceUI>().ToList();

    private void Awake()
    {
        _thumbnailUI = this;
    }
    
    public void Load(int index) {
        DestroyAllChoices();
        StoryName.text = CurrentStory.StoryName;
        if (index < 0) return; 
        Thumbnail thumbnail = CurrentStory.Thumbnails[index];
        _guid = thumbnail.Guid;
        TitleText.text = thumbnail.Title;
        DescriptionText.text = thumbnail.Description;
        foreach (Choice choice in thumbnail.Choices) {
            GameObject instantiate = Instantiate(ChoicePrefab, ChoiceContent);
            ChoiceUI choiceUI = instantiate.GetComponent<ChoiceUI>();
            choiceUI.Load(choice);
            Button button = instantiate.GetComponent<Button>();
            button.onClick.AddListener(delegate {
                Thumbnail firstOrDefault = CurrentStory.Thumbnails.FirstOrDefault(thumbnail => thumbnail.Guid == choice.ThumbnailGuid);
                int indexOf = CurrentStory.Thumbnails.IndexOf(firstOrDefault);
                Load(indexOf);
            });
        }
    }

    private void DestroyAllChoices() {
        foreach (Transform child in ChoiceContent) {
            Destroy(child.gameObject);
        }
    }


    public static void DisplayStory() {
        if (PlayerPrefs.HasKey(StoryUI.Properties.Prefs.LoadedStory) &&
            Check(PlayerPrefs.GetString(StoryUI.Properties.Prefs.LoadedStory))) {
            Load(PlayerPrefs.GetString(StoryUI.Properties.Prefs.LoadedStory));
            PlayerPrefs.DeleteKey(StoryUI.Properties.Prefs.LoadedStory);
        }
    }

    private static void Load(string storyName) {
            CurrentStory = DataReader.LoadStory(storyName);
            _thumbnailUI.Load(0);
    }

    private static bool Check(string storyName) {
        return DataReader.CheckStory(storyName);
    }
}