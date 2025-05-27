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
        //Set Tag
        SetTag(localInfo.side);
        SetStages();
    }

    public void SetTag(string tag)
    {
        transform.tag = tag;
        switch (tag)
        {
            case "Ally":
                break;
            case "Enemy":
                SetStages();
                break;
        }
    }

    public void SetStages()
    {
        if(localInfo.battleWaves.Count > 0)
        {
            return;
        }
        int waveCount = Random.Range(1, 3);
        for (int i = 0; i < waveCount; i++)
        {
            localInfo.battleWaves.Add(Managers.Pool.GetCreepPool());
        }
        Debug.Log($"{name}'s battleWave Count : {localInfo.battleWaves.Count}");
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
        Debug.Log("����");
        Managers.Data.handOverData.openLocal = localInfo.poisiton;

        if (CompareTag("Ally"))
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

        // �ٸ� UI�� ���� ������ Ŭ�� ����
        if (!Managers.Ui.IsOnlyDefaultOpen())
            return;
        
        Camera.main.GetComponent<CameraController>().FocusOnNode(transform);

        
        this.GetComponent<UiEvent>().onClick();
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ȯ��� �浹");
        if (other == null)
            return;
        Debug.Log($"{other.transform.name} data");
        localInfo.localData = Managers.Data.GetLocalData(other.transform.name);
    }*/
}
