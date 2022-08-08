using UnityEngine;
using UnityEngine.Events;

public class AgentInput : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public bool isActivePlayer = false;

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

    private void Awake()
    {
        mainCamera = Camera.main;
    }



    private void Update()
    {
        if (isActivePlayer)
        {
            GetMovementInput();
            GetPointerInput();
            GetKickInput();
        }
        else
        {
            player.gameObject.GetComponent<AgentMovement>().MoveAgent(Vector3.right * 10);
        }
    }

    private void GetKickInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (player.GetComponent<BallInteraction>().hasPossession)
            {
                holdDownStartTime = Time.time;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (player.GetComponent<BallInteraction>().hasPossession)
            {
                float holdDownTime = Time.time - holdDownStartTime;
                OnKickButtonReleased?.Invoke(CalculateHoldTimeNormalized(holdDownTime));
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (player.GetComponent<BallInteraction>().hasPossession)
            {
                float holdDownTime = Time.time - holdDownStartTime;
                OnKickButtonHeldDown?.Invoke(CalculateHoldTimeNormalized(holdDownTime));
            }
        }
    }

    private float CalculateHoldTimeNormalized(float holdTime)
    {
        float maxForceHoldDownTime = 2f;
        float holdTimeNormalized = Mathf.Clamp01(holdTime / maxForceHoldDownTime);
        return holdTimeNormalized;
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