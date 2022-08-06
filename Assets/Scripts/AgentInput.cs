using UnityEngine;
using UnityEngine.Events;

public class AgentInput : MonoBehaviour
{


    private Camera mainCamera;

    [field: SerializeField]
    public UnityEvent<Vector3> OnMovementKeyPressed { get; set; }

    [field: SerializeField]
    public UnityEvent<Vector3> OnPointerPositionChanged { get; set; }

    [field: SerializeField]
    public UnityEvent<float> OnKickButtonReleased { get; set; }

    [field: SerializeField]
    public UnityEvent<float> OnKickButtonHeldDown { get; set; }

    private float holdDownStartTime;
    [SerializeField][Range(0, 100)] private float maxForce = 50;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        GetMovementInput();
        GetPointerInput();
        GetKickInput();
    }

    //private void GetKickInput()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        OnKickButtonClicked?.Invoke();
    //    }
    //}
    private void GetKickInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            holdDownStartTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            float holdDownTime = Time.time - holdDownStartTime;
            OnKickButtonReleased?.Invoke(CalculateHoldDownForce(holdDownTime));
        }

        if (Input.GetMouseButton(0))
        {
            float holdDownTime = Time.time - holdDownStartTime;
            OnKickButtonHeldDown?.Invoke(CalculateHoldDownForce(holdDownTime));
        }


    }

    private float CalculateHoldDownForce(float holdTime)
    {
        float maxForceHoldDownTime = 2f;
        float holdTimeNormalized = Mathf.Clamp01(holdTime / maxForceHoldDownTime);
        float force = holdTimeNormalized * maxForce;
        return force;

    }


    private void GetPointerInput()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Field")
        {
            OnPointerPositionChanged?.Invoke(hit.point);
        }
    }

    private void GetMovementInput()
    {
        OnMovementKeyPressed?.Invoke(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")));
    }


}
