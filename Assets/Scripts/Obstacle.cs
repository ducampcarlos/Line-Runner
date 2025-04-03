using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if(transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
