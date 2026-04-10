using UnityEngine;

public class OffscreenIndicator : MonoBehaviour
{
    public Transform[] targets;
    public GameObject[] indicators;
    public float padding = 15f;
    public Camera cam;


    private void LateUpdate()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(targets[i].position);
            bool isBehind = screenPosition.z < 0;
            bool isOnScreen = screenPosition.x > 0 && screenPosition.x < Screen.width && screenPosition.y > 0 && screenPosition.y < Screen.height;

            if (isOnScreen)
            {
                indicators[i].SetActive(false);
                continue;
            }
            indicators[i].SetActive(true);



            Vector3 local = cam.transform.InverseTransformPoint(targets[i].position);
            Vector2 dir = new Vector2(local.x, local.y).normalized;

            Vector2 center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
            float scale = Mathf.Min(center.x / Mathf.Abs(dir.x), center.y / Mathf.Abs(dir.y));
            Vector2 pos = center + dir * scale;
            indicators[i].transform.position = pos;


            //if (isBehind)
            //{
            //    screenPosition *= -1f;
            //}
            //screenPosition.x = Mathf.Clamp(screenPosition.x, padding, Screen.width - padding);
            //screenPosition.y = Mathf.Clamp(screenPosition.y, padding, Screen.height - padding);
            //screenPosition.z = 0f;

            //indicators[i].transform.position = screenPosition;
        }
    }
}

