using UnityEngine;

public class OffscreenIndicator : MonoBehaviour
{
    public Transform[] targets;
    public GameObject[] indicators;
    public float padding = 15f;


    private void LateUpdate()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(targets[i].position);
            bool isBehind = screenPosition.z < 0;
            bool isOnScreen = !isBehind && screenPosition.x > 0 && screenPosition.x < Screen.width && screenPosition.y > 0 && screenPosition.y < Screen.height;

            if (isOnScreen)
            {
                indicators[i].SetActive(false);
                continue;
            }
            indicators[i].SetActive(true);

            if (isBehind)
            {
                screenPosition *= -1f;
            }
            screenPosition.x = Mathf.Clamp(screenPosition.x, padding, Screen.width - padding);
            screenPosition.y = Mathf.Clamp(screenPosition.y, padding, Screen.height - padding);
            screenPosition.z = 0f;

            indicators[i].transform.position = screenPosition;
        }
    }
}

