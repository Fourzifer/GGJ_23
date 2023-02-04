using UnityEngine;
using Freya;

public class Grow : MonoBehaviour
{
    public float GrowSpeed;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        var scale = transform.localScale;
        scale.y += GrowSpeed * Time.deltaTime;
        scale.y = Mathfs.Clamp01(scale.y);
        transform.localScale = scale;
    }
}
