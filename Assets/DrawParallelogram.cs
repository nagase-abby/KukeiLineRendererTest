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
        Vector2 line1A2A = LineIntersect(line1.GetFirstVertex, line1.PointA, line2.GetFirstVertex, line2.PointA);
        Vector2 line1B2B = LineIntersect(line1.GetFirstVertex, line1.PointB, line2.GetFirstVertex, line2.PointB);
        line.SetPosition(1, line1A2A);
        line.SetPosition(3, line1B2B);
    }

    private Vector2 LineIntersect(Vector2 origin1, Vector2 direction1, Vector2 origin2, Vector2 direction2)
    {
        Vector2 intersect = Vector2.zero;

        Vector2 slopeV1 = origin1 - direction1;
        float slopeF1 = slopeV1.y / slopeV1.x;

        Vector2 slopeV2 = origin2 - direction2;
        float slopeF2 = slopeV2.y / slopeV2.x;

        intersect.x = (slopeF1 * origin1.x - slopeF2 * origin2.x + origin2.y - origin1.y) / (slopeF1 - slopeF2);
        intersect.y = slopeF1 * (intersect.x - origin1.x) + origin1.y;

        return intersect;
    }
}
