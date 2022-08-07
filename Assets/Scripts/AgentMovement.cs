using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AgentMovement : MonoBehaviour
{
    [SerializeField] private GameObject ball;

    protected Player player;

    protected Rigidbody rb;

    //[field: SerializeField]
    //public PlayerDataSO PlayerData { get; set; }

    [SerializeField] protected float currentVelocity = 3f;
    protected Vector3 movementDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        player = GetComponent<Player>();
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
            currentVelocity += player.PlayerData.acceleration * Time.deltaTime;
        }
        else
        {
            currentVelocity -= player.PlayerData.deacceleration * Time.deltaTime;
        }
        return Mathf.Clamp(currentVelocity, 0f, player.PlayerData.maxSpeed);
    }


    private void FixedUpdate()
    {
        rb.velocity = currentVelocity * movementDirection.normalized;
    }



}
