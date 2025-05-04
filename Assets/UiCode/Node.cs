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

    private void OnMouseDown()
    {
        Managers.Data.handOverData.openLocal = localInfo.poisiton;

        // 다른 UI가 열려 있으면 클릭 무시
        if (!Managers.Ui.IsOnlyDefaultOpen())
            return;
        this.GetComponent<UiEvent>().onClick();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("환경과 충돌");
        if (other == null)
            return;
        Debug.Log($"{other.transform.name} data");
        localInfo.localData = Managers.Data.GetLocalData(other.transform.name);
    }
}
