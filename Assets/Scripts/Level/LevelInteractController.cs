using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInteractController : MonoBehaviour
{
    [Serializable]
    public class Level
    {
        public Transform PlayerPosition;
        public Transform CameraPosition;
        public byte AvailableSpace;
    }
    public enum PlayMode
    {
        Game, Editor
    }
    [SerializeField] private Editor editor;
    [SerializeField] private Transform _startLevelCameraPosition;
    [SerializeField] private Transform _startLevelPlayerPosition;
    [SerializeField] private Level[] levels;
    [SerializeField] private Camera _camera;
    private int currentLevel;
    public void MoveNext()
    {
        currentLevel++;
        if(currentLevel != 1)
            LevelController.Instance.LevelInfo.DestroyAllEntities();
        SetupLevelEnities();
        SetPlaymode(PlayMode.Editor);
    }
    public void StartGame()
    {
        currentLevel = 0;
        SetPlaymode(PlayMode.Game);
        SetupLevelEnities();
    }
    private void SetupLevelEnities()
    {
        int curLevelIndex = currentLevel - 1;
        Vector2 cameraPos, playerPos;
        if(curLevelIndex < 0)
        {
            cameraPos = _startLevelCameraPosition.position;
            playerPos = _startLevelPlayerPosition.position;
        }
        else
        {
            cameraPos = levels[curLevelIndex].CameraPosition.position;
            playerPos = levels[curLevelIndex].PlayerPosition.position;
        }
        Player.Instance.SetPosition(playerPos);
        _camera.transform.position = new Vector3(cameraPos.x, cameraPos.y, _camera.transform.position.z);
    }
    public void SetPlaymode(PlayMode playmode)
    {
        var level = currentLevel >= 1 ? levels[currentLevel - 1] : null;
        editor.SwitchEditorMode(level, playmode == PlayMode.Editor);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveNext();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }
}
