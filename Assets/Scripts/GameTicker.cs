using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Game
{
    public static LevelData LevelBeingLoaded;

    public static void LoadLevel(LevelData levelData)
    {
        LevelBeingLoaded = levelData;
        SceneManager.LoadScene("LevelScene");
    }

	public static void LoadNextLevel(LevelData levelData)
	{
        var allLevels = GameSettings.Instance.AllLevels;
        var nextIndex = allLevels.IndexOf(levelData) + 1;
        var level = nextIndex < allLevels.Count ? allLevels[nextIndex] : allLevels[0];
        LoadLevel(level);
	}
}

public class GameTicker : MonoBehaviour
{
    [SerializeField]
    public LevelData levelData = null;
    [SerializeField]
    private InGameUIController uiController = null;

    private GameWorld gameWorld;
    private ViewController viewController;
    private InputController inputController;
    private CameraController cameraController;

    public GameWorld GameWorld => gameWorld;

    public void Start()
    {
        // load from command if needed.
        if (Game.LevelBeingLoaded != null)
        {
            levelData = Game.LevelBeingLoaded;
            Game.LevelBeingLoaded = null;
        }

        cameraController = FindObjectOfType<CameraController>();
        gameWorld = new GameWorld(levelData);
        viewController = new ViewController(gameWorld);
        inputController = new InputController(viewController, Camera.main);
        cameraController.Initialize(levelData);

        var background = FindObjectOfType<BackgroundController>();
        background.Init(levelData);
        uiController.Initialize(gameWorld);
    }

    void Update()
    {
        if (inputController == null)
            return;


        if (!gameWorld.IsPaused)
        {
            inputController.ProcessInput();

            // make dTs deterministic
            accumulatedTime += Time.deltaTime;
            while (accumulatedTime >= DeterministicTick)
            {
                gameWorld.Tick(DeterministicTick);
                accumulatedTime -= DeterministicTick;
            }
        }

        // update view stuff.
        viewController.Render();
        uiController.OnUpdate();
        cameraController.CameraUpdate();

        // restars level.
        if (Input.GetKey(KeyCode.R))
        {
            Game.LoadLevel(levelData);
        }

        if (Input.GetKey(KeyCode.F12))
        {
            Game.LoadNextLevel(levelData);
        }
    }

    float accumulatedTime;
    public const float DeterministicTick = 1f / 240f;
}