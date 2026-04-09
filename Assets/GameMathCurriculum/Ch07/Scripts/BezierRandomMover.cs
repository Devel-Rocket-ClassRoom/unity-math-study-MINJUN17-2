using UnityEngine;

public class BezierRandomMover : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public int spawnCount = 5;
    public float randomRange = 5f;

    public float minDuration = 1f;
    public float maxDuration = 4f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < spawnCount; i++)
            {
                SpawnSphere();
            }
        }
    }

    private void SpawnSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = startPoint.position;
        sphere.transform.localScale = Vector3.one * 0.3f;

        Vector3 p0 = startPoint.position;
        Vector3 p3 = endPoint.position;
        Vector3 p1 = p0 + Random.insideUnitSphere * randomRange;
        Vector3 p2 = p3 + Random.insideUnitSphere * randomRange;

        float duration = Random.Range(minDuration, maxDuration);

        BezierMoveAgent agent = sphere.AddComponent<BezierMoveAgent>();
        agent.Init(p0, p1, p2, p3, duration);
    }
}