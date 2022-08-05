using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AgentMovement : MonoBehaviour
{
    [SerializeField] private GameObject ball;

    protected Rigidbody rb;

    [field: SerializeField]
    public MovementDataSO MovementData { get; set; }

    [SerializeField] protected float currentVelocity = 3f;
    protected Vector3 movementDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void MoveAgent(Vector3 movementInput)
    {
        if (movementInput.magnitude > 0)
        {
            movementDirection = movementInput;
            RotateAgent(movementDirection);
        }

        currentVelocity = CalculateSpeed(movementInput);
    }

    protected void RotateAgent(Vector3 movementDirection)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection), 0.15f);
    }

    private float CalculateSpeed(Vector3 movementInput)
    {
        if (movementInput.magnitude > 0)
        {
            currentVelocity += MovementData.acceleration * Time.deltaTime;
        }
        else
        {
            currentVelocity -= MovementData.deacceleration * Time.deltaTime;
        }
        return Mathf.Clamp(currentVelocity, 0f, MovementData.maxSpeed);
    }


    private void FixedUpdate()
    {
        rb.velocity = currentVelocity * movementDirection.normalized;
    }



}
