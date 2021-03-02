using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector3 motion;
    [SerializeField]float speed;
    [SerializeField]float limit;

    float direction = 0.2f;
    bool right = true;

    public int points = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > limit && right)
        {
            direction = -direction;
            right = false;
        }
        if(transform.position.x < -limit && !right)
        {
            direction = 0.2f;
            right = true;
        }
        motion.x = direction;
        transform.position = transform.position + motion * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().health -= points;
            Debug.Log(other.GetComponent<Player>().health);
        }
    }
}
