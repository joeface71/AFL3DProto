using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    [SerializeField] private float jumpSpeed = 8.0f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private float speed = 9.0f;

    private Vector3 moveDirection = Vector3.zero;
    private Ball ball;
    [SerializeField] private Transform ballPossessionTransform;

    float chargeTimer;
    float chargeTimeMax = 3f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        HandleKicking();
    }

    private void HandleMovement()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontal, 0, vertical) * (speed * Time.deltaTime));

        if (characterController.isGrounded)
        {

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            ball = collision.gameObject.GetComponent<Ball>();
            //Debug.Log("possession");
            ball.transform.position = ballPossessionTransform.position;
            ball.transform.SetParent(ballPossessionTransform, true);
            ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void HandleKicking()
    {
        if (Input.GetMouseButton(0))
        {
            chargeTimer += Time.deltaTime;
            Debug.Log(chargeTimer);
            if (chargeTimer >= chargeTimeMax)
            {
                Kick(chargeTimeMax);
                chargeTimer = 0f;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Kick(chargeTimer);
            chargeTimer = 0;
        }

    }

    private void Kick(float kickPower)
    {
        Debug.Log($"kicking with {kickPower} kickPower");
    }


}
