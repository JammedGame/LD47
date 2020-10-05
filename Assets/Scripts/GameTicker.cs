using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTicker : MonoBehaviour
{
    [SerializeField]
    private LevelData levelData;
    private GameWorld gameWorld;
    private ViewController viewController;
    private InputController inputController;
    private CameraController cameraController;

    public GameWorld GameWorld => gameWorld;

    public void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
        gameWorld = new GameWorld(levelData);
        viewController = new ViewController(gameWorld);
        inputController = new InputController(viewController, Camera.main);
        cameraController.Initialize(levelData);

        var background = FindObjectOfType<BackgroundController>();
        background.Init(levelData);
    }

    void Update()
    {
        if (inputController == null)
            return;

        inputController.ProcessInput();

        // make dTs deterministic
        accumulatedTime += Time.deltaTime;
        while (accumulatedTime >= DeterministicTick)
        {
            gameWorld.Tick(DeterministicTick);
            accumulatedTime -= DeterministicTick;
        }

        viewController.Render();
        cameraController.CameraUpdate();

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("LevelScene");
        }
    }

    float accumulatedTime;
    public const float DeterministicTick = 1f / 60f;
}