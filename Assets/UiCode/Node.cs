using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
public class Node : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 0.5f, -0.5f);
    public Vector2 pin;
    public List<List<BattleWavePreset>> battlewave = new List<List<BattleWavePreset>>();
    public LocalData localData;

    public void Init(string tag , Vector3 position)
    {
        pin = position;
        //Set Position
        transform.position = position + offset;
        //Set Tag
        SetTag(tag);
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
        int waveCount = Random.Range(1, 3);
        for (int i = 0; i < waveCount; i++)
        {
            battlewave.Add(Managers.instance.poolManager.GetCreepPool());
        }
    }
    private void OnMouseDown()
    {
        Managers.instance.dataManager.handOverData.openLocal = pin;
        // 다른 UI가 열려 있으면 클릭 무시
        if (!UiManager.instance.IsOnlyDefaultOpen())
            return;
        this.GetComponent<UiEvent>().onClick();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision == null)
            return;
        Debug.Log($"{collision.transform.name} data");
        localData = Managers.instance.dataManager.GetLocalData(collision.transform.name);
    }
}
