using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EditorSpaceController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelSpaceView;
    public LevelInteractController.Level CurrentLevel { get; private set; }
    [SerializeField] private Editor editor;
    public void SetLevel(LevelInteractController.Level level)
    {
        CurrentLevel = level;
    }
    public bool TryAllocateSpace(Entity entity)
    {
        int newSpace = GetEntityAllocatedSpace() + entity.SpaceRequired;
        if (newSpace <= CurrentLevel.AvailableSpace)
        {
            return true;
        }
        OnNotEnoughSpace();
        return false;
    }
    public void UpdateLevelAllocateStatus()
    {
        LevelController.Instance.LevelInfo.UpdateMapInfo();
        int available = 0;
        if(CurrentLevel != null) available = CurrentLevel.AvailableSpace;
        int allocated = GetEntityAllocatedSpace();
        SetLevelSpaceView(allocated);
        editor.ActiveRunButton(available == allocated);
    }
    public void SetLevelSpaceView(int allocated)
    {
        _levelSpaceView.text = $"{allocated}/{CurrentLevel.AvailableSpace}";

    }
    private int GetEntityAllocatedSpace()
    {
        if (LevelController.Instance.LevelInfo?.Entities == null) return 0;
        int space = 0;
        foreach (var obj in LevelController.Instance.LevelInfo.Entities)
        {
            if(obj != null)
                space += obj.SpaceRequired;
        }
        return space;
    }
    public void OnNotEnoughSpace()
    {

    }
}
