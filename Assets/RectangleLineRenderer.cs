using UnityEngine;

public enum Direction
{
    Right,

    Left,
}

public class RectangleLineRenderer : MonoBehaviour
{
    [SerializeField]
    private LineRenderer line = null;

    [SerializeField]
    private Direction direction;

    [SerializeField, Range(0, 10)]
    private float distance = 1.41421356237f;

    [SerializeField]
    private bool isMouse = false;

    [SerializeField]
    private bool isUpdate = false;

    private Vector2 GetFirstVertex => line.GetPosition(0);

    private void Start()
    {
        line.positionCount = 5;
        line.loop = true;
        for (int i = 0; i < line.positionCount; i++)
        {
            // line.SetPosition(i, Vector3.zero);
        }
    }

    private void Update()
    {
        if (!isUpdate)
            return;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0;
        Vector2 vertex = Camera.main.ScreenToWorldPoint(mousePosition);

        if (isMouse)
        {
            line.SetPosition(0, vertex);
            line.SetPosition(2, vertex);
            line.SetPosition(4, vertex);
        }

        Test();
    }

    private void Test()
    {
        float y1 = GetFirstVertex.y + Mathf.Sin(GetAngle1(direction) * Mathf.Deg2Rad) * distance;
        float x1 = GetFirstVertex.x + Mathf.Cos(GetAngle1(direction) * Mathf.Deg2Rad) * distance;
        line.SetPosition(1, new Vector2(x1, y1));

        float y3 = GetFirstVertex.y + Mathf.Sin(GetAngle3(direction) * Mathf.Deg2Rad) * distance;
        float x3 = GetFirstVertex.x + Mathf.Cos(GetAngle3(direction) * Mathf.Deg2Rad) * distance;
        line.SetPosition(3, new Vector2(x3, y3));
    }

    private float GetAngle1(Direction direction)
    {
        return direction == Direction.Right ? 25 : 155;
    }

    private float GetAngle3(Direction direction)
    {
        return direction == Direction.Right ? 335 : 205;
    }
}
