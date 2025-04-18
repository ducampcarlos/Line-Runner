using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private PlayerStates obstacleType;
    [SerializeField] private Color colorForThisState = Color.white;

    private List<SpriteRenderer> sr = new List<SpriteRenderer>();

    private void OnEnable()
    {
        if (sr.Count == 0)
        {
            SpriteRenderer single;
            if (TryGetComponent<SpriteRenderer>(out single) && single != null)
            {
                sr.Add(single);
            }
            else if (GetComponentsInChildren<SpriteRenderer>().ToList() != null)
            {
                foreach (var sprite in GetComponentsInChildren<SpriteRenderer>().ToList())
                {
                    if (sprite != null && sprite.gameObject != gameObject)
                    {
                        sr.Add(sprite);
                    }
                }
            }
        }

        foreach (var sprite in sr)
        {
            sprite.color = colorForThisState;
        }
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < -15)
        {
            gameObject.SetActive(false); // En lugar de destruir, desactivamos
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public PlayerStates GetObstacleType()
    {
        return obstacleType;
    }
}
