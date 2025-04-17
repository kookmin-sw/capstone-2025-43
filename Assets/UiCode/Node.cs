using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.AddressableAssets.Build.BuildPipelineTasks;
public class Node : MonoBehaviour
{
    public int nodeId;
    public LocalInfo localInfo;
    public Vector3 offset = new Vector3(0, 0.5f, -0.5f);
    public void Init(LocalInfo inputInfo , int id)
    {
        nodeId = id;
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
            localInfo.battleWaves.Add(Managers.instance.poolManager.GetCreepPool());
        }
    }
    private void OnMouseDown()
    {
        Managers.instance.dataManager.handOverData.openLocal = nodeId;
        // 다른 UI가 열려 있으면 클릭 무시
        if (!Managers.instance.uiManager.IsOnlyDefaultOpen())
            return;
        this.GetComponent<UiEvent>().onClick();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision == null)
            return;
        Debug.Log($"{collision.transform.name} data");
        localInfo.localData = Managers.instance.dataManager.GetLocalData(collision.transform.name);
    }
}
