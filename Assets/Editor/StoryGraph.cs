using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class StoryGraph : EditorWindow
{
    StoryGraphView _graphView;
    string _fileName = "New Story";

    [MenuItem("Graph/Story Graph")]
    public static void OpenDialogGraphWindow()
    {
        var window = GetWindow<StoryGraph>();
        window.titleContent = new GUIContent("Story Graph");
    }

    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolbar();
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


    void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        var fileNameTextField = new TextField("File Name");
        fileNameTextField.SetValueWithoutNotify(_fileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback((evt) => _fileName = evt.newValue);

        toolbar.Add(fileNameTextField);

        var nodeCreateButton = new Button(() => { _graphView.CreateNode("Story Node"); });
        nodeCreateButton.text = "Create Node";

        toolbar.Add(nodeCreateButton);
        rootVisualElement.Add(toolbar);
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }
}
