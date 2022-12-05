using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StoryUI : MonoBehaviour
{
    private GameObject parent;
    
    public void Do() {
        ThumbnailUI.MaMethodeDeMerde();
        parent = GetComponentInParent<GameObject>();
        parent.SetActive(false);
    }
    public static class Properties{
    

        public struct Prefs {
            public const string LoadedStory = "LoadedStory";
        }
        
        
    }
}