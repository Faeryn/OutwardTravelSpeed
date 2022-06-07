namespace TravelSpeed.Extensions {
	public class StatusEffectExt {
		private bool timerSuspended = false;

		public bool TimerSuspended {
			get => timerSuspended;
			set => timerSuspended = value;
		}
	}
}