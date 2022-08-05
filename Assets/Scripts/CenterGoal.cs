public class CenterGoal : BaseGoal
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void TriggerGoal()
    {
        OnGoalScored?.Invoke(6);
    }
}
