using UnityEngine;

public abstract class BuildingTool : MonoBehaviour
{
    protected Editor _editor;
    public abstract void Execute();
    public void SetEditor(Editor e) => _editor = e;
}
