using System.Collections.Generic;
using TMPro;

public class WagonCollectionCounterUI : InGameUIObject
{
	public TrainColor TrainColor;
	TextMeshProUGUI text;

	private List<CargoSpawn> MySpawns;

	public override void OnInitialize(GameWorld gameWorld)
	{
		text = GetComponentInChildren<TextMeshProUGUI>();
		MySpawns = gameWorld.LevelData.CargoSpawns.FindAll(x => x.Color == TrainColor);

		if (MySpawns.Count == 0)
		{
			gameObject.SetActive(false);
			return;
		}

		UpdateText();
	}

	public void UpdateText()
	{
		text.text = $"0/{MySpawns.Count}";
	}
}