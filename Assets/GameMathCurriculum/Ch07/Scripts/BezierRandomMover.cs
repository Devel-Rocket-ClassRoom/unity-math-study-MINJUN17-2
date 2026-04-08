using UnityEngine;

public class BezierRandomMover : MonoBehaviour
{
    [Header("=== 시작/끝 지점 ===")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    [Header("=== 생성 설정 ===")]
    [SerializeField] private int spawnCount = 5;
    [SerializeField] private float randomRange = 5f;

    [Header("=== 이동 시간 범위 ===")]
    [SerializeField] private float minDuration = 1f;
    [SerializeField] private float maxDuration = 4f;

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
        sphere.transform.localScale = Vector3.one * 0.1f;

        TrailRenderer trail = sphere.AddComponent<TrailRenderer>();
        trail.time = 0.2f;
        trail.startWidth = 0.05f;
        trail.endWidth = 0f;
        trail.material = new Material(Shader.Find("Sprites/Default"));
        trail.startColor = new Color(1f, 0.3f, 1f); // 보라색
        trail.endColor = new Color(1f, 0.3f, 1f, 0f);


        Vector3 p0 = startPoint.position;
        Vector3 p3 = endPoint.position;

        Vector3 spreadDir = Random.onUnitSphere;
        spreadDir.y = Mathf.Abs(spreadDir.y); // 위쪽으로만
        Vector3 p1 = p0 + spreadDir * randomRange;

        // p2는 끝점 가까이 (수렴하도록)
        Vector3 p2 = Vector3.Lerp(p1, p3, 0.7f);

        float duration = Random.Range(minDuration, maxDuration);

        BezierMoveAgent agent = sphere.AddComponent<BezierMoveAgent>();
        agent.Init(p0, p1, p2, p3, duration);
    }
}
