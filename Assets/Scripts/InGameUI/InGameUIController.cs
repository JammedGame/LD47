using UnityEngine;

// main controller
public class InGameUIController : MonoBehaviour
{
	GameWorld world;
	InGameUIObject[] allUIControls;

	public void Initialize(GameWorld gameWorld)
	{
		this.world = gameWorld;
		this.allUIControls = GameObject.FindObjectsOfType<InGameUIObject>();

		foreach(var obj in allUIControls)
			obj.OnInitialize(world);
	}

	public void OnUpdate()
	{
	}
}

public abstract class InGameUIObject : MonoBehaviour
{
	public abstract void OnInitialize(GameWorld gameWorld);
}