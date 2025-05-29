using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.AddressableAssets.Build.BuildPipelineTasks;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public LocalInfo localInfo;
    public Vector3 offset = new Vector3(0, 0, -0.5f);
    public void Init(LocalInfo inputInfo)
    {
        localInfo = inputInfo;
        //Set Position
        transform.position = localInfo.poisiton + offset;
    }
    private List<Node> GetConnectedNodes()
    {
        List<Node> connectedNodes = new List<Node>();

        foreach (Edge edge in Managers.Data.handOverData.roads)
        {
            if (edge.v0.Equals(localInfo.poisiton))
            {
                Node otherNode = GetNodeByPosition(edge.v1);
                if (otherNode != null && otherNode.CompareTag("Ally"))
                {
                    Node enemyNode = GetNodeByPosition(edge.v0);
                    if (enemyNode != null && enemyNode.CompareTag("Enemy"))
                    {
                        connectedNodes.Add(enemyNode);
                    }
                }
            }
            else if (edge.v1.Equals(localInfo.poisiton))
            {
                Node otherNode = GetNodeByPosition(edge.v0);
                if (otherNode != null && otherNode.CompareTag("Ally"))
                {
                    Node enemyNode = GetNodeByPosition(edge.v1);
                    if (enemyNode != null && enemyNode.CompareTag("Enemy"))
                    {
                        connectedNodes.Add(enemyNode);
                    }
                }
            }
        }
        return connectedNodes;
    }

    private Node GetNodeByPosition(Vector2 position)
    {
        Node[] allNodes = Object.FindObjectsByType<Node>(FindObjectsSortMode.None);
        foreach (var node in allNodes)
        {
            if (node.localInfo.poisiton.Equals(position))
            {
                return node;
            }
        }
        return null;
    }

    private void OnMouseDown()
    {
        if (tag == "Ally")
            return;

        List<Node> connectedNodes = GetConnectedNodes();
        bool canAttack = false;

        foreach (Node connectedNode in connectedNodes)
        {
            if (connectedNode.CompareTag("Enemy"))
            {
                canAttack = true;
                break;
            }
        }
        if (!canAttack)
            return;
        if (!Managers.Ui.IsOnlyDefaultOpen())
            return;

        Camera.main.GetComponent<CameraController>().FocusOnNode(transform);
        Managers.Data.handOverData.openLocal = localInfo.poisiton;
        this.GetComponent<UiEvent>().onClick();
    }
}
