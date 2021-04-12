using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LogicGate : CircuitComponent
{

    // cablecitos dobles
    public LogicGate(Vector3Int _location, Circuit _circuit) : base(_location, _circuit) {}

    //connect two to the right
    public override void Connect(){
        List<Vector3Int> possibleNeighbors = new List<Vector3Int>();

        Vector3Int upperNeighbor = location + new Vector3Int(0, 1, 0);
        possibleNeighbors.Add(upperNeighbor);

        Vector3Int lowerNeighbor = location + new Vector3Int(0, -1, 0);
        possibleNeighbors.Add(lowerNeighbor);
        
        Vector3Int leftNeighbor = location + new Vector3Int(-1, 0, 0);
        possibleNeighbors.Add(leftNeighbor);
        
        foreach(Vector3Int neighbors in possibleNeighbors){
            if (circuit.circuitComponents.TryGetValue(neighbors, out CircuitComponent neighbor)) {
                ins.Add(neighbor);
                neighbor.outs.Add(this);
            }
        }

        Vector3Int twoStepsRightNeighbor = location + new Vector3Int(2, 0, 0);
        if (circuit.circuitComponents.TryGetValue(twoStepsRightNeighbor, out CircuitComponent neighbor)) {
            outs.Add(neighbor);
            neighbor.ins.Add(this);
        }

        Turn(CheckIns());
    }
}