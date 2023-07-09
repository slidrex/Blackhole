using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InfoController : MonoBehaviour
{
    [SerializeField] private bool _closeApp;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private InfoTableObject _infoTableObject;
    [SerializeField] private Transform _table;
    [SerializeField] private float _typingSpeed;
    [SerializeField] private Button _nextLineButton;
    private Queue<string> currentQueue;

    private void Awake()
    {
        InsertObject(_infoTableObject);
        _nextLineButton.onClick.RemoveAllListeners();
        _nextLineButton.onClick.AddListener(StartMoveQueue);
    }
    public void InsertObject(InfoTableObject infoTable)
    {
        _table.gameObject.SetActive(true);
        currentQueue = new Queue<string>();
        foreach (var str in _infoTableObject.TableQueue)
        {
            currentQueue.Enqueue(str);
        }
        StartMoveQueue();
    }
    private void StartMoveQueue()
    {
        if (currentQueue.TryDequeue(out var sentence))
        {
            StartCoroutine(PrintLine(sentence));
            SetActiveNextLineButton(false);
        }
        else
        {
            OnQueueEnd();
            _table.gameObject.SetActive(false);
        }
    }
    private IEnumerator PrintLine(string line)
    {
        _text.text = "";
        int lineLength = line.Length;
        int i = 0;
        while (lineLength > 0)
        {
            yield return new WaitForSeconds(1 / _typingSpeed);
            _text.text += line[i];
            lineLength--;
            i++;
        }
        OnQueueAnimationEnd();
    }
    private void OnQueueAnimationEnd()
    {
        SetActiveNextLineButton(true);
    }
    private void OnQueueEnd()
    {
        if (_closeApp) SceneManager.LoadScene(0);
    }
    private void SetActiveNextLineButton(bool active)
    {
        _nextLineButton.gameObject.SetActive(active);
    }
}
