using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    [SerializeField] private GameObject ballPrefab;

    private GameObject ballInstance;
    // Start is called before the first frame update
    void Start()
    {
        InstantiateBall();
    }

    private void InstantiateBall()
    {
        ballInstance = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
    }

    public void DestroyBallAndReinstantiate(int points)
    {
        Destroy(ballInstance);
        InstantiateBall();
    }
}
