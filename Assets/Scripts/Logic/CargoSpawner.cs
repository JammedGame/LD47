using System;
using System.Collections.Generic;

public class CargoSpawner
{
	public readonly GameWorld World;
	public readonly Tile Tile;
	public readonly List<CargoSpawn> CargoToBeSpawned = new List<CargoSpawn>();
	public readonly List<Cargo> Cargos = new List<Cargo>();

	public event Action OnUpdate;

	public CargoSpawner(Tile tile)
	{
		this.Tile = tile;
		this.World = tile.World;
		this.World.RegisterSpawner(this);
	}

	public void AddSpawn(CargoSpawn spawn)
	{
		CargoToBeSpawned.Add(spawn);
		OnUpdate?.Invoke();
	}

	public void Tick(float dT)
	{
		// spawn cargos in time
		for (var i = CargoToBeSpawned.Count - 1; i >= 0; i--)
		{
			var cargoSpawn = CargoToBeSpawned[i];
			if (World.SecondsElapsed >= cargoSpawn.SpawnsAtSeconds)
			{
				Cargos.Add(new Cargo(cargoSpawn.Color, cargoSpawn.DespawnsAfterSeconds));
				CargoToBeSpawned.RemoveAt(i);
				OnUpdate?.Invoke();
			}
		}
	}

	/// <summary>
	/// Removes all cargos for color and returns how much train should get.
	/// </summary>
	public int RemoveAllFor(TrainColor color)
	{
		int count = 0;

		for (int i = 0; i < Cargos.Count; i++)
		{
			Cargo cargo = Cargos[i];
			if (cargo.Color == color)
			{
				Cargos.RemoveAt(i--);
				count++;
			}
		}

		if (count > 0)
		{
			OnUpdate?.Invoke();
			SoundManager.Instance.PlaySoundCargoPickUp();
		}

		return count;
	}
}