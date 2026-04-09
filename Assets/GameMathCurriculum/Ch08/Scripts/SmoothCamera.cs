using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 2f, -3f);
    public float positionSmoothTime = 0.3f;
    public float rotationSmoothSpeed = 5f;
    private Vector3 positionVelocity = Vector3.zero;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 worldOffset = target.rotation * offset;
        Vector3 targetPos = target.position + worldOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref positionVelocity, positionSmoothTime);
        Vector3 look = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(look);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothSpeed * Time.deltaTime);
    }
}
