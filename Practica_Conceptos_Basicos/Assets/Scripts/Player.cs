using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int health = 0;
    Vector3 motion;
    [SerializeField] float speed;
    public Text pointDisplay; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pointDisplay.text = health.ToString();
        motion.y = Input.GetAxisRaw("Vertical");
        motion.x = Input.GetAxisRaw("Horizontal");
        transform.position = transform.position + motion * speed;
        if(health < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
