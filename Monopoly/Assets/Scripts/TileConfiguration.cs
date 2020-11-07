using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Tile Configuration", menuName = "TrueGames/Monopoly Tile Configuration", order = 0)]
public class TileConfiguration : ScriptableObject
{
    public Texture Texture;
    public string Name;
    public int Value;
}
