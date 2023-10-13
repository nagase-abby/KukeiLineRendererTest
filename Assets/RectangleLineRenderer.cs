using UnityEngine;

public class RectangleLineRenderer : MonoBehaviour
{
    [SerializeField]
    private LineRenderer line = null;

    private Vector2 GetFirstVertex => line.GetPosition(0);

    private void Start()
    {
        line.positionCount = 4;
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);
        line.SetPosition(2, Vector3.zero);
        line.SetPosition(3, Vector3.zero);
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10;
        Vector2 vertex = Camera.main.ScreenToWorldPoint(mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            SetAllPosition(vertex);
        }

        if (Input.GetMouseButton(0))
        {
            line.SetPosition(1, new Vector2(vertex.x, GetFirstVertex.y));
            line.SetPosition(2, vertex);
            line.SetPosition(3, new Vector2(GetFirstVertex.x, vertex.y));
        }
    }

    private void SetAllPosition(Vector2 vertex)
    {
        for (int i = 0; i < line.positionCount; i++)
        {
            line.SetPosition(i, vertex);
        }
    }
}
