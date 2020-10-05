public class Cargo
{
	public TrainColor Color { get; }

	public Cargo(TrainColor color, float secondsUntilDespawn)
	{
		Color = color;
	}
}