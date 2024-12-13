using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float imageWidth;
    [SerializeField] private GameObject target;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float distanceLeft = (speed * Time.time) % imageWidth;
        transform.position = initialPosition + distanceLeft * Vector3.left;
    }
}
