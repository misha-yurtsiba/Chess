using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject moveSprite;
    [SerializeField] private GameObject attackSprite;
    public int xPos { get; private set; }
    public int zPos { get; private set; }

    public Figure figure;

    public Team team;

    private void OnMouseUpAsButton()
    {
        moveSprite.SetActive(true);
    }

}
