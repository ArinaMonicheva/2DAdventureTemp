using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// The purpose of this class is SOLELY to create the level layout.
/// </summary>
public class LevelCreator : MonoBehaviour
{

    public LevelCreationData _levelCreationData;
    public Tilemap _tilemapToDrawFloor;
    public Tilemap _tilemapToDrawWalls;
    public TileBase _fillRuleTile;

    public TileBase _fillRuleTileWalls;

    private HashSet<Vector2Int> _dungeonTiles;
    private HashSet<Vector2Int> _dungeonTilesWalls;
    private int[,] _maze; //

    private void Start()
    {
        _dungeonTiles = DrunkWalkerManager.CreateMap(_levelCreationData);
        _dungeonTilesWalls = DrunkWalkerManager.CreateMap(_levelCreationData);
        //////
        _maze = MazeGenerator.combina(); //

        for (int i = 0; i <= _maze.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= _maze.GetUpperBound(1); j++)
            {
                if (_maze[i, j] == 0)
                {
                    Vector2Int newPosition = new Vector2Int(j, i);
                    _dungeonTiles.Add(newPosition);
                }
                else
                {
                    Vector2Int newPosition = new Vector2Int(j, i);
                    _dungeonTilesWalls.Add(newPosition);
                }
            }
        }
        //////
        if (_levelCreationData._tileSize > 1)
        {
            _dungeonTiles = ReturnListOfScaledTiles(_dungeonTiles);
            _dungeonTilesWalls = ReturnListOfScaledTiles(_dungeonTilesWalls);
        }

        DrawDungeonTiles(_dungeonTiles, _tilemapToDrawFloor, _fillRuleTile);
        DrawDungeonTiles(_dungeonTilesWalls, _tilemapToDrawWalls, _fillRuleTileWalls);
    }


    private void DrawDungeonTiles(IEnumerable<Vector2Int> tiles, Tilemap tilemapToUse, TileBase tilesToUse)
    {
        foreach (Vector2Int tileLocation in tiles)
        {
            tilemapToUse.SetTile(new Vector3Int(tileLocation.x, tileLocation.y, 0), tilesToUse);
        }
    }


    private HashSet<Vector2Int> ReturnListOfScaledTiles(IEnumerable<Vector2Int> tiles)
    {
        HashSet<Vector2Int> scaledTiles = new HashSet<Vector2Int>();

        foreach (Vector2Int tileLocation in tiles)
        {
            Vector2Int startPosition = tileLocation * _levelCreationData._tileSize;
            Vector2Int newPosition;

            for (int i = 0; i < _levelCreationData._tileSize; i++)
            {
                for (int j = 0; j < _levelCreationData._tileSize; j++)
                {
                    newPosition = new Vector2Int(i, j) + new Vector2Int(startPosition.x, startPosition.y);
                    scaledTiles.Add(newPosition);
                }
            }
        }

        return scaledTiles;
    }
}



























