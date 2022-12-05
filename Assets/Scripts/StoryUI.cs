using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StoryUI : MonoBehaviour
{
    public void Do() {
        ThumbnailUI.MaMethodeDeMerde();
    }
    public static class Properties{
    

        public struct Prefs {
            public const string LoadedStory = "LoadedStory";
        }
        
        
    }
}