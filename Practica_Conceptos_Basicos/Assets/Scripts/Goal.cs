using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public int points = 1;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    [SerializeField]Vector2 limits;
    
    Vector3 position;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().health += points;
            transform.position =  new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
            Debug.Log(other.GetComponent<Player>().health);
        }
    }

}
