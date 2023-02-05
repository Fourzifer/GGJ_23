using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Freya;

public static class SplineExtensions
{
    public static BezierCubic2D[] ToBezier(this Spline spline, Vector2 pos)
    {
        int points = spline.GetPointCount();
        int segments = points;
        if (spline.isOpenEnded) segments -= 1;
        BezierCubic2D[] splines = new BezierCubic2D[segments];

        for (int i = 0; i < segments; i++)
        {
            var next = SplineUtility.NextIndex(i, points);

            var p = pos + (Vector2)spline.GetPosition(i);
            var pn = pos + (Vector2)spline.GetPosition(next);

            var pt = (Vector2)spline.GetRightTangent(i);
            var pnt = (Vector2)spline.GetLeftTangent(next);

            splines[i] = new BezierCubic2D(p, p + pt, pn + pnt, pn);

            BezierCubic2D bez = new BezierCubic2D(p, p + pt, pn + pnt, pn);
        }

        return splines;
    }

    public static float CalcLength(this BezierCubic2D[] spline)
    {
        float length = 0;
        for (int i = 0; i < spline.Length; i++)
        {
            length += spline[i].Curve.GetArcLength();
        }
        return length;
    }

    public static Vector2 PointFromDistance(this BezierCubic2D[] spline, float distance)
    {
        float remainingDistance = distance;
        int i = 0;
        while (remainingDistance > spline[i].Curve.GetArcLength())
        {
            remainingDistance -= spline[i].Curve.GetArcLength();
            i++;
        }

        float t = remainingDistance / spline[i].Curve.GetArcLength();

        return spline[i].Curve.Eval(t);
    }

    public static (BezierCubic2D curve, float t) CurveAndTFromDistance(this BezierCubic2D[] spline, float distance)
    {
        float remainingDistance = distance;
        int i = 0;
        while (remainingDistance > spline[i].Curve.GetArcLength())
        {
            remainingDistance -= spline[i].Curve.GetArcLength();
            i++;
        }

        float t = remainingDistance / spline[i].Curve.GetArcLength();

        return (spline[i], t);
    }
}
