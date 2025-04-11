using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;
public class Node : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 0.5f, -0.5f);
    public Vector2 pin;
    public int roundNum;
    public List<List<Dictionary<string, int>>> rounds;
    public LocalInfo localInfo;

    public void Init(string tag , Vector3 position)
    {
        pin = position;
        //Set Position
        transform.position = position + offset;
        //Set Tag
        SetTag(tag);
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
        for(int i = 0; i < roundNum; i++)
        {
            rounds.Add(Managers.instance.poolManager.GetCreepPool());
        }
    }
}
