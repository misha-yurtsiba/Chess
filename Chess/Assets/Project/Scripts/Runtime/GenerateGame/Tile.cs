using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject moveSprite;
    [SerializeField] private GameObject attackSprite;
    [SerializeField] private GameObject selectSprite;
    public int xPos { get; private set; }
    public int zPos { get; private set; }

    public Figure figure;

    public Team team;

    public void Init(int x, int z)
    {
        xPos = x;
        zPos = z;
    }
    public void SelectMarkerSetActive(bool active)
    {
        selectSprite.gameObject.SetActive(active);
    }
    public void MoveMarkerSetActive(bool active)
    {
        moveSprite.gameObject.SetActive(active);
    }
    public void AttackMarkerSetActive(bool active)
    {
        attackSprite.gameObject.SetActive(active);
    }

    public bool HasFigure()
    {
        return figure != null;
    }

}
