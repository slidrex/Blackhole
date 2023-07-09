using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EditorSpaceController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelSpaceView;
    private LevelInteractController.Level currentLevel;
    [SerializeField] private Editor editor;
    public void SetLevel(LevelInteractController.Level level)
    {
        currentLevel = level;
    }
    public bool TryAllocateSpace(Entity entity)
    {
        int newSpace = GetEntityAllocatedSpace() + entity.SpaceRequired;
        if (newSpace <= currentLevel.AvailableSpace)
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
        if(currentLevel != null) available = currentLevel.AvailableSpace;
        int allocated = GetEntityAllocatedSpace();
        _levelSpaceView.text = $"{allocated}/{available}";
        editor.ActiveRunButton(available == allocated);
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
