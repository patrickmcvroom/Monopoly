using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public static (float width, float height) CornerSize = (2f, 2f);
    public static (float width, float height) SideSize = (1.2f, 2f);

    [SerializeField] private float cellWidth;
    [SerializeField] private float cellHeight;

    public float CellWidth { get { return cellWidth; } }
    public float CellHeight { get { return cellHeight; } }

    void Awake()
    {
        var scale = transform.GetChild(0).lossyScale;
        cellWidth = scale.z;
        cellHeight = scale.x;
    }
}
