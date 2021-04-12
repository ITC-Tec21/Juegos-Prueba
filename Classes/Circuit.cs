using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Circuit
{
    public Tilemap tilemap;
    public Dictionary<Vector3Int, CircuitComponent> circuitComponents;

    public void AddComponent(Vector3Int location, CircuitComponent component){
        circuitComponents.Add(location, component);
        component.Connect();
    }

    // public void AddComponent(Vector3Int location, Type type){
    //     circuitComponents.Add(location, new CircuitComponent(location, this) as type);
    //     circuitComponents.Item[location].Connect();
    // }

    public void RemoveComponent(Vector3Int location){
        circuitComponents.Item[location].Disconnect();
        circuitComponents.Remove(location);
    }
}