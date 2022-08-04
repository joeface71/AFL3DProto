using UnityEngine;

[CreateAssetMenu(menuName = "Agent/MovementData")]
public class MovementDataSO : ScriptableObject
{
    [Range(0f, 10f)]
    public float maxSpeed = 5f;


    [Range(0.1f, 100f)]
    public float acceleration = 10f, deacceleration = 10f;
}
