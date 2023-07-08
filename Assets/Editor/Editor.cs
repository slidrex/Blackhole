using UnityEngine;
using System.Collections.Generic;

public class Editor : MonoBehaviour
{
    [field: SerializeField] public BuildingTool CurrentTool { get; private set; }
    // entities array
    [field: SerializeField] public List<BuildingTool> Tools { get; private set; }
    public void SetCurrentTool(BuildingTool tool)
    {
        CurrentTool = tool;
    }
}
