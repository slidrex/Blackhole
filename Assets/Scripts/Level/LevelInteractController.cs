using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelInteractController : MonoBehaviour
{
    [Serializable]
    public class Level
    {
        public Transform PlayerPosition;
        public Transform CameraPosition;
        public int CameraSize;
        public byte AvailableSpace;
    }
    public enum PlayMode
    {
        Game, Editor
    }
    public PlayMode Mode;
    [SerializeField] private Editor editor;
    [SerializeField] private Transform _startLevelCameraPosition;
    [SerializeField] private Transform _startLevelPlayerPosition;
    [SerializeField] private Level[] levels;
    [SerializeField] private Camera _camera;
    [SerializeField] private Entity violet;
    [SerializeField] private Transform violetPosition;
    private int currentLevel;
    public bool IsLastLevel;
    private void Start()
    {
        StartGame();
        LevelController.Instance.Runner.OnLevelRun += OnLevelRun;
    }
    private void OnDestroy()
    {
        LevelController.Instance.Runner.OnLevelRun -= OnLevelRun;
    }
    private void OnLevelRun(bool run)
    {
        if (IsLastLevel)
        {
            if (!run)
                OnLastLevelLoaded();
        }
    }
    public void CheckSceneState()
    {
        var mobs = FindObjectsOfType<Mob>();
        if (mobs == null) editor.ActiveRunButton(true);
        foreach(var mob in mobs)
        {
            if (mob.IsDead == false) return;
        }
        editor.ActiveRunButton(true);
    }
    public void MoveNext()
    {
        currentLevel++;
        LevelController.Instance.Runner.OnMoveNext.Invoke();
        
        StopLevel();
        editor.ActiveRunButton(false);
        if (currentLevel >= levels.Length) OnLastLevelLoaded();
    }
    public void OnLastLevelLoaded()
    {
        IsLastLevel = true;
        Instantiate(violet, violetPosition.transform.position, Quaternion.identity);
    }
    public void StartGame()
    {
        IsLastLevel = false;
        currentLevel = 0;
        LevelController.Instance.LevelInfo.DestroyAllEntities();
        LevelController.Instance.Runner.OnGameStart?.Invoke();
        SetupLevelEnities();
        SetPlaymode(PlayMode.Game);
        editor.ActiveRunButton(true);
    }
    public void StopLevel()
    {
        SetupCurrentLevel();
        LevelController.Instance.Runner.StopLevel();
    }
    public void SetupCurrentLevel()
    {
        LevelController.Instance.LevelInfo.DestroyAllEntities();
        SetupLevelEnities();
        SetPlaymode(PlayMode.Editor);
    }
    private void SetupLevelEnities()
    {
        int curLevelIndex = currentLevel - 1;
        Vector2 cameraPos, playerPos;
        if(levels.Length <= curLevelIndex)
        {
            OnGameEnd();
            return;
        }
        if(curLevelIndex < 0)
        {
            cameraPos = _startLevelCameraPosition.position;
            playerPos = _startLevelPlayerPosition.position;
        }
        else
        {
            Camera.main.orthographicSize = levels[curLevelIndex].CameraSize;
            cameraPos = levels[curLevelIndex].CameraPosition.position;
            playerPos = levels[curLevelIndex].PlayerPosition.position;
        }
        Player.Instance.SetPosition(playerPos);
        _camera.transform.position = new Vector3(cameraPos.x, cameraPos.y, _camera.transform.position.z);
    }
    public void SetPlaymode(PlayMode playmode)
    {
        Mode = playmode;
        Level level;
        if (levels.Length <= currentLevel - 1) level = null;
        else
        level = currentLevel >= 1 ? levels[currentLevel - 1] : null;
        editor.SwitchEditorMode(level, playmode == PlayMode.Editor);
    }
    public void OnGameEnd()
    {
        SceneManager.LoadScene(2);
    }
}
