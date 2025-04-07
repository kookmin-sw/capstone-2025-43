using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using MyProject.Utils;

public class PlayerController : MonoBehaviour
{
    //virtual Camera Options
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    CinemachineComponentBase componentBase;

    [SerializeField] InputActionReference IA_Move;
    [SerializeField] InputActionReference IA_Zoom;
    [SerializeField] InputActionReference IA_Rotate;

    public float moveSpeed;
    public float zoomSensitivity;
    public Boundary1D<float> zoomBoundary = new Boundary1D<float>(2, 10);
    public float rotationSpeed;
    public float rotationResetSpeed;
    public float rotationResetAngle;

    void Start()
    {
        if (componentBase == null)
        {
            componentBase = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        }
    }
    void Update()
    {
        MoveByInputAction();
        ZoomByInputAction();
        RotateByInputAction();
    }
    private void MoveByInputAction()
    {
        if (IA_Move == null)
            return;

        Vector2 moveInputVal = IA_Move.action.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInputVal.x, 0, moveInputVal.y).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    private void ZoomByInputAction()
    {
        if(IA_Zoom == null)
            return;

        float zoomInputVal = IA_Zoom.action.ReadValue<float>();
        if (zoomInputVal != 0)
        {
            if (componentBase is CinemachineFramingTransposer transposer)
            {
                transposer.m_CameraDistance = Mathf.Clamp(
                    transposer.m_CameraDistance - zoomInputVal * zoomSensitivity * Time.deltaTime,
                    zoomBoundary.min,
                    zoomBoundary.max
                );
            }
        }
    }
    private void RotateByInputAction()
    {
        if (IA_Rotate == null)
            return;
        float rotateInputVal = IA_Rotate.action.ReadValue<float>();

        if (rotateInputVal != 0)
        {
            transform.Rotate(Vector3.up * rotateInputVal * rotationSpeed * Time.deltaTime);
        }
        else
        {
            //Get Current YAngle (Translate to -180 ~ 180)
            float currentYAngle = transform.eulerAngles.y;
            if (currentYAngle > 180) currentYAngle -= 360;

            // if YAngle is lower than rotationResetAngle slowly Turn to ZeroAngle
            if (Mathf.Abs(currentYAngle) < rotationResetAngle)
            {
                float newRotation = Mathf.Lerp(currentYAngle, 0, rotationResetSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, newRotation, 0);
            }
        }

    }

}
