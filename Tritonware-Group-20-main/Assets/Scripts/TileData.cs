using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : ScriptableObject
{
    [SerializeField] private bool isFlammable;
    [SerializeField] private bool isOnFire;
    [SerializeField] private GameObject objectOnTile;
    [SerializeField] private float timeTilSpread;
    [SerializeField] private string tileType;

}
