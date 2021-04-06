using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public bool on = false;
    private GameObject next;
    private GameObject prev;
    public Collider2D outputCol;
    public Collider2D inputCol;

    private void OnTriggerStay2D(Collider2D other){
        if(other.gameObject.tag == "Wire")
        next = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other){
        next.GetComponent<Wire>().Deactivate();
        next = null;
    }
    public void Activate(){
        on = true;
        this.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);

        if(next != null){
            if(next.tag == "Wire")
            next.GetComponent<Wire>().Activate();
        }
        
    }

    public void Deactivate(){
        on = false;
        this.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
    }
}
