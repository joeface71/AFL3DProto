using UnityEngine;
using UnityEngine.Events;

public class BallInteraction : MonoBehaviour
{
    [field: SerializeField]
    public UnityEvent<int> OnPossessionGained { get; set; }

    [SerializeField] private Transform ballPossessionTransform;
    [SerializeField] private Transform ballPossessionPivotTransform;
    private GameObject ball;
    [SerializeField][Range(1f, 20f)] private float elevationMultiplier = 5f;
    [SerializeField] private Player player;

    public bool hasPossession;

    private Vector3 mouseLookHitPoint;

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            hasPossession = true;
            ball.transform.parent = ballPossessionTransform;
            ball.transform.position = ballPossessionTransform.position;
            ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            ball.transform.eulerAngles = new Vector3(0, 0, 60);
            OnPossessionGained?.Invoke(player.PlayerData.ID);
        }
    }

    public void PointBallAtRayHitPoint(Vector3 hitPoint)
    {
        mouseLookHitPoint = hitPoint;
        if (hasPossession)
        {
            ballPossessionPivotTransform.LookAt(mouseLookHitPoint);
        }
    }

    public void KickBall(float kickButtonHoldDownTimeNormalized)
    {
        if (hasPossession)
        {
            Vector3 elevation = Vector3.up * elevationMultiplier;
            Vector3 direction = (mouseLookHitPoint - transform.position + elevation).normalized;

            ball.transform.parent = null;
            Rigidbody ballRB = ball.GetComponent<Rigidbody>();
            ballRB.constraints = RigidbodyConstraints.None;

            Vector3 kickPointOffset = new Vector3(0, 0.2f, 0);
            ballRB.AddForceAtPosition(direction * kickButtonHoldDownTimeNormalized * player.PlayerData.maxForce, ball.transform.position - kickPointOffset, ForceMode.Impulse);

            hasPossession = false;
        }
    }
}