using System.Collections.Generic;
using UnityEngine;

public class GameSettings : ScriptableObject
{
    [Table] public List<TileTypeSettings> SettingsPerType;
    [Table] public List<TrainTypeSettings> SettingsPerTrainType;
    [Table] public List<TrainColorSettings> SettingsPerTrainColor;
    public List<LevelData> AllLevels;

    public TrainColorSettings GetColorSettings(TrainColor trainColor)
    {
        foreach(var type in SettingsPerTrainColor)
        {
            if (type.TrainColor == trainColor)
            {
                return type;
            }
        }

        return default;
    }

    public TileTypeSettings GetSettings(TileType tileType)
    {
        foreach(var type in SettingsPerType)
        {
            if (type.TileType == tileType)
            {
                return type;
            }
        }

        return null;
    }

    public (Texture texture, Rotation rotation1, Texture overlayTexture) GetTexture(TileType tileType)
    {
        if (!cache.TryGetValue((int)tileType, out var result))
        {
            var (src, rotation) = tileType.GetRotation();
            result = (Resources.Load<Texture>($"Textures/{src}"), rotation, Resources.Load<Texture>($"Textures/{src}Overlay"));
        }
        return result;
    }

    Dictionary<int, (Texture texture, Rotation rotation1, Texture overlayTexture)> cache =
        new Dictionary<int, (Texture texture, Rotation rotation1, Texture overlayTexture)>();

    static GameSettings instance;

    public static GameSettings Instance
    {
        get
        {
            if (instance) return instance;
            instance = Resources.Load<GameSettings>("Settings/GameSettings");
            return instance;
        }
    }

    void OnValidate()
    {
        if (SettingsPerType == null)
            SettingsPerType = new List<TileTypeSettings>();

        foreach(var type in System.Enum.GetValues(typeof(TileType)))
        {
            if (type is TileType tileType)
            {
                var (src, rotation) = tileType.GetRotation();

                var settings = GetSettings(tileType);
                if (settings == null)
                {
                    settings = new TileTypeSettings() {
                        TileType = tileType
                    };
                    SettingsPerType.Add(settings);
                }

                if (src != tileType)
                {
                    var srcSettings = GetSettings(src);

                    settings.LeftExit =  srcSettings.GetExitDirection(Direction.Left.RotateCCW(rotation)).Rotate(rotation);
                    settings.RightExit =  srcSettings.GetExitDirection(Direction.Right.RotateCCW(rotation)).Rotate(rotation);
                    settings.TopExit =  srcSettings.GetExitDirection(Direction.Top.RotateCCW(rotation)).Rotate(rotation);
                    settings.BottomExit =  srcSettings.GetExitDirection(Direction.Bottom.RotateCCW(rotation)).Rotate(rotation);
                }
            }

            SettingsPerType.Sort((a, b) => a.TileType.CompareTo(b.TileType));
        }
    }
}