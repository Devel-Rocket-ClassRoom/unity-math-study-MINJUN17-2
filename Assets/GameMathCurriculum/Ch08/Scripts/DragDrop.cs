using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public Camera camera;
    public LayerMask ground;
    public LayerMask dragObject;
    public LayerMask dropZone;
    private bool isDraging = false;
    private DragObject draggingObject;

    
    private void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, dragObject))
            {
                isDraging = true;
                draggingObject = hitInfo.collider.GetComponent<DragObject>();
                draggingObject.DragStart();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDraging)
            {
                if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, dropZone))
                {
                    draggingObject.DragEnd();
                }
                else
                {
                    draggingObject.Return();
                }
            }
            isDraging = false;
            draggingObject = null;
        }

        if (isDraging)
        {
            
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, ground))
            {
                draggingObject.transform.position = hitInfo.point;
            }
        }
        
    }
}
