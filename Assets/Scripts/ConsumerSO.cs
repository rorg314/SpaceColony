using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Defines consumer component of a building

[CreateAssetMenu(fileName = "Consumer Data", menuName = "Data/Consumer Data")]
public class ConsumerSO : ScriptableObject {
    
    // List of consumed resources
    public List<Item> consumedResources { get; protected set; }

    // Dict of resource -> time to consume one unit 
    public Dictionary<Item, int> resourceConsumptionTimeDict { get; protected set; }

}
