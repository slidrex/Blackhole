using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Editor : MonoBehaviour
{
    [field: SerializeField] public List<BuildingTool> Tools { get; private set; } = new();
    [field: SerializeField] public List<EntityHolder> Holders { get; private set; } = new();
    [field: SerializeField] public Transform Parent { get; private set; }
    public EntityHolder CurrentHolder { get; private set; }
    [field: SerializeField] public EditorSpaceController SpaceController { get; private set; }
    [SerializeField] private Button _runButton;

    private void Start()
    {
        _runButton.onClick.AddListener(() => LevelController.Instance.Runner.RunLevel());
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
    public void SwitchEditorMode(LevelInteractController.Level currentLevel, bool trueIfSwitch)
    {
        gameObject.SetActive(trueIfSwitch);
        SpaceController.SetLevel(currentLevel);
        SpaceController.UpdateLevelAllocateStatus();
    }
    public void ActiveRunButton(bool active) => _runButton.gameObject.SetActive(active);
}
