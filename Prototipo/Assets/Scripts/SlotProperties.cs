using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotProperties : MonoBehaviour
{
    public GameObject gate;
    [SerializeField] public bool editable;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setGate(GameObject newGate) {
        gate = newGate;
    }

    public GameObject getGate(){
        return gate;
    }
}
