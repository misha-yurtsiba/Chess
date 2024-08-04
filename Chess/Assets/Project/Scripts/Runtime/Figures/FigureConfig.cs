using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName ="FigureConfig")]
public class FigureConfig : ScriptableObject
{
    public List<FigureData> data = new List<FigureData>(6);
}
