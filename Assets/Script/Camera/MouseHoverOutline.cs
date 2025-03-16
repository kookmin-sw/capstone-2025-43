using UnityEngine;

public class MouseHoverOutline : MonoBehaviour
{
    public LayerMask Outlinemask;//������ LayerMask
    private Camera cam;

    // ����ĳ��Ʈ �ֱ� ����
    private float raycastInterval = 0.05f;
    private float nextRaycastTime = 0f;

    private Outline lastOutline; // ���� �ֱ��� �ƿ�����

    void Start()
    {
        cam = Camera.main; // ī�޶� ����
    }

    void Update()
    {
        if (Time.time >= nextRaycastTime)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, Outlinemask))
            {
                Outline outline = hit.collider.GetComponent<Outline>();
                if (outline != null)
                {
                    if (lastOutline != outline) // ���ο� ������Ʈ�� ȣ������ ��
                    {
                        ResetLastOutline(); // ���� ������Ʈ�� �ƿ����� ��Ȱ��ȭ

                        lastOutline = outline; 
                        lastOutline.enabled = true;
                    }
                }
            }
            else
            {
                ResetLastOutline(); // ���콺�� ������Ʈ ������ ������ �ƿ����� ��Ȱ��ȭ
            }

            nextRaycastTime = Time.time + raycastInterval; // ����ĳ��Ʈ �ֱ� ����
        }
    }

    void ResetLastOutline()
    {
        if (lastOutline != null)
        {
            lastOutline.enabled = false; // ���� ������Ʈ�� �ƿ����� ��Ȱ��ȭ
            lastOutline = null; // �ƿ����� �ʱ�ȭ
        }
    }
}
