using UnityEngine;
using Freya;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class Grow : MonoBehaviour
{
    public float GrowSpeed;
    public float SplineWidth = 0.2f;

    private SpriteShapeController[] Splines;

    struct SplineGrowData
    {
        public float T;
        public int Index;
    }

    private SplineGrowData[] GrowData;

    // Start is called before the first frame update
    void Start()
    {
        Splines = GetComponentsInChildren<SpriteShapeController>();
        GrowData = new SplineGrowData[Splines.Length];
        foreach (var controller in Splines)
        {
            controller.autoUpdateCollider = false;

            var spline = controller.spline;
            int points = spline.GetPointCount();
            for (int i = 0; i < points; i++)
            {
                spline.SetHeight(i, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Splines.Length; i++)
        {
            if (GrowData[i].Index < Splines[i].spline.GetPointCount())
            {
                GrowData[i].T += GrowSpeed * Time.deltaTime;
                if (GrowData[i].T > 1) GrowData[i].T = 1;
                Splines[i].spline.SetHeight(GrowData[i].Index, GrowData[i].T * SplineWidth);
                if (GrowData[i].T == 1)
                {
                    GrowData[i].T = 0;
                    GrowData[i].Index++;
                }
            }
        }
    }
}
