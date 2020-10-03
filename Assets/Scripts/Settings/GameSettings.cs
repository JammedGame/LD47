using System.Collections.Generic;
using UnityEngine;

public class GameSettings : ScriptableObject
{
    [Table]
    public List<TileTypeSettings> SettingsPerType;

    [Table]
    public List<TileIconSettings> IconsForTile;

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

    public (Texture, Rotation) GetTexture(TileType tileType, bool enabled = true)
    {
        foreach(var type in IconsForTile)
        {
            if (type.TileType == tileType)
            {
                return !enabled && type.IconDisabled
                    ? (type.IconDisabled, type.Rotation)
                    : (type.Icon, type.Rotation);
            }
        }

        return default;
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

                var iconsSettings = IconsForTile.Find(x => x.TileType == tileType);
                if (iconsSettings == null)
                {
                    iconsSettings = new TileIconSettings() {
                        TileType = tileType
                    };
                    IconsForTile.Add(iconsSettings);
                }

                if (src != tileType)
                {
                    iconsSettings.Icon = GetTexture(src, true).Item1;
                    iconsSettings.IconDisabled = GetTexture(src, false).Item1;
                    iconsSettings.Rotation = rotation;
                    
                    var srcSettings = GetSettings(src);
                }
            }
        }
    }
}