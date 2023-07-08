using UnityEngine;

public class EntityHolder : MonoBehaviour
{
    [field: SerializeField] public Entity _entity { get; private set; }
    [field: SerializeField] public GameObject _backLight { get; private set; }
    private Editor _editor;
    public void SetEditor(Editor editor) => _editor = editor;
    public void OnSelect()
    {
        _editor.SetCurrentHolder(this);
        _backLight.SetActive(true);
    }
}
