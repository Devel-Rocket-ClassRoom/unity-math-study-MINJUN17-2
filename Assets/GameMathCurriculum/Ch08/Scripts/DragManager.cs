using UnityEngine;

public class DragManager : MonoBehaviour
{
    private float maxDistance = 1000f;
    public float returnSpeed = 10f;
    private GameObject target;
    public LayerMask floorZoneMask;
    public LayerMask dropZoneMask;
    public Vector3 startPos;
    private bool isReturn;
    private Terrain terrain;

    private void Awake()
    {
        terrain = Terrain.activeTerrain;
    }

    private void Start()
    {
        startPos.y = terrain.SampleHeight(startPos);
    }


    private void Update()
    {
        if (isReturn)
        {
            Vector3 newPos = Vector3.Lerp(target.transform.position, startPos, returnSpeed * Time.deltaTime);
            newPos.y = terrain.SampleHeight(newPos);
            target.transform.position = newPos;
            if(Vector3.Distance(target.transform.position, startPos) < 0.1f)
            {
                isReturn = false;
                target.transform.position = startPos;
                target = null;
            }
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Target"))
                {
                    target = hit.collider.gameObject;
                    startPos = target.transform.position;
                    startPos.y = terrain.SampleHeight(startPos);
                }
            }
        }

        if (Input.GetMouseButton(0) && target != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, floorZoneMask))
            {
                Vector3 newPos = hit.point;
                target.transform.position = newPos;
            }
        }
        if (Input.GetMouseButtonUp(0) && target != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit, maxDistance, dropZoneMask))
            {
                isReturn = true;
            }
        }
        
    }
}
