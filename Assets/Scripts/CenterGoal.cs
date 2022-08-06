public class CenterGoal : BaseGoal
{

    public override void TriggerGoal()
    {
        OnGoalScored?.Invoke(6);
    }
}
