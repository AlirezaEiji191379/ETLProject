﻿using ETLProject.Pipeline.Abstractions;

namespace ETLProject.Pipeline.Graph;

public class DataPipelineGraph
{
    private Dictionary<IPlugin, List<IPlugin>> _adjacencyList;
    
    public DataPipelineGraph()
    {
        _adjacencyList = new Dictionary<IPlugin, List<IPlugin>>();
    }


    public List<IPlugin> GetNodeDependencies(Guid nodeId)
    {
        var node = GetNode(nodeId);
        return _adjacencyList[node];
    }

    public void AddVertex(IPlugin vertex)
    {
        if (!_adjacencyList.ContainsKey(vertex))
        {
            _adjacencyList[vertex] = new List<IPlugin>();
        }
    }

    public void AddEdge(IPlugin source, IPlugin destination)
    {
        if (!_adjacencyList.ContainsKey(source))
        {
            AddVertex(source);
        }

        if (!_adjacencyList.ContainsKey(destination))
        {
            AddVertex(destination);
        }

        _adjacencyList[destination].Add(source);
    }
    
    
    private IPlugin GetNode(Guid nodeId)
    {
        return _adjacencyList.Keys.First(node => node.PluginId == nodeId);
    }
    
}