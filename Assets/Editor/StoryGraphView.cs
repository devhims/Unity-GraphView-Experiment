using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class StoryGraphView : GraphView
{
    public readonly Vector2 DefaultNodeSize = new Vector2(300, 200);

    public StoryGraphView()
    {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        //styleSheets.Add(Resources.Load<StyleSheet>("StoryGraph"));

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();

        AddElement(GenerateEntryPointNode());
    }

    StoryNode GenerateEntryPointNode()
    {
        var node = new StoryNode
        {
            title = "Start",
            GUID = Guid.NewGuid().ToString(),
            CardText = "Entry Point",
            EntryPoint = true
        };

        var generatedPort = GeneratePort(node, Direction.Output);
        generatedPort.portName = "Next";
        node.outputContainer.Add(generatedPort);

        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(new Vector2(100, 200), new Vector2(100, 150)));
        return node;
    }

    Port GeneratePort(StoryNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
    }

    public StoryNode CreateStoryNode(string nodeName)
    {

        var storyNode = new StoryNode
        {
            title = nodeName,
            CardText = nodeName,
            GUID = Guid.NewGuid().ToString()
        };

        var inputPort = GeneratePort(storyNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        storyNode.inputContainer.Add(inputPort);


        storyNode.RefreshExpandedState();
        storyNode.RefreshPorts();

        storyNode.SetPosition(new Rect(new Vector2(100, 100), DefaultNodeSize));

        return storyNode;
    }

    public void CreateNode(string nodeName)
    {
        AddElement(CreateStoryNode(nodeName));
    }
}
