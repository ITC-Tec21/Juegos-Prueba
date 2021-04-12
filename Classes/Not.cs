using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Not : CircuitComponent
{

    public Not(Vector3Int _location, Circuit _circuit) : base(_location, _circuit) {}

    // cambiar de izquierda a derecha nada mas
    public override void Connect(){
        List<Vector3Int> possibleNeighbors = new List<Vector3Int>();
        
        Vector3Int rightNeighbor = location + new Vector3Int(1, 0, 0);
        possibleNeighbors.Add(rightNeighbor);
        
        Vector3Int leftNeighbor = location + new Vector3Int(-1, 0, 0);
        possibleNeighbors.Add(leftNeighbor);

        Vector3Int twoStepsLeftNeighbor = location + new Vector3Int(-2, 0, 0);
        possibleNeighbors.Add(twoStepsLeftNeighbor);
        
        foreach(Vector3Int neighborLocation in possibleNeighbors){
            if (circuit.circuitComponents.TryGetValue(neighborLocation, out CircuitComponent neighbor)) {
                if (neighborLocation == twoStepsLeftNeighbor && neighbor is LogicGate) {
                    ins.Add(neighbor);
                    neighbor.outs.Add(this);
                }
                else if (neighborLocation == rightNeighbor && (neighbor is Output || neighbor is LogicGate)){
                    outs.Add(neighbor);
                    neighbor.ins.Add(this);
                }
                else if (neighbor is Input && neighborLocation == leftNeighbor){
                    ins.Add(neighbor);
                    neighbor.outs.Add(this);
                }
                else if (neighbor is Wire) {
                    if (neighborLocation == leftNeighbor) {
                        ins.Add(neighbor);
                        neighbor.outs.Add(this);
                    }
                    else if (neighborLocation == rightNeighbor) {
                        outs.Add(neighbor);
                        neighbor.ins.Add(this);
                    }
                }
            }
        }

        Turn(CheckIns());
    }

    public override bool CheckIns() {
        if (ins.Count == 0) {
            return false;
        }
        else {
            return !ins.First().on;
        }
    }
}