using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cell : MonoBehaviour
{
    public static (float width, float height) CornerSize = (2f, 2f);
    public static (float width, float height) SideSize = (1.2f, 2f);

    [SerializeField] private float cellWidth;
    [SerializeField] private float cellHeight;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text tValue;
    [SerializeField] private int iValue;

    public float CellWidth { get { return cellWidth; } }
    public float CellHeight { get { return cellHeight; } }

    public void SetConfiguration(TileConfiguration configuration)
    {
        _renderer.material.mainTexture = configuration.Texture;
        _name.text = configuration.Name;
        tValue.text = configuration.Value.ToString();
    }

    void Awake()
    {
        var scale = transform.GetChild(0).lossyScale;
        cellWidth = scale.z;
        cellHeight = scale.x;
    }
}
