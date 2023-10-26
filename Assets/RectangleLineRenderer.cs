using UnityEngine;

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

    public Vector2 GetFirstVertex => line.GetPosition(0);

    /// <summary>
    /// 原点から引かれる線分の座標A
    /// </summary>
    public Vector2 PointA => line.GetPosition(1);

    /// <summary>
    /// 原点から引かれる線分の座標B
    /// </summary>
    public Vector2 PointB => line.GetPosition(3);

    private void Start()
    {
        line.positionCount = 5;
        line.loop = true;
        for (int i = 0; i < line.positionCount; i++)
        {
            // line.SetPosition(i, Vector3.zero);
        }
    }

    public void OnUpdate(Vector2 mouseVertex)
    {
        if (isMouse)
        {
            line.SetPosition(0, mouseVertex);
            line.SetPosition(2, mouseVertex);
            line.SetPosition(4, mouseVertex);
        }

        Test();
    }

    private void Test()
    {
        // 指定の角度からなる座標を作成
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
