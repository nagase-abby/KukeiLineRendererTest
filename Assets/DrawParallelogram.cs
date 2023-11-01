// リファレンス
// 計算の公式 https://unitylab.wiki.fc2.com/wiki/%E4%BA%8C%E7%B7%9A%E3%81%AE%E4%BA%A4%E5%B7%AE%E3%81%99%E3%82%8B%E7%82%B9%E3%82%92%E6%A4%9C%E5%87%BA%282D%29
// 数式の解読 https://text.tomo.school/two-lines-intersection/

using UnityEngine;

public enum Direction
{
    Right,

    Left,
}

public class DrawParallelogram : MonoBehaviour
{
    [SerializeField]
    private LineRenderer line = null;

    [SerializeField]
    private LineRenderer offsetLine = null;

    private void Start()
    {
        line.positionCount = 5;
        line.loop = false;
        for (int i = 0; i < line.positionCount; i++)
        {
            line.SetPosition(i, Vector3.zero);
        }

        offsetLine.positionCount = 3;
        offsetLine.loop = false;
        for (int i = 0; i < offsetLine.positionCount; i++)
        {
            offsetLine.SetPosition(i, Vector3.zero);
        }
    }

    /// <summary>
    /// マウスクリック開始地点
    /// </summary>
    private Vector2 origin = Vector2.zero;

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0;
        // 終点
        Vector2 mouseOrigin = Camera.main.ScreenToWorldPoint(mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            // 始点
            origin = mouseOrigin;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 originPointA = GetAngle1(origin, Direction.Right);
            Vector2 originPointB = GetAngle2(origin, Direction.Right);

            Vector2 mousePointA = GetAngle1(mouseOrigin, Direction.Left);
            Vector2 mousePointB = GetAngle2(mouseOrigin, Direction.Left);

            // 交点の座標
            Vector2 line1A2A = ClacPointIntersect(origin, originPointA, mouseOrigin, mousePointA);
            Vector2 line1B2B = ClacPointIntersect(origin, originPointB, mouseOrigin, mousePointB);

            line.SetPositions(new Vector3[]
            {
                origin,
                line1A2A,
                mouseOrigin,
                line1B2B,
                origin
            });

            offsetLine.SetPositions(new Vector3[]
            {
                line1A2A,origin,line1B2B
            });
        }
    }

    private Vector2 GetAngle1(Vector2 origin, Direction direction)
    {
        // 指定の角度からなる座標を作成
        float y = origin.y + Mathf.Sin(GetAngle1(direction) * Mathf.Deg2Rad);
        float x = origin.x + Mathf.Cos(GetAngle1(direction) * Mathf.Deg2Rad);
        return new Vector2(x, y);
    }

    private Vector2 GetAngle2(Vector2 origin, Direction direction)
    {
        float y = origin.y + Mathf.Sin(GetAngle2(direction) * Mathf.Deg2Rad);
        float x = origin.x + Mathf.Cos(GetAngle2(direction) * Mathf.Deg2Rad);
        return new Vector2(x, y);
    }

    private float GetAngle1(Direction direction)
    {
        return direction == Direction.Right ? 25 : 155;
    }

    private float GetAngle2(Direction direction)
    {
        return direction == Direction.Right ? 335 : 205;
    }

    /// <summary>
    /// ２つの直線から交点を求める
    /// </summary>
    private Vector2 ClacPointIntersect(Vector2 origin1, Vector2 direction1, Vector2 origin2, Vector2 direction2)
    {
        Vector2 intersect = Vector2.zero;

        // 各直線の傾きを計算
        Vector2 m1xy = origin1 - direction1;
        // 傾きの公式 m = y /x
        float m1 = m1xy.y / m1xy.x;

        Vector2 m2xy = origin2 - direction2;
        float m2 = m2xy.y / m2xy.x;

        // 2直線の交点 == 連立方程式の解から
        // 交点を求めるために２点間の座標から方程式を解きxとyを算出する

        // 一次関数の公式 y = ax + b
        // 公式 y = m1x + n と y = m2x + nから連立方程式を解く
        intersect.x = (m1 * origin1.x - m2 * origin2.x + origin2.y - origin1.y) / (m1 - m2);
        intersect.y = m1 * (intersect.x - origin1.x) + origin1.y;

        return intersect;
    }
}
