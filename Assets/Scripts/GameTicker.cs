using UnityEngine;

public class GameTicker : MonoBehaviour
{
    [SerializeField]
    private LevelData levelData;
    private GameWorld gameWorld;
    private ViewController viewController;
    private InputController inputController;

    public GameWorld GameWorld => gameWorld;

    public void Start()
    {
        gameWorld = new GameWorld(levelData);
        viewController = new ViewController(gameWorld);
        inputController = new InputController(viewController, Camera.main);
    }

    void Update()
    {
        inputController.ProcessInput();
        gameWorld.Tick(1f / 60f);
        viewController.Render();
    }
}