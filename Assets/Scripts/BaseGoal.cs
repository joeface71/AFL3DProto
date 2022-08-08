using UnityEngine;
using UnityEngine.Events;

public abstract class BaseGoal : MonoBehaviour
{
    [field: SerializeField]
    public UnityEvent<int> OnGoalScored { get; set; }
    public abstract void TriggerGoal();

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ball>() != null)
        {
            TriggerGoal();
        }
    }
}
