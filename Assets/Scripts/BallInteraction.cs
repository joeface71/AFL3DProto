using UnityEngine;

public class BallInteraction : MonoBehaviour
{
    [SerializeField] private Transform ballPossessionTransform;
    [SerializeField] private Transform ballPossessionPivotTransform;
    [SerializeField] private Transform ball;

    private bool hasPossession;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            hasPossession = true;
            ball.parent = ballPossessionTransform;
            ball.position = ballPossessionTransform.position;
            ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void PointBallAtRayHitPoint(Vector3 hitPoint)
    {
        if (hasPossession)
        {
            ballPossessionPivotTransform.LookAt(hitPoint);
        }
    }
}
