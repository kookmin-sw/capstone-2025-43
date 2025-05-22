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
    private void OnMouseDown()
    {
        Debug.Log("����");
        Managers.Data.handOverData.openLocal = localInfo.poisiton;

        if (CompareTag("Ally"))
            return;
            
        // �ٸ� UI�� ���� ������ Ŭ�� ����
        if (!Managers.Ui.IsOnlyDefaultOpen())
            return;
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
