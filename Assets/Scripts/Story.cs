using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class Story : MonoBehaviour
{ 
        public static string StoryName;
        public List<Thumbnail> Thumbnails;

        public Story(string storyName) {
            StoryName = storyName;
            Thumbnails = new List<Thumbnail>();
        }
    
    } 
