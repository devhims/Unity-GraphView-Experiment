using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StoryContainer : ScriptableObject
{
    public List<NodeLinkData> NodeLinks = new List<NodeLinkData>();
    public List<StoryNodeData> DialogueNodeData = new List<StoryNodeData>();
}
