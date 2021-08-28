using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Producer Data", menuName = "Data/Producer Data")]
public class ProducerSO : ScriptableObject {
    
    // List of resources produced by this producer
    public List<Item> producedResources { get; protected set; }

    // Dict of resource -> time to produce one unit
    public Dictionary<Item, int> resourceProductionTimeDict { get; protected set; }

}

