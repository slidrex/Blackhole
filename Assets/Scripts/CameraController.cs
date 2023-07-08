using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    private int i = 0;

    private void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, points[i].position, Time.deltaTime * 5);
    }
}
