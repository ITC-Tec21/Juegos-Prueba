using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class CircuitComponent
{
    public Vector3Int location;
    public bool on;
    public HashSet<CircuitComponent> ins;
    public HashSet<CircuitComponent> outs;
    public Circuit circuit;

    public CircuitComponent(Vector3Int _location, Circuit _circuit){
        location = _location;
        on = false;
        ins = new HashSet<CircuitComponent>();
        outs = new HashSet<CircuitComponent>();
        circuit = _circuit;
    }

    public virtual void Connect(){}

    public virtual bool CheckIns(){}

    // Obeys status parameter
    public void Turn(bool status) {
        on = status;
        foreach(CircuitComponent outp in outs) {
            // si ya esta prendido o apagado no vuelve a prenderlo o apagarlo
            if ((outp is Wire) && (outp.on != status)) {
                outp.Turn(status);
            }
            else if ((outp is LogicGate) || (outp is Not)) {
                outp.Turn(CheckIns());
            }
        }
    }

    // disconnect() que tenga bfs
    public void Disconnect() {
        foreach(CircuitComponent inp in ins) {
            inp.outs.Remove(this);
        }
        foreach(CircuitComponent outp in outs) {
            outp.ins.Remove(this);
            if (outp.on) {
                outp.BFS();
            }
        }
    }

    public void BFS()
    {
        int maxIterations = 100;

        HashSet<CircuitComponent> visited = new HashSet<CircuitComponent>();
        HashSet<CircuitComponent> gates = new HashSet<CircuitComponent>();
        Queue<CircuitComponent> componentQueue = new Queue<CircuitComponent>();
        CircuitComponent currentComponent = this;
        componentQueue.Enqueue(currentComponent);
        bool foundInput = false;

        while(componentQueue.Count != 0 && maxIterations > 0)
        {
            maxIterations --;

            currentComponent = componentQueue.Dequeue();
            visited.Add(currentComponent);
            // si encuentra un input prendido
            if (currentComponent is InputComponent && currentComponent.on) {
                foundInput = true;
            }
            else if (currentComponent is LogicGate || currentComponent is Not) {
                gates.Add(currentComponent);
            }
            // cambie de ins a outs
            foreach(CircuitComponent neighbor in currentComponent.ins){
                if(visited.Contains(neighbor) == false)
                {
                    componentQueue.Enqueue(neighbor);
                }
            }
        }

        foreach (CircuitComponent component in visited) {
            component.on = foundInput;
        }
        foreach (CircuitComponent gate in gates) {
            gate.Turn(CheckIns());
        }
    }
}