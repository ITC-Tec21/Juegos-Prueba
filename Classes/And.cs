using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class And : LogicGate
{
    public And(Vector3Int _location, Circuit _circuit) : base(_location, _circuit) {}

    // Checks inputs and returns true or false according to gate truth table
    public override bool checkIns() {
        if (ins.Count == 2) {
            bool a = ins.First().on;
            bool b = ins.Skip(1).First().on;
            return a && b;
        }
        else {
            return false;
        }
    }
}