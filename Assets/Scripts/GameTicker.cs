using UnityEngine;

public class GameTicker : MonoBehaviour
{
    [SerializeField]
    private LevelData levelData;
    private GameWorld gameWorld;
    private ViewController viewController;
    private CameraController cameraController;

    public GameWorld GameWorld => gameWorld;

    public void Start()
    {
        gameWorld = new GameWorld(levelData);
        viewController = new ViewController(gameWorld);
        cameraController = new CameraController(gameWorld);
    }

    void Update()
    {
        gameWorld.Tick();
        viewController.Render();
    }
}