using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FullSerializer;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = System.Object;

public class DataReader : MonoBehaviour {
    private static readonly fsSerializer Serializer = new fsSerializer();
    private static readonly string StoryPath = Application.streamingAssetsPath;
    
    //[SerializeField] public TextMeshPro Title;
    [SerializeField] public GameObject ButtonPrefab, LoadPanel;
    [SerializeField] public Transform panel;

    public static List<string> StoryFiles => Directory.GetFiles(StoryPath, "*.sty").Select(Path.GetFileName).ToList();
    
    public void DisplayAllFoundFiles() {
        foreach (string variableFile in StoryFiles) {
            Button tempButton = Instantiate(ButtonPrefab, panel).GetComponent<Button>();
            TextMeshProUGUI textMeshPro = tempButton.GetComponentInChildren<TextMeshProUGUI>();
            textMeshPro.text = variableFile;
            tempButton.onClick.AddListener(delegate {
                PlayerPrefs.SetString(StoryUI.Properties.Prefs.LoadedStory, textMeshPro.text);
                LoadPanel.SetActive(false);
            });
        }
    }
    
    public static Story LoadStory(string FileTarget) {
        string path = StoryPath + Path.DirectorySeparatorChar + FileTarget;
        foreach (string _path in StoryFiles) {
            if (File.Exists(path)) File.OpenRead(path);
            else Debug.LogError("File not found");
        }

        string fileJson = File.ReadAllText(path);
        Debug.Log(fileJson);
        return Deserialize(typeof(Story), fileJson) as Story;
    }
    public static object Deserialize(Type type, string jsonTarget) {
        fsData data = fsJsonParser.Parse(jsonTarget);
        object deserialized = null;
        Serializer.TryDeserialize(data, type, ref deserialized).AssertSuccessWithoutWarnings();
        return deserialized;
    }
    
    public static bool CheckStory(string storyNameWithExtension) {
        string path = StoryPath + Path.DirectorySeparatorChar + storyNameWithExtension;
        return File.Exists(path);
    }
}