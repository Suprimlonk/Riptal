using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float bobSpeed = 1f;
    public float bobHeight = 0.5f;
    public float rotateSpeed = 30f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float bobOffset = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(startPosition.x, startPosition.y + bobOffset, startPosition.z);
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }
}
