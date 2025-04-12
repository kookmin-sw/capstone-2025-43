using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using MyProject.Utils;
using UnityEngine.Animations;
using Unity.VisualScripting;
using System.Threading.Tasks;

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
    private Vector3 controlDirection = Vector3.forward;
    public bool isPassive = false;

    void Start()
    {
        if (componentBase == null)
        {
            componentBase = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        }
    }
    void Update()
    {
        if (isPassive) //Cant Control
        {
        }
        else
        {
            MoveByInputAction_ControlDirection();
            RotateByInputAction("x");
        }
        ZoomByInputAction();
    }

    private void MoveByInputAction_ControlDirection()
    {
        if (IA_Move == null)
            return;

        Vector2 moveInputVal = IA_Move.action.ReadValue<Vector2>();

        Vector3 forward = controlDirection;
        Vector3 right = Vector3.Cross(Vector3.up, forward);

        Vector3 moveDirection = (right * moveInputVal.x + forward * moveInputVal.y).normalized;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
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

    public Vector3 GetControlDirection()
    {
        return controlDirection;
    }
    public void SetControlDirection(Transform start, Transform end)
    {
        controlDirection = (end.position - start.position).normalized;
    }
    public void SetControlDirection(Vector3 direction)
    {
        controlDirection = (direction).normalized;
    }
    public async Task RotateToDirection(Vector3 direction, float rotateSpeed = 5f)
    {
        direction.y = 0;
        if (direction.normalized == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime * 100f);
            await Task.Yield();
        }

        transform.rotation = targetRotation;
    }

    public void RotateTowardsDirection(Vector3 Direction)
    {
        if (controlDirection.sqrMagnitude > 0.0001f)
        {
            transform.rotation = Quaternion.LookRotation(Direction);
        }
    }

}
