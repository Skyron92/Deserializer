using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataReader : MonoBehaviour
{
    private static readonly string StoryPath = Application.streamingAssetsPath;

    public List<string> StoryFiles => Directory.GetFiles(StoryPath, "*.sty").Select(Path.GetFileName).ToList();

    private void Awake()
    {
        
    }

    public void Load()
    {
        string path = StoryPath;
        if (File.Exists(path)) File.OpenRead(path);
        else Debug.LogError("File not found");
        string fileJson = File.ReadAllText(path);
        Debug.Log(fileJson);
    }
}