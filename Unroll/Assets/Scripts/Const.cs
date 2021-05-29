using System.Collections.Generic;
using UnityEngine;
public static class Const
{
    public static readonly string[] LEVEL_NAMES = new string[]
    {
        "Easy-1",
        "Easy-2",
        "Easy-3",
        "Intermediate-1"
    };

    public static readonly Collectible.Metal[] COLLECTIBLE_METALS = new Collectible.Metal[]{
        Collectible.Metal.Copper,
        Collectible.Metal.Silver,
        Collectible.Metal.Gold
    };
}
