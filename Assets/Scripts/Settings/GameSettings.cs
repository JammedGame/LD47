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

    public Texture GetTexture(TileType tileType, bool enabled = true)
    {
        foreach(var type in IconsForTile)
        {
            if (type.TileType == tileType)
            {
                return !enabled && type.IconDisabled
                    ? type.IconDisabled
                    : type.Icon;
            }
        }

        return null;
    }

    void OnValidate()
    {
        if (SettingsPerType == null)
            SettingsPerType = new List<TileTypeSettings>();

        foreach(var type in System.Enum.GetValues(typeof(TileType)))
        {
            if (type is TileType tileType)
            {
                if (tileType != TileType.Undefined && GetSettings(tileType) == null)
                {
                    SettingsPerType.Add(new TileTypeSettings() {
                        TileType = tileType
                    });
                }

                if (!IconsForTile.Exists(x => x.TileType == tileType))
                {
                    IconsForTile.Add(new TileIconSettings() {
                        TileType = tileType
                    });
                }
            }
        }
    }
}