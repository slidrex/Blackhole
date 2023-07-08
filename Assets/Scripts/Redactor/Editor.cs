using UnityEngine;
using System.Collections.Generic;

public class Editor : MonoBehaviour
{
    [field: SerializeField] public List<BuildingTool> Tools { get; private set; } = new();
    [field: SerializeField] public List<EntityHolder> Holders { get; private set; } = new();
    [field: SerializeField] public Transform Parent { get; private set; }
    public EntityHolder CurrentHolder { get; private set; }

    private void Start()
    {
        Transform content = GameObject.Find("HolderContent").transform;
        Holders.AddRange(content.GetComponentsInChildren<EntityHolder>());
        for (int i = 0; i < Tools.Count; i++)
            Tools[i].SetEditor(this);
        for (int i = 0;i < Holders.Count; i++)
            Holders[i].SetEditor(this);
    }
    public void SetCurrentHolder(EntityHolder holder)
    {
        for (int i = 0; i < Holders.Count; i++)
        {
            Holders[i]._backLight.SetActive(false);
        }
        CurrentHolder = holder;
    }
}
