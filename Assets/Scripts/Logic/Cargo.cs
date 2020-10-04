public class Cargo
{
	public Cargo(TrainColor color, float secondsUntilDespawn)
	{
		Color = color;
		Despawns = secondsUntilDespawn == 0f;
		SecondsUntilDespawn = secondsUntilDespawn;
	}

	public TrainColor Color { get; }
	public bool Despawns { get; }
	public float SecondsUntilDespawn { get; private set; }

	public void Tick(float dT)
	{
		if (Despawns) SecondsUntilDespawn -= dT;
	}
}