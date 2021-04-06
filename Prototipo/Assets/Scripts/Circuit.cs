using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker : MonoBehaviour
{

    public GameObject slotGroup, inputs, wires, Gates, Outputs;
    // Start is called before the first frame update
    void Start()
    {
        SimulateCircuit();
    }

    void SimulateCircuit(){
        inputs.GetComponentInChildren<Collider2D>();
    }
}