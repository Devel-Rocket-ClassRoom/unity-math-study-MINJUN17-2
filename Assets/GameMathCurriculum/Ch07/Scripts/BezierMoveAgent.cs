using UnityEngine;

public class BezierMoveAgent : MonoBehaviour
{
    private Vector3 p0, p1, p2, p3;
    private float duration;
    private float t;

    public void Init(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float duration)
    {
        this.p0 = p0;
        this.p1 = p1;
        this.p2 = p2;
        this.p3 = p3;
        this.duration = duration;
        this.t = 0f;
    }

    private void Update()
    {
        t += Time.deltaTime / duration;

        if (t >= 1f)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = CubicBezier(p0, p1, p2, p3, t);
    }

    private Vector3 CubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return (1 - t) * (1 - t) * (1 - t) * p0 + 3f * (1 - t) * (1 - t) * t * p1 + 3f * (1 - t) * t * t * p2 + t * t * t * p3;
    }
}
