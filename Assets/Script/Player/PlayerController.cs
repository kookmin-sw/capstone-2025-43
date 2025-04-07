using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using MyProject.Utils;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    //virtual Camera Options
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    CinemachineComponentBase componentBase;
    [SerializeField] private Boundary1D<float> xAngleLimit = new Boundary1D<float>(-45f, 45f);

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
        RotateByInputAction("x");
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
    private void RotateByInputAction(string axis)
    {
        if (IA_Rotate == null)
            return;

        float rotateInputVal = IA_Rotate.action.ReadValue<float>();
        Vector3 currentEulerAngles = transform.eulerAngles;
        float currentAngle = 0f;
        Vector3 rotationAxis = Vector3.zero;

        switch (axis.ToLower())
        {
            case "x":
                rotationAxis = Vector3.right;
                currentAngle = currentEulerAngles.x;
                break;
            case "y":
                rotationAxis = Vector3.up;
                currentAngle = currentEulerAngles.y;
                break;
            default:
                Debug.LogWarning("Invalid axis provided. Use 'x' or 'y'.");
                return;
        }

        if (currentAngle > 180) currentAngle -= 360;

        if (rotateInputVal != 0)
        {
            float direction = (axis == "x") ? -1f : 1f;
            float rotationDelta = rotateInputVal * rotationSpeed * direction * Time.deltaTime;

            if (axis == "x")
            {
                float newX = Mathf.Clamp(currentAngle + rotationDelta, xAngleLimit.min, xAngleLimit.max);
                transform.rotation = Quaternion.Euler(newX, currentEulerAngles.y, 0);
            }
            else if (axis == "y")
            {
                transform.Rotate(Vector3.up * rotationDelta);
            }
        }
        else
        {
            if (Mathf.Abs(currentAngle) < rotationResetAngle)
            {
                float newRotation = Mathf.Lerp(currentAngle, 0, rotationResetSpeed * Time.deltaTime);

                if (axis == "x")
                {
                    newRotation = Mathf.Clamp(newRotation, xAngleLimit.min, xAngleLimit.max);
                    transform.rotation = Quaternion.Euler(newRotation, currentEulerAngles.y, 0);
                }
                else if (axis == "y")
                {
                    transform.rotation = Quaternion.Euler(0, newRotation, currentEulerAngles.z);
                }
            }
        }
    }
}
