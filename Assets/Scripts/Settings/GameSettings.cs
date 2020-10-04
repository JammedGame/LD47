using System.Collections.Generic;
using UnityEngine;

public class GameSettings : ScriptableObject
{
    [Table]
    public List<TileTypeSettings> SettingsPerType;

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

    public (Texture, Rotation) GetTexture(TileType tileType)
    {
        var (src, rotation) = tileType.GetRotation();
        return (Resources.Load<Texture>($"Textures/{src}"), rotation);
    }

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