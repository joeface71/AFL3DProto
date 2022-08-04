using UnityEngine;
using UnityEngine.Events;

public class AgentInput : MonoBehaviour
{
    [field: SerializeField]
    public UnityEvent<Vector3> OnMovementKeyPressed { get; set; }

    private void Update()
    {
        GetMovementInput();
    }

    private void GetMovementInput()
    {
        OnMovementKeyPressed?.Invoke(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")));
    }
}
