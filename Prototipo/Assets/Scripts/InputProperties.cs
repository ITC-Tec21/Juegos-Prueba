using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProperties : MonoBehaviour
{
    public bool on;

    private void OnTriggerStay2D(Collider2D other){
        Debug.Log("Collision");
        if(other.gameObject.tag == "Wire" && on)
        other.gameObject.GetComponent<Wire>().Activate();
    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "Wire")
        other.gameObject.GetComponent<Wire>().Deactivate();
    }
}
