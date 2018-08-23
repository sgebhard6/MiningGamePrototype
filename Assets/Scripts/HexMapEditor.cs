using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HexMapEditor : MonoBehaviour {

    public Color[] colors;
    public HexGrid hexGrid;
    private Color activeColor;
    private EventSystem es;

    private void Awake()
    {
        SelectColor(0);
    }

    public void SelectColor(int index)
    {
        activeColor = colors[index];
    }
}
