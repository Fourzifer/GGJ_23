using Freya;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using Random = Freya.Random;

public class Vines : MonoBehaviour
{
    struct SpawnPosition
    {
        // FIXME: Some way to go back to the spline or sprite shape?
        public Vector3 Position;
        public float Angle;

        public SpawnPosition(Vector3 position, float angle)
        {
            Position = position;
            Angle = angle;
        }
        public override string ToString()
        {
            return $"{Position} {Angle}";
        }
    }

    public float StartGrowPosition;
    public float EndGrowPosition;

    public float VineGrowPosition;
    public float VineGrowSpeed;

    private int SpawnPointIndex = 0;

    /// <summary>
    /// Approximate vines per unit.
    /// </summary>
    public float VineDensity = 0.8f;

    private List<BezierCubic2D[]> GroundSplines = new List<BezierCubic2D[]>();

    public List<GameObject> VinePrefabs;

    private List<SpawnPosition> spawnPositions = new List<SpawnPosition>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Ground");
        foreach (var obj in objects)
        {
            SpriteShapeController shape = obj.GetComponent<SpriteShapeController>();
            if (shape != null)
            {
                GroundSplines.Add(shape.spline.ToBezier(shape.transform.position));
            }
            else
            {
                Debug.LogWarning($"Object {obj} had tag 'Ground' but no SplineShape component.");
            }
        }

        SparseSequence seq = new SparseSequence(Random.Value);

        // Now we generate spawn points
        foreach (var spline in GroundSplines)
        {
            float splineLength = spline.CalcLength();

            int vines = (int)(splineLength * VineDensity);

            for (int i = 0; i < vines; i++)
            {
                float dist = Random.Value * splineLength;
                dist = seq.Next() * splineLength;

                var (curve, t) = spline.CurveAndTFromDistance(dist);

                var position = curve.Curve.Eval(t);
                var tangent = curve.Curve.EvalTangent(t);
                var angle = Vector2.Angle(Vector2.up, tangent.Rotate90CCW());

                if (position.x < StartGrowPosition) continue;

                spawnPositions.Add(new SpawnPosition(position, angle));
            }
        }

        spawnPositions.Sort((a, b) => Mathfs.SignWithZeroAsInt(a.Position.x - b.Position.x));
    }

    // Update is called once per frame
    void Update()
    {
        VineGrowPosition += VineGrowSpeed * Time.deltaTime;
        VineGrowPosition = Mathfs.Clamp(VineGrowPosition, StartGrowPosition, EndGrowPosition);

        while (SpawnPointIndex < spawnPositions.Count &&
            VineGrowPosition > spawnPositions[SpawnPointIndex].Position.x)
        {
            var position = spawnPositions[SpawnPointIndex].Position;
            var angle = spawnPositions[SpawnPointIndex].Angle;

            GameObject prefab = VinePrefabs[Random.Range(0, VinePrefabs.Count)];

            Instantiate(prefab, position, Quaternion.Euler(0, 0, angle), transform);

            SpawnPointIndex++;
        }
    }

    private void OnDrawGizmos()
    {
        const float Height = 2;
        const float HalfHeight = Height / 2f;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(StartGrowPosition, -HalfHeight, 0), new Vector3(StartGrowPosition, HalfHeight, 0));
        Gizmos.DrawLine(new Vector3(EndGrowPosition, -HalfHeight, 0), new Vector3(EndGrowPosition, HalfHeight, 0));

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(VineGrowPosition, -HalfHeight, 0), new Vector3(VineGrowPosition, HalfHeight, 0));
    }

    private void OnDrawGizmosSelected()
    {
        /*
        Gizmos.color = Color.magenta;
        foreach (var spawn in spawnPositions)
        {
            Gizmos.DrawLine(spawn.Position, spawn.Position + (Vector3)Vector2.up.Rotate(-spawn.Angle * Mathfs.Deg2Rad));
        }

        foreach (var spline in GroundSplines)
        {
            foreach (var curve in spline)
            {
                const int N = 40;
                for (int i = 0; i < N; i++)
                {
                    int n = i + 1;
                    var p = curve.Curve.Eval(i / (float)N);
                    var pn = curve.Curve.Eval(n / (float)N);

                    var pt = curve.Curve.EvalTangent(i / (float)N);
                    var pnormal = pt.Rotate90CCW();

                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(p, p + pnormal);

                    Gizmos.color = Color.black;
                    Gizmos.DrawLine(p, pn);
                }
            }
        }
        */
    }
}
