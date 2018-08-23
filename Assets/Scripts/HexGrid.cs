using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HexGrid : MonoBehaviour {

    public int width = 6;

    public Color defaultColor = Color.white;

    public HexCell cellPrefab;
    public TextMeshProUGUI cellLabelPrefab;

    Canvas gridCanvas;
    EventSystem es;
    List<HexCell> cells;

    private void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        int gridWidth = (width * 2) - 1;
        int rowLength = width;
        int cellCount = 0;
        for(int i = width; i < gridWidth; i++)
            cellCount += i * 2;

        cellCount += gridWidth;
        cells = new List<HexCell>(cellCount);
        int startingQ = (1 - width) + 1;

        for(int j = 0, q = 1 - width, r = width - 1; j < cellCount; r--)
        {
            for(int k = 0; k < rowLength; k++, j++, q++)
                CreateCell(q, r, j);

            if(j < (cellCount / 2))
            {
                q = 1 - width;
                rowLength++;
            }
            else
            {
                rowLength--;
                q = startingQ++;
            }
        }
    }

    private void Start()
    {
        es = EventSystem.current;
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0) && !es.IsPointerOverGameObject())
            HandleInput();
    }

    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(inputRay, out hit))
        {
            HexCell hitCell = hit.transform.parent.GetComponent<HexCell>();
            FlipCell(hitCell);
        }
    }

    public void FlipCell(HexCell cell)
    {
        cell.Flip();
    }

    void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / width) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        HexCell cell = Instantiate(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        cell.coordText.text = cell.coordinates.X + "\n" + cell.coordinates.Z;
        cells.Add(cell);
    }
}