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
                }
            }

            SettingsPerType.Sort((a, b) => a.TileType.CompareTo(b.TileType));
        }
    }
}