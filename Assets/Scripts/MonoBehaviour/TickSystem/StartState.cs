namespace AutoDefense
{
    public class StartState : TickState
    {
        /// <summary>
        /// Referenz tickmanager
        /// </summary>
        TickManager tickManager;
        /// <summary>
        /// default Values
        /// </summary>
        public StartState(TickManager _TManager)
        {
            tickManager = _TManager;

        }

        public override void EnterState()
        {

        }
        public override void HandleState()
        {
            tickManager.timeSlider.value = 0;
            tickManager.SetState("Edit");
        }
    }
}
