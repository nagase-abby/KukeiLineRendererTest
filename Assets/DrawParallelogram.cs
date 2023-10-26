// リファレンス
// 計算の公式 https://unitylab.wiki.fc2.com/wiki/%E4%BA%8C%E7%B7%9A%E3%81%AE%E4%BA%A4%E5%B7%AE%E3%81%99%E3%82%8B%E7%82%B9%E3%82%92%E6%A4%9C%E5%87%BA%282D%29
// 数式の解読 https://text.tomo.school/two-lines-intersection/

using UnityEngine;

public class DrawParallelogram : MonoBehaviour
{
    [SerializeField]
    private RectangleLineRenderer line1 = null;

    [SerializeField]
    private RectangleLineRenderer line2 = null;

    [SerializeField]
    private LineRenderer line = null;

    private void Start()
    {
        line.positionCount = 5;
        line.loop = true;
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0;
        Vector2 vertex = Camera.main.ScreenToWorldPoint(mousePosition);

        line1.OnUpdate(vertex);
        line2.OnUpdate(vertex);

        // マウス座標の始点と終点
        line.SetPosition(0, line1.GetFirstVertex);
        line.SetPosition(4, line1.GetFirstVertex);
        line.SetPosition(2, line2.GetFirstVertex);

        // 交点の座標
        Vector2 line1A2A = ClacPointIntersect(line1.GetFirstVertex, line1.PointA, line2.GetFirstVertex, line2.PointA);
        Vector2 line1B2B = ClacPointIntersect(line1.GetFirstVertex, line1.PointB, line2.GetFirstVertex, line2.PointB);
        line.SetPosition(1, line1A2A);
        line.SetPosition(3, line1B2B);
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
