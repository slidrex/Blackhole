using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
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
    private int currentLevel;
    private void Start()
    {
        StartGame();
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
    }
    public void StartGame()
    {
        currentLevel = 0;
        LevelController.Instance.LevelInfo.DestroyAllEntities();
        LevelController.Instance.Runner.OnGameStart?.Invoke();
        SetupLevelEnities();
        SetPlaymode(PlayMode.Game);
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
        if(levels.Length <= curLevelIndex - 1)
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
        var level = currentLevel >= 1 ? levels[currentLevel - 1] : null;
        editor.SwitchEditorMode(level, playmode == PlayMode.Editor);
    }
    public void OnGameEnd()
    {

    }
}
