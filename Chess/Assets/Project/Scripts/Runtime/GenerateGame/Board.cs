using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public const int X_COUNT = 8;
    public const int Y_COUNT = 8;

    [field: SerializeField] public float tileOffset { get; private set; }

    public Tile[,] board = new Tile[X_COUNT, Y_COUNT];

}
