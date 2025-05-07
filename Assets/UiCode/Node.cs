using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.AddressableAssets.Build.BuildPipelineTasks;
public class Node : MonoBehaviour
{
    public LocalInfo localInfo;
    public Vector3 offset = new Vector3(0, 0.5f, -0.5f);
    public void Init(LocalInfo inputInfo)
    {
        localInfo = inputInfo;
        //Set Position
        transform.position = localInfo.poisiton + offset;
        //Set Tag
        SetTag(localInfo.side);
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
        int waveCount = Random.Range(1, 3);
        for (int i = 0; i < waveCount; i++)
        {
            localInfo.battleWaves.Add(Managers.Pool.GetCreepPool());
        }
    }
    /*
    private bool IsAdjacentToAlly()
    {
        Vector2 myPos = (Vector2)transform.position;
        Debug.Log($"[Node] 내 위치: {myPos}");

        foreach (var edge in Managers.Data.handOverData.roads)
        {
            Vector2 neighborPos = Vector2.zero;
            bool isConnected = false;

            if (ApproximatelyEqual(edge.v0, myPos))
            {
                neighborPos = edge.v1;
                isConnected = true;
            }
            else if (ApproximatelyEqual(edge.v1, myPos))
            {
                neighborPos = edge.v0;
                isConnected = true;
            }

            if (isConnected && Managers.Data.handOverData.localInfos.TryGetValue(neighborPos, out var neighborInfo))
            {
                Debug.Log($"[Edge] {edge.v0} <-> {edge.v1}");
                if (neighborInfo.side == "Ally")
                    return true;
            }
        }

        return false;
    }

    private bool ApproximatelyEqual(Vector2 a, Vector2 b, float tolerance = 0.01f)
    {
        return Vector2.Distance(a, b) < tolerance;
    }
    */

    
    private void OnMouseDown()
    {
        if (Managers.Game.time != 0)
        {
            Debug.Log("노드는 아침에만 상호작용할 수 있습니다.");
            return;
        }

        if (CompareTag("Ally"))
            return;
        /*
        if (CompareTag("Enemy") && !IsAdjacentToAlly())
            return;
        */
            
        // �ٸ� UI�� ���� ������ Ŭ�� ����
        if (!Managers.Ui.IsOnlyDefaultOpen())
            return;

        Managers.Data.handOverData.openLocal = localInfo.poisiton;
        this.GetComponent<UiEvent>().onClick();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision == null)
            return;
        Debug.Log($"{collision.transform.name} data");
        localInfo.localData = Managers.Data.GetLocalData(collision.transform.name);
    }
}
