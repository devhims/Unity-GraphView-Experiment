using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GraphSaveUtility
{

    StoryGraphView _targetGraphView;

    List<Edge> Edges => _targetGraphView.edges.ToList();
    List<StoryNode> Nodes => _targetGraphView.nodes.ToList().Cast<StoryNode>().ToList();


    public static GraphSaveUtility GetInstance(StoryGraphView targetGraphView)
    {
        return new GraphSaveUtility
        {
            _targetGraphView = targetGraphView
        };
    }

    public void SaveGraph(string fileName)
    {
        if (!Edges.Any()) return;

        var dialogContainer = ScriptableObject.CreateInstance<StoryContainer>();
        var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();

        for (var i = 0; i < connectedPorts.Length; i++)
        {
            var outputNode = connectedPorts[i].output.node as StoryNode;
            var inputNode = connectedPorts[i].input.node as StoryNode;

            dialogContainer.NodeLinks.Add(new NodeLinkData
            {
                BaseNodeGUID = outputNode.GUID,
                PortName = connectedPorts[i].output.portName,
                TargetNodeGUID = inputNode.GUID
            });
        }

        foreach (var node in Nodes.Where(node => !node.EntryPoint))
        {
            dialogContainer.DialogueNodeData.Add(new StoryNodeData
            {
                NodeGUID = node.GUID,
                StoryTitle = node.CardTitle,
                StoryText = node.CardText,
                Position = node.GetPosition().position
            });
        }

        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
        {
            AssetDatabase.CreateFolder("Assets", "Resources");
        }

        AssetDatabase.CreateAsset(dialogContainer, $"Assets/Resources/{fileName}.asset");
        AssetDatabase.SaveAssets();

    }

}
