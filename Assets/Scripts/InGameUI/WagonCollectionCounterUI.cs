using System.Collections.Generic;
using TMPro;

public class WagonCollectionCounterUI : InGameUIObject
{
	public TrainColor TrainColor;
	TextMeshProUGUI text;
	GameWorld world;

	private List<CargoSpawn> MySpawns;

	public override void OnInitialize(GameWorld gameWorld)
	{
		this.world = gameWorld;
		text = GetComponentInChildren<TextMeshProUGUI>();
		MySpawns = gameWorld.LevelData.CargoSpawns.FindAll(x => x.Color == TrainColor);
		gameWorld.OnScoreUpdated += UpdateText;

		if (MySpawns.Count == 0)
		{
			gameObject.SetActive(false);
			return;
		}

		UpdateText();
	}

	public void UpdateText()
	{
		world.CollectedCarsScore.TryGetValue(TrainColor, out int score);
		text.text = $"{score}/{MySpawns.Count}";
	}
}