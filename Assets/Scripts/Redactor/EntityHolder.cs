using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityHolder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _housingSpace;
    [SerializeField] private Image _icon;
    [field: SerializeField] public Entity _entity { get; private set; }
    [field: SerializeField] public GameObject _backLight { get; private set; }
    private Editor _editor;
    public void SetEditor(Editor editor) => _editor = editor;
    private void Awake()
    {
        var renderer = _entity.GetComponent<SpriteRenderer>();
        _icon.sprite = renderer.sprite;
        _icon.color = renderer.color;
        _housingSpace.text = _entity.SpaceRequired.ToString();
    }
    public void OnSelect()
    {
        _editor.SetCurrentHolder(this);
        SoundController.Instance.PlaySelectEntity();
        _backLight.SetActive(true);
    }
}
