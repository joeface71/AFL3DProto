using UnityEngine;

public class BoundsCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ball>() != null)
        {
            collision.gameObject.transform.position = new Vector3(0, 1, 0);
        }

        if (collision.gameObject.GetComponent<Player>() != null)
        {
            Debug.Log("Player collision");
            collision.gameObject.transform.position = Vector3.zero;

        }
    }
}
