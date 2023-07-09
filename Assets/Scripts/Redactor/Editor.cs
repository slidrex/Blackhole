using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Threading.Tasks;

public class Editor : MonoBehaviour
{
    [SerializeField] private GameObject playmodeScreen;
    [SerializeField] private GameObject editorScreen;
    [field: SerializeField] public List<BuildingTool> Tools { get; private set; } = new();
    [field: SerializeField] public List<EntityHolder> Holders { get; private set; } = new();
    [field: SerializeField] public Transform Parent { get; private set; }
    public EntityHolder CurrentHolder { get; private set; }
    [field: SerializeField] public EditorSpaceController SpaceController { get; private set; }
    [SerializeField] private Button _resetButton;
    [SerializeField] private Button _runButton;
    [SerializeField] private Button _stopButton;
    private void Start()
    {
        LevelController.Instance.Runner.OnLevelRun +=OnLevelRun;
        _resetButton.onClick.AddListener(() => LevelController.Instance.InteractController.StartGame());
        _runButton.onClick.AddListener(OnRunButtonPressed);
        _stopButton.onClick.AddListener(() => LevelController.Instance.Runner.StopLevel());
        
        Transform content = GameObject.Find("HolderContent").transform;
        Holders.AddRange(content.GetComponentsInChildren<EntityHolder>());
        for (int i = 0; i < Tools.Count; i++)
            Tools[i].SetEditor(this);
        for (int i = 0;i < Holders.Count; i++)
            Holders[i].SetEditor(this);
    }
    private void OnRunButtonPressed()
    {
        
        if (LevelController.Instance.IsRunning || LevelController.Instance.InteractController.Mode == LevelInteractController.PlayMode.Game) LevelController.Instance.InteractController.MoveNext();
        else LevelController.Instance.Runner.RunLevel();
    }

    private void OnDestroy()
    {
        LevelController.Instance.Runner.OnLevelRun -= OnLevelRun;
        _runButton.onClick.RemoveAllListeners();
        _resetButton.onClick.RemoveAllListeners();
    }
    public void SetCurrentHolder(EntityHolder holder)
    {
        for (int i = 0; i < Holders.Count; i++)
        {
            Holders[i]._backLight.SetActive(false);
        }
        CurrentHolder = holder;
    }
    private void OnLevelRun(bool isRunning)
    {
        _stopButton.gameObject.SetActive(isRunning);
        SwitchEditorMode(SpaceController.CurrentLevel, !isRunning);
        
        if(!isRunning)
        {
            LevelController.Instance.InteractController.SetupCurrentLevel();
            SpaceController.UpdateLevelAllocateStatus();
            SpaceController.SetLevelSpaceView(0);
        }
        ActiveRunButton(false);
    }
    public void SwitchEditorMode(LevelInteractController.Level currentLevel, bool trueIfSwitch)
    {
        editorScreen.SetActive(trueIfSwitch);
        playmodeScreen.SetActive(!trueIfSwitch);
        if (trueIfSwitch)
        {
            SpaceController.SetLevel(currentLevel);
            SpaceController.UpdateLevelAllocateStatus();
        }
    }
    public void ActiveRunButton(bool active)
    {
        _runButton.gameObject.SetActive(active);
        _stopButton.gameObject.SetActive(!active);
    }
}
