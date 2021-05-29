using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class StoryGraph : EditorWindow
{
    StoryGraphView _graphView;

    [MenuItem("Graph/Story Graph")]
    public static void OpenDialogGraphWindow()
    {
        var window = GetWindow<StoryGraph>();
        window.titleContent = new GUIContent("Story Graph");
    }

    private void OnEnable()
    {
        ConstructGraphView();
    }

    void ConstructGraphView()
    {
        _graphView = new StoryGraphView
        {
            name = "Story Graph"
        };

        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }
}
