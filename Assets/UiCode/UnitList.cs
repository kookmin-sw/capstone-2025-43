using System.Data.Common;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class UnitList : MonoBehaviour
{
    public bool isOwned;
    public GameObject listContent;
    public List<UnitData> dataList;

    // 기존 리스트 정리 (중복 추가 방지)
    private void Init()
    {
        foreach (Transform child in listContent.transform)
        {
            Destroy(child.gameObject);
        }
    }


    public void SetList()
    {
        Init();

        if (isOwned) 
            dataList = Managers.instance.poolManager.ownHeroData;
        else 
            dataList = Managers.instance.poolManager.onSaleHeroData;

        foreach (UnitData data in dataList)
        {
            // listidx ����
            GameObject go = Managers.instance.resourceManager.Instantiate("ListIdx", listContent.transform);
            if (go == null)
            {
                Debug.LogError("ListIdx 프리팹이 Instantiate되지 않았습니다!");
            }
            go.gameObject.GetComponent<ListIdx>().Init(data);
        }
        Debug.Log("SetList 실행됨: " + gameObject.name);
    }
}
