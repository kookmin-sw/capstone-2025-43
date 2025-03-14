using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor.EventSystems;
using Unity.VisualScripting;

public class Icon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public IconData data;
    GameObject desc;
    GameObject image;

    private void Awake() // ���� �ʱ�ȭ
    {
        image = transform.GetChild(0).gameObject;
        desc = transform.GetChild(1).gameObject;

        Init();
    }

    private void Init() // ���� + �̹��� �ʱ�ȭ
    {
        image.GetComponent<Image>().sprite = data.iconImg;
        desc.GetComponentInChildren<Image>().sprite = data.descImg;
        desc.GetComponentInChildren<Text>().text = data.descText;
    }



    public void OnPointerEnter(PointerEventData data) // ���콺�� img�� �ö������ 1ȸ ����
    {
        desc.SetActive(true);
    }
    public void OnPointerExit(PointerEventData data) // ���콺�� img���� ����� 1ȸ ����
    {
        desc.SetActive(false);
    }
}