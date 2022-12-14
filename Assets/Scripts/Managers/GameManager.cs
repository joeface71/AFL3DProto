using Cinemachine;
using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private GameObject ballInstance;
    private Rigidbody rb;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        InstantiateBall();
    }

    private void InstantiateBall()
    {
        ballInstance = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
        rb = ballInstance.GetComponent<Rigidbody>();
        virtualCamera.Follow = ballInstance.transform;
    }

    public void MoveBallBackToCenterCircle(int points)
    {
        ballInstance.transform.position = new Vector3(0, 10, 0);
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.rotation = Quaternion.Euler(0, 0, 0);
        rb.constraints = RigidbodyConstraints.None;
        rb.velocity = Vector3.zero;
    }
}
