using UnityEngine;

public enum BuildingType {
    Dome, Factory
}

[CreateAssetMenu(fileName = "Building Data", menuName = "Data/Building Data")]
public class BuildingSO : ScriptableObject {
    public BuildingType buildingType;

    // Building operation requirements
    public int wattage;

    public int workload;

    // Recipe to build this building
    public RecipeSO buildRecipe;

    // Recipe object produced by this building
    public RecipeSO recipe;

    public Sprite sprite;
}