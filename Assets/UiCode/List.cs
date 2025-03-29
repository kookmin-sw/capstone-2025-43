using System.Data.Common;
using UnityEngine;

public class List : MonoBehaviour
{
    public bool own;
    public GameObject listContent;
    public UnitData[] dataList;

    public void SetList()
    {
        dataList = Managers.instance.dataManager.GetUnitDataset("Ally");
        foreach(UnitData data in dataList)
        {
            if (data.own)
            {
                // listidx »ý¼º
                GameObject go = Managers.instance.resourceManager.Instantiate("ListIdx", listContent.transform);
                go.gameObject.GetComponent<ListIdx>().data = data;
                go.transform.position = listContent.transform.position;
            }
        }
    }
}
