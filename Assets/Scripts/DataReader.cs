using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FullSerializer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class DataReader : MonoBehaviour
{
    private static readonly fsSerializer Serializer = new fsSerializer();
    private static readonly string StoryPath = Application.streamingAssetsPath;
    [SerializeField] public TextMeshPro Title;
    [SerializeField] public GameObject ButtonPrefab;
    [SerializeField] public Transform panel;

    public List<string> StoryFiles => Directory.GetFiles(StoryPath, "*.sty").Select(Path.GetFileName).ToList();

    public void DisplayAllFoundedFiles()
    {
        foreach (string variableFile in StoryFiles)
        {
            GameObject tempButton = Instantiate(ButtonPrefab, panel);
            TextMeshPro text = tempButton.GetComponentInChildren<TextMeshPro>();
            text.text = variableFile;
        }
    }
    public Story Load(string FileTarget)
    {
        string path = StoryPath + Path.DirectorySeparatorChar + FileTarget;
        foreach (string _path in StoryFiles) {
            if (File.Exists(path)) File.OpenRead(path);
            else Debug.LogError("File not found");
        }
        string fileJson = File.ReadAllText(path);
        Debug.Log(fileJson);
        return Deserialize(typeof(Story), fileJson) as Story;
    }

    private static object Deserialize(Type type, string jsonTarget)
    {
        fsData data = fsJsonParser.Parse(jsonTarget);

        object deserialized = null;
        Serializer.TryDeserialize(data, type, ref deserialized).AssertSuccessWithoutWarnings();
        return deserialized;
    }
    
}