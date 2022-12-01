using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/plantData", fileName = "Plant_Data")]
public class PlantData : ScriptableObject
{

    public string plantname;
    public float Require_water;
    public float Duration = 20f;

    public GameObject PlantModeling;
}
